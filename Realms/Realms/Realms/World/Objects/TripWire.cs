using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class TripWire : BaseObject
    {
        string name;

        public TripWire(Texture2D tex, Location loc, string mapName)
            : base(tex, loc, 1, 1, 1, 1)
        {
            name = mapName;
        }

        public void trigger() //this will most likely get long
        {
            if (name.ToLower().Equals("testdungeon"))
            {
                Grid newG = new TestDungeon(10, 10);
                Game1.changeWorld(newG);//wont need parameters
            }
            else if (name.ToLower().Equals("testgrid"))
            {
                Game1.changeWorld(new TestGrid());
            }
        }
    }
}
