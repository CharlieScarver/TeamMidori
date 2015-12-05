using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Midori.Core;
using Midori.Core.TextureLoading;
using Midori.DebugSystem;
using Midori.GameObjects;
using Midori.GameObjects.Units;
using System.Collections.Generic;
using System.Linq;

namespace Midori
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlayableCharacter player;
        MidoriDebug debug;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            this.Window.AllowUserResizing = true;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
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

            Engine.InitializeTiles();
            player = Engine.InitializePlayer();
            Engine.InitializeEnemies();
            Engine.InitializeObjects();

            debug = new MidoriDebug(Content, spriteBatch);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureLoader.Load(this.Content);
            
            

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            World.isCollidedWithWorldBounds(graphics, player);

            
            // TODO: Add your update logic here
            player.Update(gameTime);

            foreach (TempEnemy en in Engine.Enemies)
            {
                en.Update(gameTime);
                World.isCollidedWithWorldBounds(graphics, en);               
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(TextureLoader.Background, new Rectangle(0,0,graphics.GraphicsDevice.Viewport.Width,graphics.GraphicsDevice.Viewport.Height), Color.White);

            foreach (Tile tile in Engine.Tiles)
            {
                tile.Draw(spriteBatch);
                tile.DrawBB(spriteBatch, Content, Color.Crimson);
            }
            

            player.Draw(spriteBatch);
            //player.DrawBB(spriteBatch, Content, Color.Orange);

            foreach (TempEnemy en in Engine.Enemies)
            {
                //en.Draw(spriteBatch);
                //en.DrawBB(spriteBatch, Content);
            }

            debug.StatsOnHover();

            spriteBatch.End();
        


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
