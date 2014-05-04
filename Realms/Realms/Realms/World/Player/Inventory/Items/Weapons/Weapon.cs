using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Weapon : Item
    {
        protected int damage, id, materiaMax;
        protected float critDamagePercent;
        protected List<Materia> materia;
        protected Stats stats;

        public int Damage
        {
            get { return damage; }
        }

        public float CritDamagePercent
        {
            get { return critDamagePercent; }
        }

        public int MaxSlots
        {
            get { return materiaMax; }
        }

        public Stats WepStats
        {
            get { return stats; }
        }

        public Weapon(Texture2D texture, float scaleFactor, string name)
            : base(texture, scaleFactor, name)
        {
            stats = new Stats(null);
            //update from the server
            //stats.increaseStrength(50);
            damage = 10;

            options[0] = "Equip";
        }

        public override void chooseOption(string option, BaseCharacter player)
        {
            switch (option)
            {
                case "Equip":
                    player.equipWeapon(this);
                    break;
            }

            base.chooseOption(option, player);
        }
    }
}
