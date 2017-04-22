using System.Linq;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Key = OpenTK.Input.Key;

namespace GameMagic
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GMGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private World world;
        private KeyboardState newKeyState;
        private KeyboardState oldKeyState;
        public static Effect lightEffect;
        public static Effect post1;
        public static Effect vectorEffect;
        RenderTarget2D rt;

        public int Width => graphics.GraphicsDevice.Viewport.Width;
        public int Height => graphics.GraphicsDevice.Viewport.Height;

        public static bool DebugOverlay = false;

        public GMGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            world = new MainWorld(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            StaticImg.asprite = Content.Load<Texture2D>("img/asprite");

            lightEffect = Content.Load<Effect>("fx/Light");
            post1 = Content.Load<Effect>("fx/Post1");
            vectorEffect = Content.Load<Effect>("fx/Vector");

            rt = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, true, graphics.PreferredBackBufferFormat, graphics.PreferredDepthStencilFormat);

            world.Init();
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

            newKeyState = Keyboard.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (KeyPressed(Keys.F3))
            {
                DebugOverlay = !DebugOverlay;
            }

            world.Update(gameTime);

            oldKeyState = newKeyState;
            base.Update(gameTime);
        }

        private bool KeyPressed(Keys k)
        {
            return newKeyState.IsKeyDown(k) && oldKeyState.IsKeyUp(k);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(rt);
            GraphicsDevice.Clear(Color.Black);

            //lightEffect.Parameters["res"].SetValue(new Vector2(Width, Height));
            lightEffect.Parameters["res"].SetValue(Vector2.One);
            //  lightEffect.Parameters["time"].SetValue(gameTime.TotalGameTime.Milliseconds/1000.0f);

            //spriteBatch.Begin(SpriteSortMode.Deferred, 
            //    BlendState.NonPremultiplied, 
            //    SamplerState.PointClamp,DepthStencilState.Default, 
            //    RasterizerState.CullNone, 
            //    lightEffect);
            world.Draw(gameTime, spriteBatch);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(0, null, null, null, null, post1);
            spriteBatch.Draw(rt, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private static Texture2D rect;

        public void DrawRectangle(Rectangle coords, Color color)
        {
            if (rect == null)
            {
                rect = new Texture2D(GraphicsDevice, 1, 1);
                rect.SetData(new[] { Color.White });
            }
            spriteBatch.Draw(rect, coords, color);
        }
    }
}
