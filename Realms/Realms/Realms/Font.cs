using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Font
    {
        public static SpriteFont Normal
        {
            get { return Game1.GameContent.Load<SpriteFont>("Normal"); }
        }

        // other Spritefonts...
    }
}
