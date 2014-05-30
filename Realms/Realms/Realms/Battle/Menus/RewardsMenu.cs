using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class RewardsMenu
    {
        int expPool, selectorIndex, lastIndex;
        bool levelUp;
        List<Label> itemList;
        List<Item> items;
        Item rareItem;
        BaseCharacter realCharacter;

        Rectangle displayRec;
        Vector2 expPos, itemStartPos, exitButtonPos;

        public bool Exited
        {
            get;
            protected set;
        }

        public RewardsMenu(List<BattleEnemy> enemies, BaseCharacter bc)
        {
            float width = Game1.View.Width * .66f, height = Game1.View.Height * .66f;
            displayRec = new Rectangle((int)((Game1.View.Width - width) / 2), (int)((Game1.View.Height - height) / 2), 
                (int)width, (int)height);
            realCharacter = bc;
            selectorIndex = 0;
            items = new List<Item>();
            itemList = new List<Label>();

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    Item drop = enemies[i].getDrop();
                    if (drop != null)
                        items.Add(drop);
                    expPool += enemies[i].ExpGiven;

                    if (i == enemies.Count - 1)
                    {
                        Item rare = enemies[i].getRare();
                        if (rare != null)
                            rareItem = rare;
                    }
                }
            }
            foreach (Item i in items)
            {
                if (i != null)
                    itemList.Add(new Label(i.NameAndCount, Vector2.Zero, Fonts.BattleMessage));//make rewards menu font
            }

            levelUp = bc.awardExp(expPool);

            expPos = new Vector2(displayRec.X, displayRec.Y);
            itemStartPos = new Vector2(displayRec.X + (displayRec.Width * .05f), expPos.Y + (displayRec.Height * .075f));
        }

        public void update(GameTime gameTime)
        {
            if (itemList.Count > 0)
            {
                if (Input.downPressed())
                {
                    lastIndex = selectorIndex;
                    selectorIndex++;
                    if (selectorIndex >= items.Count)
                        selectorIndex = 0;
                }
                else if (Input.upPressed())
                {
                    lastIndex = selectorIndex;
                    selectorIndex--;
                    if (selectorIndex < 0)
                        selectorIndex = items.Count - 1;
                }

                itemList[lastIndex].Hover = false;
                itemList[selectorIndex].Hover = true;

                if (Input.actionBarPressed())
                {
                    if (realCharacter.Inventory.addItem(items[selectorIndex]))
                    {
                        items.RemoveAt(selectorIndex);
                        itemList.RemoveAt(selectorIndex);
                        if (selectorIndex > itemList.Count)
                        {
                            if (itemList.Count > 0)
                            {
                                selectorIndex = itemList.Count - 1;

                                selectorIndex = 0;
                            }
                        }

                        if (lastIndex > itemList.Count)
                        {
                            if (itemList.Count > 0)
                            {
                                lastIndex = itemList.Count - 1;
                            }
                            else
                                lastIndex = 0;
                        }
                    }
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 itemPos = itemStartPos;

            spriteBatch.Draw(Image.Particle, displayRec, Color.Black);
            spriteBatch.DrawString(Fonts.BattleMessage, "Experience gained: " + expPool, expPos, Color.White);

            foreach(Label l in itemList)
            {
                if (l != null)
                {
                    l.Position = itemPos;
                    l.draw(spriteBatch);
                    //spriteBatch.DrawString(l.Font, l.Text, itemPos, Color.White);
                    itemPos.Y += l.Font.MeasureString(l.Text).Y + .02f * displayRec.Height;
                }

            }
        }

        public void setPlayer(BaseCharacter bc)
        {
            realCharacter = bc;
        }
    }
}
