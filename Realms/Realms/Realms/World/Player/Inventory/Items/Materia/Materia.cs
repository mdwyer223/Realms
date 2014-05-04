using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Realms
{
    public class Materia : Item
    {
        public Materia(Texture2D texture, float scaleFactor, string name)
            : base(texture, scaleFactor, name)
        {

        }
    }
}
