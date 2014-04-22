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

        public Textbox(bool focus, Vector2 startPos, float scaleFactor, string name)
            : base(startPos, scaleFactor, name)
        {
            this.hasFocus = focus;
            this.hasHover = false;
            this.label = name;
        }

        public Textbox(Vector2 startPos, float scaleFactor, string name)
            : base(startPos, scaleFactor, name)
        {
            this.hasFocus = hasHover = false;
            this.label = name;
        }

        public override void Update(GameTime gameTime)
        {
            if (hasFocus)
            {
                input += Input.getRecentKeys();
                if (Input.backPressed())
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(input.Length - 1, 1);
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
            {
                //draw textbox with cursor at the end of the string
            }
            else
            {
                //draw textbox normally
            }
            base.Draw(spriteBatch);
        }
    }
}
