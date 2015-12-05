using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.GameObjects.Units;
using Midori.Core.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.GameObjects;

namespace Midori.Core
{
    public static class Engine
    {
        private static List<Tile> tiles = new List<Tile>();
        private static PlayableCharacter player;
        private static List<Unit> enemies = new List<Unit>();
        private static List<GameObject> objects = new List<GameObject>();

        public static List<GameObject> Objects
        {
            get 
            {
                return new List<GameObject>(objects);
            }
        }

        public static List<Tile> Tiles
        {
            get
            {
                return new List<Tile>(tiles);
            }
        }

        public static PlayableCharacter Player
        {
            get { return Engine.player; }
        }

        public static List<Unit> Enemies
        {
            get
            {
                return new List<Unit>(enemies);
            }
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


            //int counter = 1;
            //for (int i = 5; i < 1920; i += 128)
            //{

            //    switch (counter)
            //    {
            //        case 1:
            //            tiles.Add(new Tile(new Vector2(i, 500), 1));
            //            break;
            //        case 2:
            //        case 3:
            //            tiles.Add(new Tile(new Vector2(i, 500), 2));
            //            break;
            //        case 4:
            //            tiles.Add(new Tile(new Vector2(i, 500), 3));
            //            break;
            //    }

            //    counter++;
            //    if (counter == 5)
            //    {
            //        counter = 1;
            //        i += 128 * 2;
            //    }
            //}
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
        }

        
    }
}
