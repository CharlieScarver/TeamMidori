using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Midori.Core;
using Midori.Core.TextureLoading;
using Midori.DebugSystem;
using Midori.GameObjects;
using Midori.GameObjects.Units;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Linq;
using Midori.DebugSystem;

namespace Midori
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> objects;
        PlayableCharacter player;
        Camera2D camera;
        MidoriDebug debug;

        bool isPlayerCollidingWithSomething;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 1050;
            this.Window.AllowUserResizing = true;
            this.IsMouseVisible = true;
            this.objects = new List<GameObject>();
            Content.RootDirectory = "Content";
            //Mouse.WindowHandle = Window.Handle;
        }

        protected override void Initialize()
        {
            isPlayerCollidingWithSomething = false;

            base.Initialize();            
            Engine.InitializeTiles();
            player = Engine.InitializePlayer();
            Engine.InitializeEnemies();
            camera = new Camera2D(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureLoader.Load(this.Content);
            debug = new MidoriDebug(Content, spriteBatch);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var state = Keyboard.GetState();

            World.isCollidedWithWorldBounds(graphics, player);

            player.Update(gameTime);

            foreach (TempEnemy en in Engine.Enemies)
            {
                en.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(TextureLoader.Background, new Rectangle(0,0,graphics.GraphicsDevice.Viewport.Width,graphics.GraphicsDevice.Viewport.Height), Color.White);

            foreach (Tile tile in Engine.Tiles)
            {
                tile.Draw(spriteBatch);
                tile.DrawBB(spriteBatch, Content);
            }

            player.DrawBB(spriteBatch, Content);

            player.Draw(spriteBatch);
            debug.StatsOnHover();

            //foreach (TempEnemy en in Engine.Enemies)
            //{
            //    en.Draw(spriteBatch);
            //}
            //debug.DrawMouseBB();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
