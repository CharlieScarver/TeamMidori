﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Midori.GameObjects;

namespace Midori.DebugSystem
{
    public class DebugObject
    {
        private List<System.Reflection.PropertyInfo> attributes;
        public GameObject item;
        private int id;

        public DebugObject(GameObject item)
        {
            this.item = item;
            this.Attributes = new List<System.Reflection.PropertyInfo>(item.GetType().GetProperties());
            this.Id = item.GetHashCode();
        }

        public string LongestLine
        {
            get
            {
                int max = 0;
                string longest = "";
                foreach (var item in Attributes)
                {
                    string text = string.Format("{0} = {1}", item.Name, item.GetValue(this.item, null));
                    if (text.Length > max)
                    {
                        max = text.Length;
                        longest = text;
                    }
                }
                return longest;
            }
        }

        public int Id
        {
            get { return this.id; }
            private set { this.id = value; }
        }

        public Vector2 Position { get; set; }

        public List<System.Reflection.PropertyInfo> Attributes
        {
            get
            {
                return attributes;
            }

            set
            {
                attributes = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.item.GetType().Name);
            sb.AppendLine(this.Id.ToString());
            foreach (var item in Attributes)
            {
                sb.AppendLine(string.Format("{0} = {1}", item.Name, item.GetValue(this.item, null)));
            }
            return sb.ToString();
        }

        public static string toString(object obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(obj.GetType().Name);
            sb.AppendLine(obj.ToString());
            foreach (var prop in obj.GetType().GetProperties())
            {
                sb.AppendLine(string.Format("{0} = {1}", prop.Name, prop.GetValue(obj, null)));
            }
            return sb.ToString();
        }
    }
}
