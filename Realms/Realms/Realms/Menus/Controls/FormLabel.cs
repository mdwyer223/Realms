﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class FormLabel : Button
    {
        public FormLabel(Vector2 startPos, float scaleFactor, string text)
            : base(startPos, scaleFactor, text)
        {

        }
    }
}