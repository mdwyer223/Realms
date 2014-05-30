using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Engineer : BaseCharacter
    {
        public Engineer(Texture2D texture, Location startLoc, int level, int id)
            : base(texture, 6, startLoc, level, id)
        {
            equips.armor = new Cloth();
            equips.wep = new MasterSword();

            equips.armor.equipMateria(new GatlingCannon(1), 0, this);
        }
    }
}
