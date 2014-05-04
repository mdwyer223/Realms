using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class WoodenSword : Weapon
    {
        public WoodenSword()
            : base(null, .03f, "Wooden Sword")
        {
            this.damage = 10;
        }
    }
}
