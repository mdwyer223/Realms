using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Fire : Materia
    {
        public Fire(int level)
            : base(Image.Particle, .02f, "Fire", level, false, false)
        {
            type = SpellType.FIRE;
            maxLevel = 5;
            mana = 50;
            if (level > maxLevel)
                level = maxLevel;

            switch (level)
            {
                case 1:
                    spellDamage = 10;
                    break;
                case 2:
                    spellDamage = 25;
                    break;
                case 3:
                    spellDamage = 50;
                    break;
                case 4:
                    spellDamage = 75;
                    break;
                case 5:
                    spellDamage = 100;
                    break;
                default:
                    spellDamage = 5000;
                    break;
            }

        }
    }
}
