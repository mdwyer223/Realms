using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Ultima : Weapon
    {
        public Ultima()
            : base(null, 0.0f, "Ultima")
        {
            damage = 9999;
            critDamagePercent = 2.5f;
            stats.increaseCritChance(75);
            stats.increaseStrength(125);
            stats.increaseSpeed(75);
        }
    }
}
