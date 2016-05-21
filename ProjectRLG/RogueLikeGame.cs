namespace ProjectRLG
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using ProjectRLG.Contracts;
    using ProjectRLG.Infrastructure;
    using ProjectRLG.Models;
    using ProjectRLG.Utilities;
    using System;

    public class RogueLikeGame : Game
    {
        private const int ScreenWidth = 800;
        private const int ScreenHeight = 500;

        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        private EventHandler<TextInputEventArgs> _onTextEntered;
        private KeyboardState _prevKeyState;
        private SpriteFont _logFont;
        private SpriteFont _voFont;
        private VisualEngine _vo;

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
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
            IsMouseVisible = true;

            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load resources
            _logFont = Content.Load<SpriteFont>("consolas12");
            _voFont = Content.Load<SpriteFont>("bpmono40bold");

            // Load initial game objects
            IMap testMap = new Map(MapUtils.CreateRandomCellCollection(25, 20));
            _vo = new VisualEngine(32, 16, 11, testMap, _voFont);
            _vo.DeltaTileDrawCoordinates = new Point(4, -6);
            _vo.ASCIIScale = 0.6f;
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

            _vo.DrawGrid(_graphics.GraphicsDevice, _spriteBatch);
            _vo.DrawGame(_spriteBatch, new Point(5, 5));

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
