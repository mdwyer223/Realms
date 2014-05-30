using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class GatlingCannon : Materia
    {
        public GatlingCannon(int level)
            : base(Image.Particle, .02f, "Gatling Cannon", level, false, true)
        {
            spellDamage = 100;
            mana = 20;
        }
    }
}
