using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Scientist : BaseCharacter
    {
        public Scientist(Texture2D texture, Location startLoc, int level)
            : base(texture, 6, startLoc, level)
        {

        }
    }
}
