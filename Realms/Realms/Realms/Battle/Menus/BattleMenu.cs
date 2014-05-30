using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BattleMenu
    {
        BattleCharacter currentBC;
        Rectangle healthRec, backRec, manaRec, waitRec, displayRec;
        Vector2 optionsPos, skillsPos;
        List<Label> skills, options, invent, summons;

        float optionsLongWidth, skillPosYOrigin;

        public BattleMenu(BattleCharacter bc)
        {
            currentBC = bc;
            healthRec.Width = backRec.Width = manaRec.Width = waitRec.Width = 100;
            healthRec.Height = backRec.Height = manaRec.Height = waitRec.Height =  8;

            int height = (int)(Game1.View.Height * .2f);
            displayRec = new Rectangle(0, (int)Game1.View.Height - height, (int)Game1.View.Width, height);

            options = currentBC.BattleOpts;
            skills = currentBC.Skills;
            invent = currentBC.Inventory;
            summons = currentBC.Summons;

            optionsLongWidth = options[0].Font.MeasureString(options[0].Text).X;
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i] != null)
                {
                    if (options[i].Font.MeasureString(options[0].Text).X > optionsLongWidth)
                    {
                        optionsLongWidth = options[i].Font.MeasureString(options[0].Text).X;
                    }
                }
            }

            optionsLongWidth += (displayRec.Width * .02f);
        }

        public void update()
        {
            healthRec.Width = (int)(backRec.Width * (currentBC.HP / currentBC.MaxHP));
            manaRec.Width = (int)(backRec.Width * (currentBC.MP / currentBC.MaxMP));
            waitRec.Width = (int)(backRec.Width * currentBC.WaitPercent);

            optionsPos = new Vector2(waitRec.X, waitRec.Y + waitRec.Height + (.05f * displayRec.Height));
            skillsPos = new Vector2(optionsPos.X + optionsLongWidth, optionsPos.Y);
            skillPosYOrigin = skillsPos.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 healthPos = new Vector2(displayRec.X + (.05f*displayRec.Width), displayRec.Y),
                manaPos = new Vector2(displayRec.X + healthPos.X + backRec.Width + (.1f* displayRec.Width), displayRec.Y),
                waitPos = new Vector2(displayRec.X + healthPos.X, (healthPos.Y + healthRec.Height + (.02f * displayRec.Height)));

            backRec.X = healthRec.X = (int)healthPos.X;
            backRec.Y = healthRec.Y = (int)healthPos.Y;

            //spriteBatch.Draw(Image.Particle, displayRec, Color.Coral);

            spriteBatch.Draw(Image.Particle, backRec, Color.Gray);
            if (currentBC.HP / currentBC.MaxHP >= .25f)
            {
                spriteBatch.Draw(Image.Particle, healthRec, Color.Green);
            }
            else
            {
                spriteBatch.Draw(Image.Particle, healthRec, Color.Red);
            }

            backRec.X = manaRec.X = (int)manaPos.X;
            backRec.Y = manaRec.Y = (int)manaPos.Y;
            
            spriteBatch.Draw(Image.Particle, backRec, Color.Gray);
            spriteBatch.Draw(Image.Particle, manaRec, Color.Blue);

            backRec.X = waitRec.X = (int)waitPos.X;
            backRec.Y = waitRec.Y = (int)waitPos.Y;

            spriteBatch.Draw(Image.Particle, backRec, Color.Gray);
            spriteBatch.Draw(Image.Particle, waitRec, Color.Purple);

            foreach (Label l in options)
            {
                if (l != null)
                {
                    l.Position = optionsPos;
                    l.draw(spriteBatch);
                    optionsPos.Y += l.Font.MeasureString(l.Text).Y + (.01f * displayRec.Height);
                }
            }
            if (currentBC.SelectingSkills)
            {
                int count = 0;
                foreach (Label l in skills)
                {
                    if (l != null)
                    {
                        l.Position = skillsPos;
                        l.draw(spriteBatch);
                        skillsPos.Y += l.Font.MeasureString(l.Text).Y + (.01f * displayRec.Height);
                        if (count != 0 && count % 3 == 0)
                        {
                            skillsPos = new Vector2(optionsPos.X + optionsLongWidth + l.Font.MeasureString(l.Text).X + (.02f * displayRec.Width)
                                , skillPosYOrigin);
                        }
                        count++;
                    }
                }
            }
            else if (currentBC.SelectingItems)
            {
                int count = 0;
                foreach (Label l in invent)
                {
                    if (l != null)
                    {
                        l.Position = skillsPos;
                        l.draw(spriteBatch);
                        skillsPos.Y += l.Font.MeasureString(l.Text).Y + (.01f * displayRec.Height);
                        if (count != 0 && count % 3 == 0)
                        {
                            skillsPos = new Vector2(optionsPos.X + optionsLongWidth + l.Font.MeasureString(l.Text).X + (.02f * displayRec.Width)
                                , skillPosYOrigin);
                        }
                        count++;
                    }
                }
            }
            else if (currentBC.SelectingSummons)
            {
                int count = 0;
                foreach (Label s in summons)
                {
                    if (s != null)
                    {
                        s.Position = skillsPos;
                        s.draw(spriteBatch);
                        skillsPos.Y += s.Font.MeasureString(s.Text).Y + (.01f * displayRec.Height);
                        if (count != 0 && count % 3 == 0)
                        {
                            skillsPos = new Vector2(optionsPos.X + optionsLongWidth + s.Font.MeasureString(s.Text).X + (.02f * displayRec.Width)
                                , skillPosYOrigin);
                        }
                        count++;
                    }
                }
            }
        }
    }
}
