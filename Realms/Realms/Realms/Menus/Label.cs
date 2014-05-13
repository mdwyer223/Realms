using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Label
    {
        string message;
        Vector2 position;
        SpriteFont font;
        Color standard = Color.White, focus = Color.Red;

        public bool Hover
        {
            get;
            set;
        }

        public string Text
        {
            get { return message; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }

        public Label(string message, Vector2 pos, SpriteFont font)
        {
            this.message = message;
            this.position = pos;
            this.font = font;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (Hover)
            {
                spriteBatch.DrawString(font, message, position, focus);
            }
            else
            {
                spriteBatch.DrawString(font, message, position, standard);
            }
        }
    }
}
