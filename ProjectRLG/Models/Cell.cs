namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using ProjectRLG.Contracts;
using Microsoft.Xna.Framework;

    public class Cell : BaseObject, ICell
    {
        private Point _p;
        private ITerrain _terrain;

        public Cell() : base()
        {
            Items = new HashSet<IGameItem>();
            Objects = new HashSet<IMapObject>();
            SpecialObjects = new HashSet<ISpecialObject>();
        }

        public bool IsTransparent
        {
            get { return !Glyph.Text.Equals("#"); }
        }
        public bool IsVisible { get; set; }
        public int X 
        {
            get
            {
                return _p.X;
            }
            set
            {
                _p.X = value;
            }
        }
        public int Y 
        {
            get
            {
                return _p.Y;
            }
            set
            {
                _p.Y = value;
            }
        }
        public ITerrain Terrain
        {
            get
            {
                if (_terrain == null)
                {
                    _terrain = new Terrain(new Glyph(" "), 0);
                }

                return _terrain;
            }
            set
            {
                _terrain = value;
            }
        }
        public IActor Actor { get; set; }
        public HashSet<IGameItem> Items { get; set; }
        public HashSet<IMapObject> Objects { get; set; }
        public HashSet<ISpecialObject> SpecialObjects { get; set; }
        public Point Position
        {
            get
            {
                return _p;
            }
            set
            {
                _p = value;
            }
        }

        public override IGlyph Glyph
        {
            get
            {
                if (Actor != null)
                {
                    return Actor.Glyph;
                }
                else if (Terrain != null)
                {
                    return Terrain.Glyph;
                }

                return new Glyph(" ");
            }
            set { }
        }
        public bool IsCellAvailable
        {
            get
            {
                return (Actor == null && Terrain.Difficulty < 50);
            }
        }
    }
}
