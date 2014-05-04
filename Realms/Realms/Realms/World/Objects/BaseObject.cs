using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BaseObject : Tile
    {
        public BaseObject(Texture2D tex, Location loc)
            : base(tex, 0, loc)
        {

        }
    }
}
