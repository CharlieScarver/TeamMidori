using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core.TextureLoading;
using Midori.GameObjects;
using Midori.GameObjects.Units;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;
using Midori.GameObjects.Items;
using Midori.GameObjects.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Midori.Core
{
    public static class Engine
    {
        private static List<Tile> tiles = new List<Tile>();
        private static PlayableCharacter player;
        private static List<Unit> enemies = new List<Unit>();
        private static List<Projectile> projectiles = new List<Projectile>();

        private static List<GameObject> objects = new List<GameObject>();
        private static List<Interfaces.IUpdatable> updatableObjects = new List<IUpdatable>();
        private static List<Item> items = new List<Item>();

        public static IEnumerable<Item> Items
        {
            get { return Engine.items; }
        }

        private static List<Interfaces.IDrawable> drawableObjects = new List<Interfaces.IDrawable>();

        public static IEnumerable<Tile> Tiles
        {
            get { return Engine.tiles; }
        }

        public static PlayableCharacter Player
        {
            get { return Engine.player; }
        }
        
        public static IEnumerable<Unit> Enemies
        {
            get { return Engine.enemies; }
        }

        public static IEnumerable<Projectile> Projectiles 
        {
            get { return Engine.projectiles;  } 
        }

        public static IEnumerable<IUpdatable> UpdatableObjects
        {
            get { return Engine.updatableObjects; }
        }

        public static IEnumerable<Interfaces.IDrawable> DrawableObjects
        {
            get { return Engine.drawableObjects; }
        }

        public static IEnumerable<GameObject> Objects
        {
            get { return Engine.objects; }
        }

        public static void InitializeItems()
        {
            items.Add(new Item(TextureLoader.TheOnePixel, new Vector2(130, 900), ItemTypes.Heal));
            items.Add(new Item(TextureLoader.TheOnePixel, new Vector2(130*2, 900), ItemTypes.Heal));

        }

        public static void InitializeTiles()
        {
            // lowest
            tiles.Add(new Tile(new Vector2(128 * 0, 900), 1));
            tiles.Add(new Tile(new Vector2(128 * 1, 900), 2));
            tiles.Add(new Tile(new Vector2(128 * 2, 900), 2));
            tiles.Add(new Tile(new Vector2(128 * 3, 900), 2));
            tiles.Add(new Tile(new Vector2(128 * 4, 900), 3));

            tiles.Add(new Tile(new Vector2(128 * 10, 900), 1));
            tiles.Add(new Tile(new Vector2(128 * 11, 900), 2));
            tiles.Add(new Tile(new Vector2(128 * 12, 900), 2));
            tiles.Add(new Tile(new Vector2(128 * 13, 900), 2));
            tiles.Add(new Tile(new Vector2(128 * 14, 900), 3));

            // middle
            tiles.Add(new Tile(new Vector2(128 * 6, 550), 1));
            tiles.Add(new Tile(new Vector2(128 * 7, 550), 2));
            tiles.Add(new Tile(new Vector2(128 * 8, 550), 2));
            tiles.Add(new Tile(new Vector2(128 * 9, 550), 2));
            tiles.Add(new Tile(new Vector2(128 * 10, 550), 3));

            // highest
            tiles.Add(new Tile(new Vector2(128 * 1, 200), 1));
            tiles.Add(new Tile(new Vector2(128 * 2, 200), 2));
            tiles.Add(new Tile(new Vector2(128 * 3, 200), 2));
            tiles.Add(new Tile(new Vector2(128 * 4, 200), 2));
            tiles.Add(new Tile(new Vector2(128 * 7, 200), 3));
            tiles.Add(new Tile(new Vector2(128 * 5, 200), 2));
            tiles.Add(new Tile(new Vector2(128 * 6, 200), 2));

            tiles.Add(new Tile(new Vector2(128 * 10, 200), 1));
            tiles.Add(new Tile(new Vector2(128 * 11, 200), 2));
            tiles.Add(new Tile(new Vector2(128 * 12, 200), 2));
            tiles.Add(new Tile(new Vector2(128 * 13, 200), 2));
            tiles.Add(new Tile(new Vector2(128 * 14, 200), 3));
        }


        public static PlayableCharacter InitializePlayer()
        {
            //player = new TempHero(new Vector2(720, 200));
            player = new Aya(new Vector2(720, 59));
            return player;
        }

        

        public static void InitializeEnemies()
        {
            //enemies.Add(new TempEnemy(new Vector2(200, 150)));
            //enemies.Add(new TempEnemy(new Vector2(850, 150)));
        }

        public static void InitializeObjects()
        {
            objects.Add(player);
            objects.AddRange(tiles);
            objects.AddRange(enemies);
            objects.AddRange(projectiles);
        }

        public static void InitializeUpdatableObjects()
        {
            updatableObjects.Add(player);
            updatableObjects.AddRange(enemies);
        }

        public static void InitializeDrawableObjects()
        {
            objects.AddRange(tiles);
            drawableObjects.Add(player);
            drawableObjects.AddRange(enemies);
        }

        public static void AddProjectile(Projectile proj)
        {
            projectiles.Add(proj);
            objects.Add(proj);
        }

        public static void CleanInactiveObjects()
        {
            var temp = new List<GameObject>(objects);
            foreach (var item in temp)
            {
                if (!item.IsActive)
                {
                    objects.Remove(item);
                    if (projectiles.Contains(item))
                    {
                        projectiles.Remove((Projectile)item);
                    }
                }
            }
        }

        
    }
}
