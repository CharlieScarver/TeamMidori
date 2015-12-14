using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core.TextureLoading;
using Midori.GameObjects;
using Midori.GameObjects.Units;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;
using Midori.GameObjects.Projectiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Midori.GameObjects.Items;
using Midori.GameObjects.Units.Enemies;
using Midori.GameObjects.Tiles;
using IDrawable = Midori.Interfaces.IDrawable;


namespace Midori.Core
{
    public static class Engine
    {
        private static List<Tile> tiles = new List<Tile>();
        private static PlayableCharacter player;
        private static List<Unit> enemies = new List<Unit>();
        private static List<Projectile> projectiles = new List<Projectile>();
        private static List<Item> items = new List<Item>();

        private static List<GameObject> objects = new List<GameObject>();
        private static List<Interfaces.IUpdatable> updatableObjects = new List<IUpdatable>();
        private static List<Interfaces.IDrawable> drawableObjects = new List<Interfaces.IDrawable>();
        
        public static Rectangle LevelBounds { get; set; }

        public static IEnumerable<Item> Items
        {
            get { return Engine.items; }
        } 

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
            items.Add(new Item(TextureLoader.Box, new Vector2(600, 100), ItemTypes.Heal));
            items.Add(new Item(TextureLoader.Box, new Vector2(780, 100), ItemTypes.Heal));
        }

        public static void InitializeTiles()
        {
            // lowest
            //tiles.Add(new GroundTile(new Vector2(128 * 0, 900), 1));
            //tiles.Add(new GroundTile(new Vector2(128 * 1, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 2, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 3, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 4, 900), 2));
            ////----
            //tiles.Add(new GroundTile(new Vector2(128 * 5, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 6, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 7, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 8, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 9, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 10, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 11, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 12, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 13, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 14, 900), 3));


            //tiles.Add(new GroundTile(new Vector2(128 * 10, 900), 1));
            //tiles.Add(new GroundTile(new Vector2(128 * 11, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 12, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 13, 900), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 14, 900), 3));

            //// middle
            //tiles.Add(new GroundTile(new Vector2(128 * 6, 550), 1));
            //tiles.Add(new GroundTile(new Vector2(128 * 7, 550), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 8, 550), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 9, 550), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 10, 550), 3));

            //// highest
            //tiles.Add(new GroundTile(new Vector2(128 * 1, 200), 1));
            //tiles.Add(new GroundTile(new Vector2(128 * 2, 200), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 3, 200), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 4, 200), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 7, 200), 3));
            //tiles.Add(new GroundTile(new Vector2(128 * 5, 200), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 6, 200), 2));

            //tiles.Add(new GroundTile(new Vector2(128 * 10, 200), 1));
            //tiles.Add(new GroundTile(new Vector2(128 * 11, 200), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 12, 200), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 13, 200), 2));
            //tiles.Add(new GroundTile(new Vector2(128 * 14, 200), 3));
                       
        }


        public static PlayableCharacter InitializePlayer()
        {
            //player = new TempHero(new Vector2(720, 200));
            return player;
        }

        

        public static void InitializeEnemies()
        {
            //enemies.Add(new Bush(new Vector2(1700, 600)));
            enemies.Add(new Ghost(new Vector2(1000, 600)));
            enemies.Add(new Ghost(new Vector2(120, 30)));
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

        public static void ChangeLevel(string level)
        {
            tiles = new List<Tile>();
            InitializeLevel(level);
        }

        public static void InitializeLevel(string level)
        {
            using (StreamReader reader = new StreamReader("Content/Levels/" + level + ".txt"))
            {
                string line = "";
                int lineCount = 0;
                string[] levelBounds = reader.ReadLine().Split(',');
                Engine.LevelBounds = new Rectangle(0, 0, int.Parse(levelBounds[0])*128, int.Parse(levelBounds[1])*128);
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == 's')
                        {
                            player = new Aya(new Vector2(i*128, lineCount*128));
                        }
                        else if (line[i] == '6' || line[i] == '7' || line[i] == 'A')
                        {
                            tiles.Add(new WallTile(new Vector2(i*128, lineCount*128), line[i].ToString()));
                        }                        
                        else if (line[i] != '0')
                        {
                            tiles.Add(new GroundTile(new Vector2(i*128, lineCount*128), line[i].ToString()));
                        }
                    }
                    lineCount++;
                    System.Diagnostics.Debug.WriteLine(line);
                }
            }
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
                    //item.Nullify();
                    objects.Remove(item);
                    if (projectiles.Contains(item))
                    {
                        projectiles.Remove((Projectile)item);
                    }
                    else if (enemies.Contains(item))
                    {
                        enemies.Remove((Enemy)item);
                    }
                    else if (item is PlayableCharacter)
                    {
                        player.Nullify();
                    }
                }
            }
        }

        
    }
}
