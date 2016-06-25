namespace ProjectRLG
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using ProjectRLG.Contracts;
    using ProjectRLG.Infrastructure;
    using ProjectRLG.Models;
    using ProjectRLG.Utilities;
    using ProjectRLG.Enums;

    public class RogueLikeGame : Game
    {
        private const int SCREEN_WIDTH = 800;
        private const int SCREEN_HEIGHT = 500;

        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        private EventHandler<TextInputEventArgs> _onTextEntered;
        private KeyboardState _prevKeyState;
        private SpriteFont _logFont;
        private SpriteFont _voFont;
        private VisualEngine _vo;
        //private ActorQueue _actorQueue;
        private MessageLog _messageLog;
        private IMap _currentMap;
        private IActor _player;

        public RogueLikeGame()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
#if OpenGL
            Window.TextInput += TextEntered;
            onTextEntered += HandleInput;
#else
            Window.TextInput += HandleInput;
            _onTextEntered += HandleInput;
#endif
            // Load game objects required to create game
            _currentMap = new Map(MapUtilities.CreateRandomCellCollection(20, 15));
            _player = new Actor("SCiENiDE", new Glyph("@"));
            _currentMap.LoadActors(new List<IActor>() { _player });

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);

            // Load fonts
            _logFont = Content.Load<SpriteFont>("consolas12");
            _voFont = Content.Load<SpriteFont>("bpmono40bold");

            // Visual Engine
            _vo = new VisualEngine(32, 6, 10, _currentMap, _voFont);
            _vo.DeltaTileDrawCoordinates = new Point(3, 7);
            _vo.ASCIIScale = 0.6f;

            // Message Log
            Rectangle messageLogZone = new Rectangle(
                0,
                _vo.MapDrawboxTileSize.X * _vo.TileSize,
                SCREEN_WIDTH - 30,
                (SCREEN_HEIGHT - 30) - (_vo.MapDrawboxTileSize.X * _vo.TileSize));
            _messageLog = new MessageLog(messageLogZone, _logFont);
        }
        protected override void UnloadContent()
        {
            Content.Unload();
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            // Send special characters
            CheckForKey(keyState, Keys.Right);
            CheckForKey(keyState, Keys.Left);
            CheckForKey(keyState, Keys.Up);
            CheckForKey(keyState, Keys.Down);
            //CheckForKey(keyState, Keys.NumPad0);
            //CheckForKey(keyState, Keys.NumPad1);
            //CheckForKey(keyState, Keys.NumPad2);
            //CheckForKey(keyState, Keys.NumPad3);
            //CheckForKey(keyState, Keys.NumPad4);
            //CheckForKey(keyState, Keys.NumPad5);
            //CheckForKey(keyState, Keys.NumPad6);
            //CheckForKey(keyState, Keys.NumPad7);
            //CheckForKey(keyState, Keys.NumPad8);
            //CheckForKey(keyState, Keys.NumPad9);

            _prevKeyState = keyState;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.Black);

            _vo.DrawGrid(base.GraphicsDevice, _spriteBatch);
            _vo.DrawGame(_spriteBatch, _player.Transform.Position);
            _messageLog.DrawLog(_spriteBatch);

            base.Draw(gameTime);
        }

        private void TextEntered(object sender, TextInputEventArgs e)
        {
            if (_onTextEntered != null)
            {
                _onTextEntered.Invoke(sender, e);
            }
        }
        private void HandleInput(object sender, TextInputEventArgs e)
        {
            int charCode = (int)e.Character.ToString().ToUpper()[0];

            #region CONTROLS

            Point actorPositionOld = _player.Transform.Position;
            Point actorPositionNew;
            switch ((Keys)charCode)
            {
                case (Keys)3:   // [ctrl + c]
                case (Keys)17:  // [ctrl + q]
                case Keys.Escape:
                    {
                        Exit();
                        break;
                    }

                case Keys.D8:
                case Keys.NumPad8:
                case Keys.W:
                case Keys.K:
                case Keys.Up:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.North);
                        break;
                    }

                case Keys.D2:
                case Keys.NumPad2:
                case Keys.S:
                case Keys.J:
                case Keys.Down:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.South);
                        break;
                    }

                case Keys.D4:
                case Keys.NumPad4:
                case Keys.A:
                case Keys.H:
                case Keys.Left:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.West);
                        break;
                    }

                case Keys.D6:
                case Keys.NumPad6:
                case Keys.D:
                case Keys.L:
                case Keys.Right:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.East);
                        break;
                    }

                case Keys.D7:
                case Keys.NumPad7:
                case Keys.Y:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.NorthWest);
                        break;
                    }

                case Keys.D9:
                case Keys.NumPad9:
                case Keys.U:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.NorthEast);
                        break;
                    }

                case Keys.D1:
                case Keys.NumPad1:
                case Keys.B:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.SouthWest);
                        break;
                    }

                case Keys.D3:
                case Keys.NumPad3:
                case Keys.N:
                    {
                        actorPositionNew = _player.Move(CardinalDirection.SouthEast);
                        break;
                    }

                default:
                    {
                        _messageLog.SendMessage(string.Format("Unknown command - [{0}].", (Keys)charCode));
                        break;
                    }
            }

            #endregion
        }
        private void CheckForKey(KeyboardState currentKeyState, Keys key)
        {
            if (currentKeyState.IsKeyDown(key) && _prevKeyState.IsKeyUp(key))
            {
                _onTextEntered.Invoke(this, new TextInputEventArgs((char)key));
            }
        }
    }
}
