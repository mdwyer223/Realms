using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public static class Image
    {
        public static Texture2D Particle
        {
            get { return Game1.GameContent.Load<Texture2D>("Particle"); }
        }

        public static Texture2D Cursor
        {
            get { return Game1.GameContent.Load<Texture2D>("Cursor"); }
        }

        // other images...
    }
}
