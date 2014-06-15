﻿using System;
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
        public Assassin(Texture2D texture, Location startLoc, int level, int id)
            : base(texture, 3, startLoc, level, id)
        {
            equips.wep = new MasterSword();
            equips.armor = new Platebody();

            equips.wep.equipMateria(new DoubleStrike(5), 0, this);
            equips.armor.equipMateria(new Fire(5), 0, this);

            invent.addItem(new Fire(2));
        }

        public override void update(GameTime gameTime, Grid map)
        {            
            base.update(gameTime, map);
        }
    }
}
