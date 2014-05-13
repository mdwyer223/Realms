using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class MasterSword : Weapon
    {
        public MasterSword()
            : base(Image.Particle, .02f, "Master Sword")
        {
            damage = 65;
            critDamagePercent = 1.25f;
        }
    }
}
