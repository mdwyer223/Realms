using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class NonControlledCharacter : AdvancedSprite
    {
        // needs to receive information from the server to move
        public NonControlledCharacter(Texture2D texture, float speed, Location startLoc)
            : base(texture, speed, startLoc)
        {

        }
    }
}
