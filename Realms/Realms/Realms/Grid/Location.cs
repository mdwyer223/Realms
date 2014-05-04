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

        public static Location Zero
        {
            get { return new Location(0, 0); }
        }

        public int Row
        {
            get;
            set;
        }

        public int Column
        {
            get;
            set;
        }

        public Location(int row, int column)
        {
            if(row >= 0)
                this.Row = row;

            if(column >= 0)
                this.Column = column;
        }

        public override bool Equals(object obj)
        {
            Location oppLoc = (Location)obj;
            return oppLoc.Row == this.Row && oppLoc.Column == this.Column;            
        }
    }
}
