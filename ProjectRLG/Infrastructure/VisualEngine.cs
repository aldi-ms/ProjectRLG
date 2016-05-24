//
//  VisualEngine.cs
//
//  Author:
//       scienide <alexandar921@abv.bg>
//
//  Copyright (c) 2015 scienide
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace ProjectRLG.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ProjectRLG.Contracts;
    using ProjectRLG.Enums;
    using ProjectRLG.Infrastructure.FieldOfView;
    using ProjectRLG.Utilities;
    using ProjectRLG.Constants;

    public class VisualEngine
    {
        private static readonly Color TileMask = Color.DarkSlateGray;

        private bool _highlighterIsOn;
        private bool _graphicMode;
        private int _tileSize;
        private Texture2D _highlightTexture;
        private List<Point> _tilesToHighlight;
        private IMap _currentMap;
        private Dictionary<string, Texture2D> _spriteDict;
        private FieldOfView<ICell> _fieldOfView;
        private ContentManager _content;

        public VisualEngine(
            int tileSize,
            int x,
            int y,
            IMap map,
            ContentManager contentManager)
            : this(true, tileSize, new Point(x, y), map, contentManager, null)
        {
        }
        public VisualEngine(
            int tileSize,
            int x,
            int y,
            IMap map,
            SpriteFont gameFont)
        : this(false, tileSize, new Point(x, y), map, null, gameFont)
        {
        }
        private VisualEngine(
            bool useGraphicMode,
            int tileSize,
            Point mapDrawboxTileSize,
            IMap map, 
            ContentManager content = null,
            SpriteFont spriteFont = null)
        {
            _graphicMode = useGraphicMode;
            _tileSize = tileSize;
            MapDrawboxTileSize = mapDrawboxTileSize;
            Map = map;
            FOVSettings = new FOVSettings();

            // Set defaults
            TopMargin = 10;
            LeftMargin = 10;
            _spriteDict = new Dictionary<string, Texture2D>();
            DeltaTileDrawCoordinates = new Point(0, 0);
            _highlighterIsOn = false;
            _tilesToHighlight = new List<Point>();

            ASCIIColor = Color.White;
            ASCIIRotation = 0f;
            ASCIIScale = 1f;
            ASCIIOrigin = new Vector2(0, 0);
            ASCIIEffects = SpriteEffects.None;
            LayerDepth = 0f;

            if (useGraphicMode)
            {
                if (content == null)
                {
                    throw new ArgumentNullException(
                        "content",
                        "ContentManager should be supplied to the constructor if Tiles mode is to be used.");
                }

                _content = content;
            }
            else
            {
                if (spriteFont == null)
                {
                    throw new ArgumentNullException(
                        "spriteFont",
                        "SpriteFont should be supplied to the constructor if ASCII mode is to be used.");
                }

                SpriteFont = spriteFont;
            }
        }

        public int TileSize
        {
            get
            {
                return this._tileSize;
            }
        }
        public IMap Map
        {
            get
            {
                return this._currentMap;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        "Map",
                        "When setting VisualEngine's currentMap, it cannot be null!");
                }

                _currentMap = value;
                _fieldOfView = new FieldOfView<ICell>(_currentMap.Cells);
                if (_graphicMode)
                {
                    LoadSprites();
                }
            }
        }
        public Point MapDrawboxTileSize { get; set; }
        public int TopMargin { get; set; }
        public int LeftMargin { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public FOVSettings FOVSettings { get; set; }
        public Point DeltaTileDrawCoordinates { get; set; }
        public Color ASCIIColor { get; set; }
        public float ASCIIRotation { get; set; }
        public Vector2 ASCIIOrigin { get; set; }
        public float ASCIIScale { get; set; }
        public SpriteEffects ASCIIEffects { get; set; }
        /// <summary>
        /// Gets or sets the layer depth of the font when using ASCII.
        /// 0 represents the front layer and 1 represents a back layer.
        /// </summary>
        /// <value>The layer depth.</value>
        public float LayerDepth { get; set; }

        public void DrawGame(SpriteBatch spriteBatch, Point mapCenter)
        {
            DrawMap(spriteBatch, mapCenter);
        }
        public void HighlightPath(List<Point> tiles)
        {
            if (tiles == null)
            {
                this._highlighterIsOn = false;
                this._tilesToHighlight.Clear();
                return;
            }

            this._highlighterIsOn = true;
            this._tilesToHighlight = tiles;
        }
        public void DrawGrid(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Texture2D simpleTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            simpleTexture.SetData<Color>(new[] { Color.White });

            spriteBatch.Begin();

            int xStop = this.MapDrawboxTileSize.X * this._tileSize;
            int yStop = this.MapDrawboxTileSize.Y * this._tileSize;

            for (int x = this.LeftMargin; x <= xStop + this.LeftMargin; x += this._tileSize)
            {
                Rectangle rect = new Rectangle(x, this.LeftMargin, 1, yStop);
                spriteBatch.Draw(simpleTexture, rect, Color.Wheat);
            }

            for (int y = this.TopMargin; y <= yStop + this.TopMargin; y += this._tileSize)
            {
                Rectangle rect = new Rectangle(this.TopMargin, y, xStop, 1);
                spriteBatch.Draw(simpleTexture, rect, Color.Wheat);
            }

            spriteBatch.End();
        }

        private void DrawMap(SpriteBatch spriteBatch, Point mapCenter)
        {
            if (_highlighterIsOn && _highlightTexture == null)
            {
                _highlightTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _highlightTexture.SetData(new Color[] { Color.Goldenrod });
            }

            _fieldOfView.ComputeFov(
                mapCenter.X,
                mapCenter.Y,
                FOVSettings.MaxRange,
                FOVSettings.LightWalls,
                FOVSettings.Method,
                FOVSettings.Shape);

            //Texture2D terrainTexture = null;
            //Texture2D itemTexture = null;
            //Texture2D fringeTexture = null;
            //Texture2D actorTexture = null;
            Point startTile = GetDrawCoordinatesStart(mapCenter);

            spriteBatch.Begin();

            for (int x = 0; x < this.MapDrawboxTileSize.X; x++)
            {
                for (int y = 0; y < this.MapDrawboxTileSize.Y; y++)
                {
                    Vector2 drawPosition = new Vector2(
                                               LeftMargin + (_tileSize * x) + DeltaTileDrawCoordinates.X,
                                               TopMargin + (_tileSize * y) + DeltaTileDrawCoordinates.Y);
                    Point cell = new Point(startTile.X + x, startTile.Y + y);

                    if (_graphicMode)
                    {
                        #region GraphicMode
                        /*
                        if (this._currentMap[tile].IsVisible)
                        {
                            if (!this._currentMap[tile].Flags.HasFlag(Flags.HasBeenSeen))
                            {
                                this._currentMap[tile].Flags |= Flags.HasBeenSeen;
                            }

                            // Draw the Terrain first as it should exist for every Tile.
                            terrainTexture = null;
                            if (this._spriteDict.TryGetValue(
                                    this._currentMap[tile].ObjectsContained.GetTerrain().DrawString,
                                    out terrainTexture))
                            {
                                spriteBatch.Draw(terrainTexture, drawPosition);
                            }

                            // Draw existing fringe objects
                            foreach (var fringe in this._currentMap[tile].ObjectsContained.GetFringes())
                            {
                                fringeTexture = null;
                                if (this._spriteDict.TryGetValue(fringe.DrawString, out fringeTexture))
                                {
                                    spriteBatch.Draw(fringeTexture, drawPosition);
                                }
                            }

                            // Draw existing items
                            foreach (var item in this._currentMap[tile].ObjectsContained.GetItems())
                            {
                                itemTexture = null;
                                if (this._spriteDict.TryGetValue(item.DrawString, out itemTexture))
                                {
                                    spriteBatch.Draw(itemTexture, drawPosition);
                                }
                            }

                            // Draw actor
                            actorTexture = null;
                            if (this._spriteDict.TryGetValue(
                                    this._currentMap[tile].ObjectsContained.GetActor().DrawString,
                                    out actorTexture))
                            {
                                spriteBatch.Draw(actorTexture, drawPosition);
                            }
                        }
                        else if (this._currentMap[tile].Flags.HasFlag(Flags.HasBeenSeen))
                        {
                            terrainTexture = null;
                            if (this._spriteDict.TryGetValue(
                                    this._currentMap[tile].ObjectsContained.GetTerrain().DrawString,
                                    out terrainTexture))
                            {
                                spriteBatch.Draw(terrainTexture, drawPosition, VisualEngine.TileMask);
                            }
                        }
                         * */
                        #endregion
                    }
                    else
                    {
                        // ASCII Mode
                        if (_currentMap[cell].IsVisible)
                        {
                            if (!_currentMap[cell].Properties.PropertyExistsAndIsNotNull(PropertyBagConst.HAS_BEEN_SEEN))
                            {
                                _currentMap[cell].Properties.SetProperty(PropertyBagConst.HAS_BEEN_SEEN, "true");
                            }

                            if (_highlighterIsOn && _tilesToHighlight.Contains(cell))
                            {
                                Rectangle rect = new Rectangle(
                                                     (int)drawPosition.X - DeltaTileDrawCoordinates.X,
                                                     (int)drawPosition.Y - DeltaTileDrawCoordinates.Y,
                                                     _tileSize,
                                                     _tileSize);

                                spriteBatch.Draw(_highlightTexture, rect, Color.Goldenrod);
                            }

                            spriteBatch.DrawString(
                                SpriteFont,
                                _currentMap[cell].Glyph.Text,
                                drawPosition,
                                ASCIIColor,
                                ASCIIRotation,
                                ASCIIOrigin,
                                ASCIIScale,
                                ASCIIEffects,
                                LayerDepth);
                        }
                        else if (_currentMap[cell].Properties.PropertyExistsAndIsNotNull(PropertyBagConst.HAS_BEEN_SEEN))
                        {
                            spriteBatch.DrawString(
                                SpriteFont,
                                _currentMap[cell].Glyph.Text,
                                drawPosition,
                                VisualEngine.TileMask,
                                ASCIIRotation,
                                ASCIIOrigin,
                                ASCIIScale,
                                ASCIIEffects,
                                LayerDepth);
                        }
                    }
                }
            }

            spriteBatch.End();
        }
        private void LoadSprites()
        {
            throw new NotImplementedException();

            //foreach (var tile in this._currentMap.Cells)
            //{
            //    foreach (var gameObject in tile.ObjectsContained)
            //    {
            //        if (this._spriteDict.ContainsKey(gameObject.DrawString))
            //        {
            //            break;
            //        }

            //        this._spriteDict.Add(
            //            gameObject.DrawString, 
            //            this._content.Load<Texture2D>(gameObject.DrawString));
            //    }
            //}
        }
        private Point GetDrawCoordinatesStart(Point mapCenter)
        {
            // Get the start coordinates for the Map
            Point startPoint = new Point(
                                  mapCenter.X - (MapDrawboxTileSize.X / 2),
                                  mapCenter.Y - (MapDrawboxTileSize.Y / 2));

            // Check coordinates lower bound < 0
            if (startPoint.X < 0)
            {
                startPoint.X = 0;
            }

            if (startPoint.Y < 0)
            {
                startPoint.Y = 0;
            }

            // Check coordinates higher bound > 0
            if (startPoint.X + MapDrawboxTileSize.X >= _currentMap.Cells.Height)
            {
                startPoint.X = _currentMap.Cells.Height - MapDrawboxTileSize.X;
            }

            if (startPoint.Y + MapDrawboxTileSize.Y >= _currentMap.Cells.Width)
            {
                startPoint.Y = _currentMap.Cells.Width - MapDrawboxTileSize.Y;
            }

            return startPoint;
        }
    }
}