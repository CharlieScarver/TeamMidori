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
            graphics.PreferredBackBufferWidth = 1900;//1920;
            graphics.PreferredBackBufferHeight = 1000;//1080;
            this.Window.AllowUserResizing = true;
            //this.IsMouseVisible = true;
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

            base.Initialize();            

            Engine.InitializeTiles();
            player = Engine.InitializePlayer();
            Engine.InitializeEnemies();
            Engine.InitializeObjects();
            Engine.InitializeUpdatableObjects();

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
            spriteBatch.Draw(TextureLoader.Background, new Rectangle(0,0,graphics.GraphicsDevice.Viewport.Width,graphics.GraphicsDevice.Viewport.Height), Color.White);

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

            debug.StatsOnHover();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
