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
        private ActorQueue _actorQueue;
        private MessageLog _messageLog;
        private IMap _currentMap;

        public RogueLikeGame()
        {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
#if OpenGL
            Window.TextInput += TextEntered;
            onTextEntered += HandleInput;
#else
            Window.TextInput += HandleInput;
#endif
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.ApplyChanges();
            IsMouseVisible = true;

            // Load initial game objects
            _currentMap = new Map(MapUtilities.CreateRandomCellCollection(25, 20));
            List<IActor> actors = new List<IActor>()
            {
                new Actor("SCiENiDE", new Glyph("@")),
                new Actor("Kobold", new Glyph("k"))
            };
            _currentMap.LoadActors(actors);

            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);

            // Load resources
            _logFont = Content.Load<SpriteFont>("consolas12");
            _voFont = Content.Load<SpriteFont>("bpmono40bold");

            // Initialize VisualEngine
            _vo = new VisualEngine(32, 16, 11, _currentMap, _voFont);
            _vo.DeltaTileDrawCoordinates = new Point(7, 4);
            _vo.ASCIIScale = 0.6f;

            // Initialize Message Log
            Rectangle messageLogZone = new Rectangle(
                0,
                _vo.MapDrawboxTileSize.Y * _vo.TileSize,
                SCREEN_WIDTH - 30,
                (SCREEN_HEIGHT - 30) - (_vo.MapDrawboxTileSize.Y * _vo.TileSize)); 
            _messageLog = new MessageLog(messageLogZone, _logFont);


        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {


            // TODO: Unload any non ContentManager content here
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
#if OpenGL
            if (keyState.IsKeyDown(Keys.Back) && _prevKeyState.IsKeyUp(Keys.Back))
            {
                onTextEntered.Invoke(this, new TextInputEventArgs('\b'));
            }
            if (keyState.IsKeyDown(Keys.Enter) && _prevKeyState.IsKeyUp(Keys.Enter))
            {
                onTextEntered.Invoke(this, new TextInputEventArgs('\r'));
            }
            // Handle other special characters here (such as tab)
#endif
            _prevKeyState = keyState;

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.Black);

            _vo.DrawGrid(base.GraphicsDevice, _spriteBatch);
            _vo.DrawGame(_spriteBatch, new Point(0, 0));
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
            int charCode = (int)e.Character;
            switch (charCode)
            {
                case (int)Keys.Escape:
                    {
                        Exit();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
