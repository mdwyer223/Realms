using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Cloth : Armor
    {
        public Cloth()
            : base(Image.Particle, 0f, "Cloth")
        {
            Stats.increaseDefense(2.0f);
        }
    }
}
