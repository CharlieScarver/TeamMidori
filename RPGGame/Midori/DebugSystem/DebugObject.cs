using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Midori.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Midori.Interfaces;

namespace Midori.DebugSystem
{
    public class DebugObject
    {
        private List<System.Reflection.PropertyInfo> attributes;
        public IGameObject item;
        private int id;

        public DebugObject(IGameObject item)
        {
            this.item = item;
            this.Attributes = new List<System.Reflection.PropertyInfo>(item.GetType().GetProperties());
            this.Id = item.GetHashCode();
        }

        ///<summary>Get the longest line in the debug text so the next anchor can be properly indented</summary>
        public string LongestLine
        {
            get
            {
                int max = 0;
                string longest = "";
                foreach (var item in Attributes)
                {
                    string text = string.Format("{0} = {1}   ({2})", item.Name, item.GetValue(this.item, null), item.GetType().Name);
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

            private set
            {
                attributes = value;
            }
        }

        ///<summary>Returns each property's name, value and type as a string for the item passed in the ctor</summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.item.GetType().Name);
            sb.AppendLine(this.Id.ToString());
            foreach (var item in Attributes)
            {
                sb.AppendLine(string.Format("{0} = {1}   ({2})", item.Name, item.GetValue(this.item, null), item.PropertyType.Name));
            }
            return sb.ToString();
        }

        ///<summary>Returns each property's name and value for the object passed</summary>
        /// <param name="obj">The object passed</param>
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
