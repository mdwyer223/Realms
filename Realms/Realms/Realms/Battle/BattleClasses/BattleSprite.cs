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
        protected List<BattleMessage> messages;

        protected const int MAX_WAIT = 4;
        protected float wait, waitTimer, maxHP, currentHP,
            maxMP, currentMP;

        protected int healthWidth = 100, healthHeight = 30;

        public bool IsDead
        {
            get;
            protected set;
        }

        public Rectangle HealthBar
        {
            get
            {
                healthBar.Height = backHealth.Height = healthHeight;
                backHealth.Width = healthWidth;
                healthBar.Width = (int)(healthWidth * (currentHP / maxHP));
                return healthBar;
            }
        }

        public Rectangle BackHealthBar
        {
            get
            {
                backHealth.Height = healthHeight;
                backHealth.Width = healthWidth;
                return backHealth;
            }
        }

        public float HP
        {
            get { return currentHP; }
        }

        public float MaxHP
        {
            get { return maxHP; }
        }

        public float MP
        {
            get { return currentMP; }
        }

        public float MaxMP
        {
            get { return maxMP; }
        }

        public BattleSprite(Texture2D texture, float scaleFactor, Stats statSheet)
            : base(texture, scaleFactor, 0, Vector2.Zero)
        {
            IsDead = false;

            this.stats = statSheet;
            rand = new Random();
            messages = new List<BattleMessage>();

            maxHP = currentHP = stats.Health;
            maxMP = currentMP = stats.Mana;
        }

        public virtual void update(GameTime gameTime, List<BattleSprite> battleField)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i] != null)
                {
                    messages[i].update(gameTime);
                    if (messages[i].Over)
                        messages.RemoveAt(i);
                }
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            foreach (BattleMessage m in messages)
            {
                if (m != null)
                {
                    m.draw(spriteBatch);
                }
            }
        }

        public virtual void damage(Stats otherStats, Weapon wep)//may need overloads for different spells
        {
            //this sprite is getting the damage dealt to them
            float dodgeChance = ((this.stats.Dodge - (otherStats.Accuracy) * 2) / 10f);
            if (dodgeChance < 0)
                dodgeChance = 0;
            float dodgeRoll = (float)rand.NextDouble() * 10;

            if (dodgeRoll < dodgeChance)
            {
                messages.Add(new BattleMessage("Dodged!", Color.White, this.Position));
                return;
            }
                       
            float totalDamage = ((otherStats.Strength * wep.Damage));
            float critChance = (otherStats.CritChance / 100f);
            float critRoll = (float)rand.NextDouble();

            if (critRoll < critChance)
            {
                totalDamage *= wep.CritDamagePercent;
            }

            float defendedDamge = totalDamage * (stats.Defense / (stats.MaxDefense * 1.25f));
            totalDamage -= defendedDamge;
            messages.Add(new BattleMessage("" + totalDamage, Color.Red, this.Position));
            currentHP -= (int)totalDamage;
            if (currentHP < 0)
            {
                currentHP = 0;
                IsDead = true;
            }
        }

        public virtual void magicDamage(Stats otherStats, Materia materia)
        {
            float dodgeChance = ((this.stats.Dodge - (otherStats.Accuracy) * 2) / 10f);
            if (dodgeChance < 0)
                dodgeChance = 0;
            float dodgeRoll = (float)rand.NextDouble() * 10;

            if (dodgeRoll < dodgeChance)
            {
                messages.Add(new BattleMessage("Dodged!", Color.White, this.Position));
                return;
            }

            float totalDamage = ((otherStats.MagicStrength * materia.SpellDamage));
            float critChance = (otherStats.CritChance / 100f);
            float critRoll = (float)rand.NextDouble();

            //if (critRoll < critChance)
            //{
                //totalDamage *= wep.CritDamagePercent;
            //}
            float defendedDamge = totalDamage * (stats.Defense / (stats.MaxDefense * 1.25f));
            totalDamage -= defendedDamge;
            messages.Add(new BattleMessage("" + totalDamage, Color.Red, this.Position));
            currentHP -= (int)totalDamage;
            if (currentHP < 0)
            {
                currentHP = 0;
                IsDead = true;
            }
        }
    }
}
