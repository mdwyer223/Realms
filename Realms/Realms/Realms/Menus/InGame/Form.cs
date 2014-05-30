using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Form
    {
        protected Npc npc;
        protected Texture2D background, headShot;
        protected Rectangle displayRec, headShotRec;
        protected Vector2 titlePos, subtitlePos, bodyPos;
        protected List<Button> controls;
        protected string messageBody, title, subtitle;

        public bool Open
        {
            get;
            protected set;
        }

        public Npc NPCGiven
        {
            get { return npc; }
        }

        public Form(Npc npc, string title, string subtitle, string body)
            : base()
        {
            Open = true;
            this.npc = npc;
            controls = new List<Button>();
            if (npc != null && npc.HeadShot != null)
            {
                this.headShot = npc.HeadShot;
            }
            else
                this.headShot = Image.Particle;//question mark maybe

            if (title != null && !title.Equals(""))
            {
                this.title = title;
            }
            else
                this.title = "?????";

            this.subtitle = subtitle;

            float width = Game1.View.Width * .66f, height = Game1.View.Height * .66f;
            displayRec = new Rectangle((int)((Game1.View.Width - width) / 2), (int)((Game1.View.Height - height) / 2),
                (int)width, (int)height);
            headShotRec = new Rectangle((int)displayRec.X, (int)displayRec.Y, 
                (int)(displayRec.Width * .15f), (int)(displayRec.Height * .2f));
            titlePos = new Vector2(headShotRec.X + headShotRec.Width + (displayRec.Width * .03f), headShotRec.Y);
            subtitlePos = new Vector2(titlePos.X, titlePos.Y + Fonts.FormTitle.MeasureString(this.title).Y);
            bodyPos = new Vector2(displayRec.X, displayRec.Y + headShotRec.Height + 10);

            int bodyWidth = 0, lineCount = 0, characterCount = 0;
            string actualBody = "";
            foreach (char c in body)
            {
                characterCount++;
                actualBody += c.ToString();
                bodyWidth += (int)Fonts.FormBody.MeasureString(c.ToString()).X;

                if (c.ToString().Equals(" "))
                {
                    string testString;
                    int index = body.IndexOf(" ", characterCount);
                    if (index == -1)
                    {
                        testString = body.Substring(characterCount);
                        if (Fonts.FormBody.MeasureString(testString).X + bodyWidth > displayRec.Width)
                        {
                            actualBody += "\n";
                            bodyWidth = 0;
                        }
                    }
                    else
                    {
                        testString = body.Substring(characterCount, index - characterCount);
                        if (Fonts.FormBody.MeasureString(testString).X + bodyWidth > displayRec.Width)
                        {
                            actualBody += "\n";
                            bodyWidth = 0;
                            lineCount++;
                        }
                    }
                }
            }

            messageBody = actualBody;
        }

        public virtual void update(GameTime gameTime)
        {
            if (Input.escapePressed())
            {
                Open = false;
            }

            foreach (Button c in controls)
            {
                if (c != null)
                    c.update(gameTime);
            }
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image.Particle, displayRec, Color.Black);
            spriteBatch.Draw(headShot, headShotRec, Color.White);
            spriteBatch.DrawString(Fonts.FormTitle, title, titlePos, Color.White);
            spriteBatch.DrawString(Fonts.FormSubTitle, subtitle, subtitlePos, Color.White);
            spriteBatch.DrawString(Fonts.FormBody, messageBody, bodyPos, Color.White);
            spriteBatch.DrawString(Fonts.FormBody, "(ESC)", 
                new Vector2(displayRec.X + displayRec.Width - Fonts.FormBody.MeasureString("(ESC)").X, displayRec.Y), Color.Red);

            foreach (Button c in controls)
            {
                if (c != null)
                    c.draw(spriteBatch);
            }
        }

        public void changeOpen(bool value)
        {
            Open = value;
        }
    }
}
