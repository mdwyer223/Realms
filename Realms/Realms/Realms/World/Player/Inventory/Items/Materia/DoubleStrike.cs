using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class DoubleStrike : Materia
    {
        float chance;
        const float MAX_CHANCE = .75f;

        public float Chance
        {
            get { return chance; }
        }

        public DoubleStrike(int level)
            : base(Image.Particle, .02f, "Double Strike", level, true, false)
        {
            maxLevel = 5;
            count = 1;
            if (level > maxLevel)
                level = maxLevel;

            if (level == 1)
            {
                chance = .15f;
            }
            else if (level == 2)
            {
                chance = .33f;
            }
            else if (level == 3)
            {
                chance = .45f;
            }
            else if (level == 4)
            {
                chance = .65f;
            }
            else
            {
                chance = .75f;
            }
        }
    }
}
