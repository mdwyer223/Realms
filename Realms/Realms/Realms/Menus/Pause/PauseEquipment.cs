using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class PauseEquipment
    {
        Rectangle inventoryZone, statsZone, equipZone;
        BaseCharacter bc;
        MateriaEquip materiaMenu;
        List<Item> equipment, onScreenEquipment;
        List<Materia> materia;
        List<DisplayItem> invent;

        DisplayItem wep, access, armor, nextPage, prevPage, pageNumber;

        int totalSlotsPerPage, currentPage = 1, pages, displaySideLength = 32,
            selectorIndex = 0;

        EquipScreen state = EquipScreen.EQUIPMENT;

        public EquipScreen State
        {
            get { return state; }
        }

        public Rectangle InventZone
        {
            get { return inventoryZone; }
        }

        public Rectangle NextPage
        {
            get { return nextPage.Rec; }
        }

        public Rectangle PrevPage
        {
            get { return prevPage.Rec; }
        }

        public DisplayItem CurrentPage
        {
            get { return pageNumber; }
        }

        public PauseEquipment()
            : base()
        {
            equipment = new List<Item>();
            invent = new List<DisplayItem>();
            materia = new List<Materia>();
            onScreenEquipment = new List<Item>();//32 height, 32 width scale from the sides of the inventory zone
            float height = Game1.View.Height * .66f, width = Game1.View.Width * (1f/4f);
            inventoryZone = new Rectangle(0, 0, (int)width, (int)height);
            height = Game1.View.Height - inventoryZone.Height;
            statsZone = new Rectangle(inventoryZone.X, inventoryZone.Y + inventoryZone.Height, (int)width, (int)height);
            equipZone = new Rectangle(inventoryZone.X + inventoryZone.Width, 0, Game1.View.Width - inventoryZone.Width, Game1.View.Height);

            nextPage = new DisplayItem(Image.Particle, new Vector2(inventoryZone.X + inventoryZone.Width, inventoryZone.Y), "Next Page", 20, true);
            prevPage = new DisplayItem(Image.Particle, new Vector2(nextPage.Rec.X, nextPage.Rec.Y + nextPage.Rec.Height + 3), "Prev Page", 20, true);
            pageNumber = new DisplayItem(Image.Particle, new Vector2(prevPage.Rec.X, prevPage.Rec.Y + prevPage.Rec.Height + 3), "0", 20, true);
        }

        public void update(GameTime gameTime, BaseCharacter bc)
        {
            this.bc = bc;

            pageNumber.Name = "" + currentPage;

            if (state == EquipScreen.EQUIPMENT)
            {
                if (currentPage != pages)
                {
                    //check next
                    if (Input.mouseDrawnRec().Intersects(nextPage.Rec))
                    {
                        if (Input.leftMouseClick())
                        {
                            currentPage++;
                            getNewItems(currentPage);
                        }
                    }
                }

                if (currentPage != 1)
                {
                    if (Input.mouseDrawnRec().Intersects(prevPage.Rec))
                    {
                        if (Input.leftMouseClick())
                        {
                            currentPage--;
                            getNewItems(currentPage);
                        }
                    }
                }

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
                    if (equipment[selectorIndex].GetType().IsSubclassOf(typeof(Weapon)))
                    {
                        Weapon w = (Weapon)equipment[selectorIndex];
                        bc.equipWeapon(w);
                        initialSetUp(bc);
                    }
                    else if (equipment[selectorIndex].GetType().IsSubclassOf(typeof(Armor)))
                    {
                        Armor a = (Armor)equipment[selectorIndex];
                        bc.equipArmor(a);
                        initialSetUp(bc);
                    }
                    else if (equipment[selectorIndex].GetType().IsSubclassOf(typeof(Accessory)))
                    {
                        Accessory ac = (Accessory)equipment[selectorIndex];
                        bc.equipAccessory(ac);
                        initialSetUp(bc);
                    }
                }

                if (Input.mouseDrawnRec().Intersects(wep.Rec))
                {
                    if (Input.leftMouseClick())
                    {
                        state = EquipScreen.MATERIA;
                        materiaMenu = new MateriaEquip(bc.Equipment.wep, bc, this);
                    }
                }
                else if (Input.mouseDrawnRec().Intersects(armor.Rec))
                {
                    if (Input.leftMouseClick())
                    {
                        state = EquipScreen.MATERIA;
                        materiaMenu = new MateriaEquip(bc.Equipment.armor, bc, this);
                    }
                }
            }
            else if (state == EquipScreen.MATERIA)
            {
                if(materiaMenu != null)
                    materiaMenu.update(gameTime);

                if (Input.escapePressed())
                    state = EquipScreen.EQUIPMENT;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image.Particle, inventoryZone, Color.Red);

            spriteBatch.Draw(Image.Particle, statsZone, Color.Green);
            if (bc != null)
            {
                spriteBatch.DrawString(Fonts.FormBody, bc.calcStats().ToString(),
                    new Vector2(statsZone.X, statsZone.Y), Color.White);
            }

            spriteBatch.Draw(Image.Particle, equipZone, Color.Blue);

            prevPage.draw(spriteBatch);
            nextPage.draw(spriteBatch);
            pageNumber.draw(spriteBatch);

            if (state == EquipScreen.EQUIPMENT)
            {
                for (int i = 0; i < invent.Count; i++)
                {
                    if (invent[i] != null)
                        invent[i].draw(spriteBatch);
                }

                if (bc != null)
                {
                    spriteBatch.DrawString(Fonts.FormSubTitle, bc.Equipment.wep.ToString(),
                        new Vector2(equipZone.X + (equipZone.Width * .1f), equipZone.Y), Color.White);
                    spriteBatch.DrawString(Fonts.FormSubTitle, bc.Equipment.armor.ToString(),
                        new Vector2(equipZone.X + (equipZone.Width / 2), equipZone.Y), Color.White);
                    wep.draw(spriteBatch);
                    armor.draw(spriteBatch);
                }
            }
            else if (state == EquipScreen.MATERIA)
            {
                if (materiaMenu != null)
                    materiaMenu.draw(spriteBatch);
                else
                    state = EquipScreen.EQUIPMENT;
            }
        }

        public void initialSetUp(BaseCharacter c)
        {
            invent = new List<DisplayItem>();
            equipment = new List<Item>();
            List<Item> inventory = c.Inventory.ItemList;
            foreach (Item i in inventory)
            {
                if (i != null)
                {
                    if (!i.GetType().IsSubclassOf(typeof(Miscellanious)) 
                        && !i.GetType().IsSubclassOf(typeof(Materia)))
                    {
                        this.equipment.Add(i);
                    }
                }
            }

            totalSlotsPerPage = (inventoryZone.Height / displaySideLength) * 2;
            pages = (equipment.Count / totalSlotsPerPage) + 1;
            //set up the first page initially
            int counter = 0;
            Vector2 setterPos = new Vector2(inventoryZone.X, inventoryZone.Y);
            for (int i = 0; i < equipment.Count; i++)
            {
                invent.Add(new DisplayItem(Image.Particle, setterPos, equipment[i].Name, displaySideLength, false));
                counter++;
                if (counter > totalSlotsPerPage)
                    break;
                if (counter % 2 == 0)
                {
                    setterPos = new Vector2(inventoryZone.X, inventoryZone.Y + (displaySideLength * (counter / 2)));
                }
                else
                {
                    setterPos = new Vector2(inventoryZone.X + inventoryZone.Width - displaySideLength, 
                        inventoryZone.Y + (displaySideLength * (counter / 2)));
                }
            }

            getNewItems(currentPage);

            wep = new DisplayItem(Image.Particle, new Vector2(equipZone.X + (equipZone.Width * .1f),
                equipZone.Y + (equipZone.Height * .33f)), c.Equipment.wep.Name, 96, false);
            armor = new DisplayItem(Image.Particle, new Vector2(equipZone.X + equipZone.Width - (equipZone.Width * .1f) - 96,
                equipZone.Y + (equipZone.Height * .33f)), c.Equipment.armor.Name, 96, false);
        }

        public void getNewItems(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > pages)
                return;

            int counter = 0;
            Vector2 setterPos = new Vector2(inventoryZone.X, inventoryZone.Y);
            invent = new List<DisplayItem>();
            for (int i = (pageNumber - 1) * totalSlotsPerPage; i < equipment.Count; i++)
            {
                invent.Add(new DisplayItem(Image.Particle, setterPos, equipment[i].Name, displaySideLength, false));
                counter++;
                if (counter > totalSlotsPerPage)
                    break;
                if (counter % 2 == 0)
                {
                    setterPos = new Vector2(inventoryZone.X, inventoryZone.Y + (displaySideLength * (counter / 2)));
                }
                else
                {
                    setterPos = new Vector2(inventoryZone.X + inventoryZone.Width - displaySideLength,
                        inventoryZone.Y + (displaySideLength * (counter / 2)));
                }
            }
        }
    }

    public class DisplayItem
    {
        Texture2D texture;
        Rectangle rec;
        Vector2 pos;
        bool drawName = false;
        string name;

        public Rectangle Rec
        {
            get { return rec; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public DisplayItem(Texture2D tex, Vector2 pos, string name, int sideLength, bool drawName)
        {
            this.texture = tex;
            this.pos = pos;
            this.name = name;
            if (this.name.Contains(" "))
            {
                int index = this.name.IndexOf(" ");
                this.name = this.name.Insert(index, "\n");
            }
            this.drawName = false;
            rec = new Rectangle((int)pos.X, (int)pos.Y, sideLength, sideLength);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, Color.White);
            if (Input.mouseDrawnRec().Intersects(this.rec))
            {
                spriteBatch.Draw(Image.Particle, new Rectangle(Input.mouseDrawnRec().X + Input.mouseDrawnRec().Width, Input.mouseDrawnRec().Y,
                    (int)Fonts.FormBody.MeasureString(name).X, (int)Fonts.FormBody.MeasureString(name).Y), Color.Black);
                spriteBatch.DrawString(Fonts.FormBody, name, new Vector2(Input.mouseDrawnRec().X + Input.mouseDrawnRec().Width, Input.mouseDrawnRec().Y), Color.White);
            }
        }
    }
}
