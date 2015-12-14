using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Midori.Core;
using Midori.Core.TextureLoading;
using Midori.DebugSystem;
using Midori.GameObjects;
using Midori.GameObjects.Projectiles;
using Midori.GameObjects.Units;
using Midori.GameObjects.Units.Enemies;
using Midori.GameObjects.Units.PlayableCharacters;
using System.Collections.Generic;
using System.Linq;
using Midori.GameObjects.Items;

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
        private Camera2D camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
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
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();
            //Engine.LevelBounds = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width * 2, graphics.GraphicsDevice.Viewport.Height);

            base.Initialize();
            
            Engine.InitializeTiles();
            Engine.InitializeLevel("Level1");
            player = Engine.InitializePlayer();
            Engine.InitializeEnemies();
            Engine.InitializeObjects();
            Engine.InitializeUpdatableObjects();
            Engine.InitializeItems();
            camera = new Camera2D(graphics.GraphicsDevice);
            camera.SetSceneBounds(new Rectangle(50, 50, Engine.LevelBounds.Width - 200, Engine.LevelBounds.Height));
            camera.SetChaseTarget(player);
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || !player.IsActive)
                Exit();

            foreach (Interfaces.IUpdatable item in Engine.UpdatableObjects)
            {
                item.Update(gameTime);
            }

            foreach (Projectile projectile in Engine.Projectiles)
            {
                projectile.Update(gameTime);
                foreach (Unit unit in Engine.UpdatableObjects)
                {
                    if (World.CheckForCollisionBetween(projectile, unit))
                    {
                        if (projectile.Owner is PlayableCharacter && unit is Enemy)
                        {
                            unit.GetHitByProjectile(projectile.Owner);
                            projectile.Nullify();
                        }
                        if (projectile.Owner is Enemy && unit is PlayableCharacter)
                        {
                            unit.GetHitByProjectile(projectile.Owner);
                            projectile.Nullify();
                        }
                    }
                }
            }

            foreach (var item in Engine.Items)
            {
                if (item.IsActive)
                {
                    item.Update(gameTime);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                Engine.ChangeLevel("Level2");
                camera.SetSceneBounds(new Rectangle(50, 50, Engine.LevelBounds.Width - 200, Engine.LevelBounds.Height));
            }

            camera.Chase(gameTime);
            camera.Update(gameTime);
            Engine.CleanInactiveObjects();

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
            spriteBatch.Draw(TextureLoader.Background, new Rectangle(0, 0, 1920, 1080), Color.White);
            debug.MouseStats();

            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: camera.Transform);

            foreach (Tile tile in Engine.Tiles)
            {
                tile.Draw(spriteBatch);
                //tile.DrawBB(spriteBatch, Color.Crimson);
            }
            

            player.Draw(spriteBatch);
            //player.DrawBB(spriteBatch, Color.Orange);

            foreach (Enemy en in Engine.Enemies)
            {
                en.Draw(spriteBatch);
                //en.DrawBB(spriteBatch, Color.LightGreen);
            }

            foreach (Projectile proj in Engine.Projectiles)
            {
                proj.Draw(spriteBatch);
                //proj.DrawBB(spriteBatch, Color.Aqua);
            }

            foreach (Item item in Engine.Items)
            {
                if (item.IsActive)
                {
                    item.Draw(spriteBatch);
                }
            }
            debug.SetCameraPosition(camera.Position);
            debug.StatsOnHover();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
