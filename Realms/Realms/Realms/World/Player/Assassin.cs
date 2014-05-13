using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Assassin : BaseCharacter
    {
        public Assassin(Texture2D texture, Location startLoc, int level)
            : base(texture, 5, startLoc, level)
        {
            equips.wep = new MasterSword();
            equips.armor = new Cloth();

            equips.wep.equipMateria(new DoubleStrike(5), 0, this);
            equips.armor.equipMateria(new Fire(5), 0, this);
        }

        public override void update(GameTime gameTime, Grid map)
        {            
            base.update(gameTime, map);
        }
    }
}
