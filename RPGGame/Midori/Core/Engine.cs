﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.GameObjects.Units;
using Midori.Core.TextureLoading;
using Midori.GameObjects;
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
        private static List<TempEnemy> enemies = new List<TempEnemy>();
        private static List<GameObject> objects = new List<GameObject>();

        public static PlayableCharacter Player
        {
            get { return player; }
        }

        public static List<GameObject> Objects
        {
            get
            {
                var list = new List<GameObject>();
                list.AddRange(enemies);
                list.AddRange(tiles);
                return list;
            }
        }

        public static List<Tile> Tiles 
        { 
            get 
            { 
                return new List<Tile>(tiles); 
            } 
        }

        public static void InitializeTiles()
        {
            Random r = new Random(); //r.Next(1,4)
            int counter = 1;
            for (int i = 10; i < 1920; i += 128)
            {

                switch (counter)
                {
                    case 1:
                        tiles.Add(new Tile(new Vector2(i, 700), 1));
                        break;
                    case 2:
                    case 3:
                        tiles.Add(new Tile(new Vector2(i, 700), 2));
                        break;
                    case 4:
                        tiles.Add(new Tile(new Vector2(i, 700), 3));
                        tiles.Add(new Tile(new Vector2(i, 650), 3));

                        break;
                }

                counter++;
                if (counter == 5)
                {
                    counter = 1;
                    i += 128;
                }
            }
                
            
        }


        public static PlayableCharacter InitializePlayer()
        {
            player = new TempHero(new Vector2(720, 300));
            return player;
        }

        public static List<TempEnemy> Enemies
        {
            get
            {
                return new List<TempEnemy>(enemies);
            }
        }

        public static void InitializeEnemies()
        {
            enemies.Add(new TempEnemy(new Vector2(200, 300)));
            enemies.Add(new TempEnemy(new Vector2(850, 300)));
        }

        
    }
}