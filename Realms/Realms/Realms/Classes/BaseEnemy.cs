using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms.Classes
{
    public class BaseEnemy : AdvancedSprite
    {
        int tier;
        EnemyType type;
        Stats stats;

        public Stats Stats
        {
            get
            {
                stats = new Stats(type, tier);
                return stats;
            }
        }

        public BaseEnemy(Texture2D texture, float speed, Location startLoc, EnemyType type, int tier)
            : base(texture, speed, startLoc)
        {
            this.tier = tier;
            this.type = type;
        }
    }
}
