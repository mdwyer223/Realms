using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BaseEnemy : AdvancedSprite
    {
        int tier;
        EnemyType type;
        Stats stats;
        Random rand;

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
            rand = new Random();
        }

        public void battled()
        {
            IsDead = true;
            IsVisible = false;
        }

        public List<BattleEnemy> spawnEnemies()
        {
            //depends on zone and everything 
            int numEnemies = rand.Next(1, 3);

            List<BattleEnemy> enemies = new List<BattleEnemy>();
            for (int i = 0; i < numEnemies; i++)
            {
                BaseEnemy e = new BaseEnemy(Image.Particle, 0, Location.Zero, type, tier);
                enemies.Add(new BattleEnemy(e.texture, .07f, e));
            }

            return enemies;
        }
    }
}
