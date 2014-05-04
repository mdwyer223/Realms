using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Armor : Item
    {
        public Armor(Texture2D texture, float scaleFactor, string name)
            : base(texture, scaleFactor, name)
        {

        }
    }
}
