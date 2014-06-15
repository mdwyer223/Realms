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
        protected SpellType type;
        protected Stats stats;
        protected int spellDamage, level, maxLevel, ap, mana;
        bool passive, summon;
        string skillName;

        public int SpellDamage
        {
            get { return spellDamage; }
        }

        public int Level
        {
            get { return level; }
        }

        public int MaxLevel
        {
            get { return maxLevel; }
        }

        public int ManaCost
        {
            get { return mana; }
        }

        public bool Passive
        {
            get { return passive; }
        }

        public bool Summon
        {
            get { return summon; }
        }

        public string Skill
        {
            get { return skillName; }
        }

        public SpellType Type
        {
            get { return type; }
        }

        public Stats Stats
        {
            get { return stats; }
        }

        public Materia(Texture2D texture, float scaleFactor, string name, int level, bool passive, bool summon)
            : base(texture, scaleFactor, name, false)
        {
            count = 1;
            stats = new Stats(null);
            skillName = name;
            spellDamage = 1;
            mana = 1;
            type = SpellType.NONE;
            this.level = level;
            maxLevel = 1;
            this.passive = passive;
            this.summon = summon;
        }
    }
}
