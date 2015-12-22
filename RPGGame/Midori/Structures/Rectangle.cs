using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Structures
{
    public struct Rectangle
    {
        private int x;        
        private int y;        
        private int width;        
        private int height;        

        public Rectangle(int x, int y, int width, int height) : this()
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public Rectangle(Vector position, int width, int height) : this()
        {
            this.X = position.X;
            this.Y = position.Y;
            this.Width = width;
            this.Height = height;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

    }
}
