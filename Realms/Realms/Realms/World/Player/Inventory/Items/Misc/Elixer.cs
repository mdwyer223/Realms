﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Elixer : Miscellanious
    {
        public Elixer()
            : base(Image.Particle, .02f, "Elixer", true)
        {

        }
    }
}