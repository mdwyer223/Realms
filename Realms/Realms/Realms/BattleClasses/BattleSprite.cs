using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BattleSprite : AnimatedSprite
    {
        protected Random rand;
        protected Stats stats;

        protected const int MAX_WAIT = 4;
        protected float wait, waitTimer, maxHP, currentHP,
            maxMP, currentMP;

        public BattleSprite(Texture2D texture, float scaleFactor, Stats statSheet)
            : base(texture, scaleFactor, 0, Vector2.Zero)
        {
            this.stats = statSheet;
            rand = new Random();

            maxHP = currentHP = stats.Health;
            maxMP = currentMP = stats.Mana;
        }

        public virtual void damage(Stats otherStats, Weapon wep)//may need overloads for different spells
        {
            //this sprite is getting the damage dealt to them
        }
    }
}
