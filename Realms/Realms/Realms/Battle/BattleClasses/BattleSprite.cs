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
        protected Rectangle healthBar, backHealth;

        protected const int MAX_WAIT = 4;
        protected float wait, waitTimer, maxHP, currentHP,
            maxMP, currentMP;

        protected int healthWidth = 100, healthHeight = 30;

        public bool IsDead
        {
            get;
            protected set;
        }

        public BattleSprite(Texture2D texture, float scaleFactor, Stats statSheet)
            : base(texture, scaleFactor, 0, Vector2.Zero)
        {
            IsDead = false;

            this.stats = statSheet;
            rand = new Random();

            maxHP = currentHP = stats.Health;
            maxMP = currentMP = stats.Mana;
        }

        public virtual void update(GameTime gameTime, List<BattleSprite> battleField)
        {
            healthBar.X = backHealth.X = (int)this.Position.X;
            healthBar.Y = backHealth.Y = 400;
            healthBar.Height = backHealth.Height = healthHeight;
            backHealth.Width = healthWidth;
            healthBar.Width = (int)(healthWidth * (currentHP / maxHP));
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image.Particle, backHealth, Color.Red);
            spriteBatch.Draw(Image.Particle, healthBar, Color.DarkGreen);
            base.draw(spriteBatch);
        }

        public virtual void damage(Stats otherStats, Weapon wep)//may need overloads for different spells
        {
            //this sprite is getting the damage dealt to them
            float dodgeChance = ((this.stats.Dodge - (otherStats.Accuracy) * 2) / 10f);
            if (dodgeChance < 0)
                dodgeChance = 0;
            float dodgeRoll = (float)rand.NextDouble();

            if (dodgeRoll < dodgeChance)
            {
                return;
            }
                       
            float totalDamage = ((otherStats.Strength * wep.Damage));
            float critChance = (otherStats.CritChance / 100f);
            float critRoll = (float)rand.NextDouble();

            if (critRoll < critChance)
            {
                totalDamage *= wep.CritDamagePercent;
            }

            float defendedDamge = totalDamage * (stats.Defense / (stats.MaxDefense * .75f));
            totalDamage -= defendedDamge;

            currentHP -= (int)totalDamage;
            if (currentHP < 0)
            {
                currentHP = 0;
                IsDead = true;
            }
            
        }
    }
}
