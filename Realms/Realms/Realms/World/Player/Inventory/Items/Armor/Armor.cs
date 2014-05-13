using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Armor : Item
    {
        List<Materia> materia;
        Stats stats;
        int maxSlots;

        public List<Materia> MateriaList
        {
            get { return materia; }
        }

        public Stats Stats
        {
            get { return stats; }
        }

        public int MaxSlots
        {
            get { return maxSlots; }
        }

        public Armor(Texture2D texture, float scaleFactor, string name)
            : base(texture, scaleFactor, name)
        {
            materia = new List<Materia>();
            stats = new Stats(null);
            maxSlots = 1;
            //determine stats using increasing methods
        }

        public void equipMateria(Materia newMateria, int slot, BaseCharacter c)
        {
            if (materia.Count > MaxSlots)
                return;

            unequipMateria(slot, c);
            materia.Add(newMateria);
        }

        public void unequipMateria(int index, BaseCharacter character)
        {
            if (materia.Count == 0)
                return;

            character.Inventory.addItem(materia[index]);
            materia.RemoveAt(index);
        }
    }
}
