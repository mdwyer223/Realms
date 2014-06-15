using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Realms
{
    public class Stick : Weapon
    {
        public Stick()
            : base(Image.Particle, .02f, "Stick")
        {
            damage = 2;
            critDamagePercent = 1.0f;
            this.battleItem = false;
        }
    }
}
