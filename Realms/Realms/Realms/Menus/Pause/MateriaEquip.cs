using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class MateriaEquip
    {
        List<Materia> materia;
        List<Materia> materiaOnItem;

        List<DisplayItem> invent, itemDisplayMateria;

        PauseEquipment pe;
        Weapon w;
        Armor a;

        BaseCharacter bc;

        int currentPage = 1, pages;

        public MateriaEquip(Armor a, BaseCharacter bc, PauseEquipment pe)
        {
            this.a = a;
            this.bc = bc;
            this.pe = pe;
            init();
        }

        public MateriaEquip(Weapon w, BaseCharacter bc, PauseEquipment pe)
        {
            this.w = w;
            this.bc = bc;
            this.pe = pe;
            init();
        }

        private void init()
        {
            invent = new List<DisplayItem>();
            materiaOnItem = new List<Materia>();

            if (w != null)
            {
                this.materiaOnItem = w.MateriaList;
            }
            else if(a != null)
                this.materiaOnItem = a.MateriaList;

            getMateria();
        }

        public void update(GameTime gameTime)
        {
            int selectorIndex = 0;
            bool picked = false;
                for (int i = 0; i < invent.Count; i++)
                {
                    if (invent[i] != null)
                    {
                        if (Input.mouseDrawnRec().Intersects(invent[i].Rec))
                        {
                            selectorIndex = i;
                            if (Input.doubleClick())
                            {
                                picked = true;
                            }
                        }
                    }
                }

                if (picked)
                {
                    equipMateria(selectorIndex);
                }

                picked = false;
                for (int i = 0; i < this.itemDisplayMateria.Count; i++)
                {
                    if (itemDisplayMateria[i] != null)
                    {
                        if (Input.mouseDrawnRec().Intersects(itemDisplayMateria[i].Rec))
                        {
                            selectorIndex = i;
                            if (Input.rightMouseClick())
                            {
                                picked = true;
                            }
                        }
                    }
                }

                if (picked)
                {
                    unequipMateria(selectorIndex);
                }
        }


        public void draw(SpriteBatch spriteBatch)
        {
            foreach (DisplayItem i in invent)
            {
                if (i != null)
                    i.draw(spriteBatch);
            }

            foreach (DisplayItem i in itemDisplayMateria)
            {
                if (i != null)
                    i.draw(spriteBatch);
            }
        }

        private void equipMateria(int index)
        {
            if (w != null)
            {
                if (w.MaxSlots != w.MateriaList.Count)
                {
                    w.equipMateria(materia[index], w.MateriaList.Count, bc);
                    getMateria();
                }
            }
            else if (a != null)
            {
                if (a.MaxSlots != a.MateriaList.Count)
                {
                    a.equipMateria(materia[index], a.MateriaList.Count, bc);
                    getMateria();
                }
            }
        }


        private void unequipMateria(int index)
        {
            if (w != null)
            {
                w.unequipMateria(index, bc);
            }
            else if (a != null)
            {
                w.unequipMateria(index, bc);
            }

            getMateria();
        }

        public void getMateria()
        {
            int totalSlotsPerPage, displaySideLength = 32;

            invent = new List<DisplayItem>();
            materia = new List<Materia>();
            materiaOnItem = new List<Materia>();
            itemDisplayMateria = new List<DisplayItem>();
            List<Item> inventory = bc.Inventory.ItemList;
            foreach (Item i in inventory)
            {
                if (i != null)
                {
                    if (i.GetType().IsSubclassOf(typeof(Materia)))
                    {
                        this.materia.Add((Materia)i);
                    }
                }
            }
            if (w != null)
            {
                foreach (Materia m in w.MateriaList)
                {
                    if (m != null)
                        materiaOnItem.Add(m);
                }
            }
            else if (a != null)
            {
                foreach (Materia m in a.MateriaList)
                {
                    if (m != null)
                        materiaOnItem.Add(m);
                }
            }

            totalSlotsPerPage = (pe.InventZone.Height / displaySideLength) * 2;
            pages = (materia.Count / totalSlotsPerPage) + 1;
            //set up the first page initially
            int counter = 0;
            Vector2 setterPos = new Vector2(pe.InventZone.X, pe.InventZone.Y);
            for (int i = 0; i < materia.Count; i++)
            {
                invent.Add(new DisplayItem(Image.Particle, setterPos, materia[i].Name, displaySideLength, false));
                counter++;
                if (counter > totalSlotsPerPage)
                    break;
                if (counter % 2 == 0)
                {
                    setterPos = new Vector2(pe.InventZone.X, pe.InventZone.Y + (displaySideLength * (counter / 2)));
                }
                else
                {
                    setterPos = new Vector2(pe.InventZone.X + pe.InventZone.Width - displaySideLength,
                        pe.InventZone.Y + (displaySideLength * (counter / 2)));
                }
            }

            int differenceX = 0;
            if (materiaOnItem.Count != 0)
            {
                 differenceX = ((Game1.View.Width / 2) - (pe.InventZone.X + pe.InventZone.Width)) / materiaOnItem.Count;
            }
            setterPos = new Vector2((pe.InventZone.X + pe.InventZone.Width + differenceX), Game1.View.Height / 2);
            for (int i = 0; i < materiaOnItem.Count; i++)
            {
                itemDisplayMateria.Add(new DisplayItem(materiaOnItem[i].Texture, setterPos, materiaOnItem[i].Name, 50, true));
                setterPos.X += differenceX;
            }
        }
    }
}
