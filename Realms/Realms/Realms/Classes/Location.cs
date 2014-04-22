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

        public static Location Zero
        {
            get { return new Location(0, 0); }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        public Location(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public override bool Equals(object obj)
        {
            Location oppLoc = (Location)obj;

            if (oppLoc.Row == this.Row && oppLoc.Column == this.Column)
                return true;
            else
                return false;
        }
    }
}
