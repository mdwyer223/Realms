using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Platebody : Armor
    {
        public Platebody()
            : base(Image.Particle, .02f, "Platebody")
        {
            Stats.increaseDefense(5.0f);
        }
    }
}
