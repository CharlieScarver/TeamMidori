using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.TextureLoading;
using Midori.GameObjects;
using Midori.GameObjects.Units;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;
using Midori.GameObjects.Projectiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Midori.GameObjects.Items;
using Midori.GameObjects.Units.Enemies;
using Midori.GameObjects.Tiles;
using IDrawable = Midori.Interfaces.IDrawable;
using Midori.Enumerations;


namespace Midori.Core
{
    public static class Engine
    {
        private static List<ITile> tiles = new List<ITile>();
        private static PlayableCharacter player;
        private static List<IEnemy> enemies = new List<IEnemy>();
        private static List<IProjectile> projectiles = new List<IProjectile>();
        private static List<Item> items = new List<Item>();

        private static List<IGameObject> objects = new List<IGameObject>();
        private static List<Interfaces.IUpdatable> updatableObjects = new List<IUpdatable>();
        private static List<Interfaces.IDrawable> drawableObjects = new List<Interfaces.IDrawable>();
        private static List<TimedBonusItem> playerTimedBonuses = new List<TimedBonusItem>();

        public static List<TimedBonusItem> PlayerTimedBonuses
        {
            get { return Engine.playerTimedBonuses; }
        } 
        public static Rectangle LevelBounds { get; set; }

        public static IEnumerable<Item> Items
        {
            get { return Engine.items; }
        } 

        public static IEnumerable<ITile> Tiles
        {
            get { return Engine.tiles; }
        }

        public static PlayableCharacter Player
        {
            get { return Engine.player; }
        }
        
        public static IEnumerable<IEnemy> Enemies
        {
            get { return Engine.enemies; }
        }

        public static IEnumerable<IProjectile> Projectiles 
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

        public static IEnumerable<IGameObject> Objects
        {
            get { return Engine.objects; }
        }

        public static void InitializeItems()
        {
            items.Add(new HealingItem(new Vector2(2000, 300)));
            items.Add(new AttackBonusItem(new Vector2(3000, 300)));
            items.Add(new MoveBonusItem(new Vector2(4000, 300)));
        }


        public static PlayableCharacter InitializePlayer()
        {
            //player = new TempHero(new Vector2(720, 200));
            return player;
        }

        public static void SpawnItem(Vector2 position)
        {
            Random rand = new Random();
            ItemType type = (ItemType)rand.Next(3);
            switch (type)
            {
                case ItemType.Heal:
                    items.Add(new HealingItem(position));
                    break;
                case ItemType.MoveBonus:
                    items.Add(new MoveBonusItem(position));
                    break;
                case ItemType.AttackBonus:
                    items.Add(new AttackBonusItem(position));
                    break;
            }
        }

        public static void InitializeEnemies()
        {
            //enemies.Add(new Bush(new Vector2(1700, 600)));
            //enemies.Add(new Ghost(new Vector2(1000, 600)));
            //enemies.Add(new Ghost(new Vector2(120, 30)));
        }

        public static void InitializeObjects()
        {
            objects.Add(player);
            objects.AddRange(tiles);
            objects.AddRange(enemies);
            objects.AddRange(Engine.Projectiles);
        }

        public static void InitializeUpdatableObjects()
        {
            updatableObjects.Add(player);
            updatableObjects.AddRange(enemies);
        }

        public static void ChangeLevel(string level)
        {
            tiles = new List<ITile>();
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
                        switch (line[i])
                        {
                            // Player and Enemies
                            case 's':
                                player = new Midori.GameObjects.Units.PlayableCharacters.Midori(new Vector2(i * 128, lineCount * 128));
                                break;
                            case 'g':
                                enemies.Add(new Ghost(new Vector2(i * 128, lineCount * 128)));
                                break;
                            case 'b':
                                enemies.Add(new Bush(new Vector2(i * 128, lineCount * 128)));
                                break;
                            // Tiles                               
                            case '!':
                                tiles.Add(new WallTile(new Vector2(i * 128, lineCount * 128), TileType.LeftWallTile));
                                break;
                            case 'i':
                                tiles.Add(new WallTile(new Vector2(i * 128, lineCount * 128), TileType.RightWallTile));
                                break;
                            case '\'':
                                tiles.Add(new InnerGroundTile(new Vector2(i * 128, lineCount * 128)));
                                break;                            
                            case '(':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.StartPlatformTile));
                                break;
                            case '_':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.MiddlePlatformTile));
                                break;
                            case ')':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.EndPlatformTile));
                                break;
                            case '-':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.MiddleGroundTile));
                                break;
                            case '[':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.StartGroundTile));
                                break;
                            case ']':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.EndGroundTile));
                                break;
                            case '}':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.LeftCornerGroundTile));
                                break;
                            case '{':
                                tiles.Add(new GroundTile(new Vector2(i * 128, lineCount * 128), TileType.RightCornerGroundTile));
                                break;
                        }
                    }
                    lineCount++;
                }
            }
        }

        public static void InitializeDrawableObjects()
        {
            objects.AddRange(tiles);
            drawableObjects.Add(player);
            drawableObjects.AddRange(enemies);
        }

        public static void AddProjectile(IProjectile proj)
        {
            projectiles.Add(proj);
            objects.Add(proj);
        }


        public static void CleanInactiveObjects()
        {
            var temp = new List<IGameObject>(objects);
            foreach (var item in temp)
            {
                if (!item.IsActive)
                {
                    //item.Nullify();
                    objects.Remove(item);
                    if (projectiles.Contains(item))
                    {
                        projectiles.Remove((IProjectile)item);
                    }
                    else if (enemies.Contains(item))
                    {
                        enemies.Remove((Enemy)item);
                        Engine.updatableObjects.Remove((IUpdatable)item);
                    }
                    else if (item is PlayableCharacter)
                    {
                        player.Nullify();
                        Engine.updatableObjects.Remove((IUpdatable)item);
                    }
                }
            }
        }

        
    }
}
