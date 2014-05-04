using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Assassin : BaseCharacter
    {
        public Assassin(Texture2D texture, float secondsToCrossScreen, Location startLoc, int level)
            : base(texture, secondsToCrossScreen, startLoc, level)
        {
            equips.wep = new WoodenSword();
        }
    }
}
