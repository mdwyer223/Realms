using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Assault : BaseCharacter
    {
        public Assault(Texture2D texture, Location startLoc, int level, int id)
            : base(texture, 7, startLoc, level, id)
        {

        }
    }
}
