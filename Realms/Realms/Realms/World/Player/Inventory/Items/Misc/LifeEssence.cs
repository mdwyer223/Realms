using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class LifeEssence : Miscellanious
    {
        public LifeEssence()
            : base(Image.Particle, .02f, "Life Essence", true)
        {
            options.Add("Use");
        }
    }
}
