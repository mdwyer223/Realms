using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Realms
{
    public class Location
    {
        Vector2 position;
        int row, column;

        public Vector2 Position
        {
            get { return position; }
        }

        public int Row
        {
            get { return row; }
        }

        public int Column
        {
            get { return column; }
        }

        public Location(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
}
