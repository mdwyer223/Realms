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

        public List<Materia> MateriaList
        {
            get { return materia; }
        }

        public Weapon(Texture2D texture, float scaleFactor, string name)
            : base(texture, scaleFactor, name, false)
        {
            stats = new Stats(null);
            materia = new List<Materia>();
            //update from the server
            //stats.increaseStrength(50);
            damage = 1;
            critDamagePercent = 1.0f;

            capacity = 1;

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

        public void equipMateria(Materia newMateria, int slot, BaseCharacter c)
        {
            if (materia.Count > MaxSlots)
                return;

            unequipMateria(slot, c);
            materia.Add(newMateria);
            c.Inventory.ItemList.Remove(newMateria);
            c.Inventory.cleanUp();
        }

        public void unequipMateria(int index, BaseCharacter character)
        {
            if (materia.Count == 0 || index + 1 > materia.Count)
                return;

            character.Inventory.addItem(materia[index]);
            materia.RemoveAt(index);
        }

        public override string ToString()
        {
            return name + "\nDmg: " + damage + "\nCritDmg: " + critDamagePercent;
        }
    }
}
