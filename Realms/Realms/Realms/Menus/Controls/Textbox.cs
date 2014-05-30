using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Textbox : Button
    {
        protected string input = "";

        public Textbox(bool focus, Vector2 startPos, float scaleFactor, string name, int numCharacters)
            : base(startPos, scaleFactor, name)
        {
            this.rec.Width = (int)(Fonts.FormBody.MeasureString("H").X * numCharacters);
            this.rec.Height = (int)(Fonts.FormBody.MeasureString("H").Y);
            this.hasFocus = focus;
            this.hasHover = false;
            this.label = name;
        }

        public Textbox(Vector2 startPos, float scaleFactor, string name, int numCharacters)
            : base(startPos, scaleFactor, name)
        {
            this.rec.Width = (int)(Fonts.FormBody.MeasureString("H").X * numCharacters);
            this.rec.Height = (int)(Fonts.FormBody.MeasureString("H").Y);
            this.hasFocus = hasHover = false;
            this.label = name;
        }

        public override void update(GameTime gameTime)
        {
            if (hasFocus)
            {
                input += Input.getRecentKeys();
                if (Input.backPressed())
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                    }
                }
            }
            base.update(gameTime);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
            {
                //draw textbox with cursor at the end of the string
                spriteBatch.Draw(texture, Rec, Color.White);
                spriteBatch.DrawString(Fonts.FormBody, input + "*", Position, Color.Red);
                spriteBatch.DrawString(Fonts.FormBody, label, 
                    new Vector2(Position.X - Fonts.FormBody.MeasureString(label).X - 1, Position.Y), Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, Rec, Color.White);
                spriteBatch.DrawString(Fonts.FormBody, input, Position, Color.Black);
                spriteBatch.DrawString(Fonts.FormBody, label, 
                    new Vector2(Position.X - Fonts.FormBody.MeasureString(label).X - 1, Position.Y), Color.White);
            }
            //base.draw(spriteBatch);
        }
    }
}
