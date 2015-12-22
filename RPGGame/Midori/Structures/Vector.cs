using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Structures
{
    public struct Vector
    {
        private int x;
        private int y;

        public Vector(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
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
    }
}
