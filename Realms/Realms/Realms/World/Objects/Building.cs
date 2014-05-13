using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Building : BaseObject
    {
        public Building(Texture2D tex, Location loc, int width, int height, int drawnWidth, int drawnHeight)
            : base(tex, loc, width, height, drawnWidth, drawnHeight)
        {

        }
    }
}
