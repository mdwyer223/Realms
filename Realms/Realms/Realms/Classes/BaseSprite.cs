using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BaseSprite
    {
        protected Rectangle rec;
        protected Vector2 position;
        protected Texture2D texture;
        protected Color color;

        public bool IsDead
        {
            get;
            protected set;
        }

        public bool IsVisible
        {
            get;
            protected set;
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Rectangle Rec
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, rec.Width, rec.Height); }
        }

        public BaseSprite(Vector2 startPos, float scaleFactor, Texture2D tex)
        {
            this.position = startPos;
            this.texture = tex;
            color = Color.White;
            //rec logic, aspectratio
        }

        public BaseSprite(Vector2 startPos, float scaleFactor, Texture2D tex, Color color)
        {
            this.position = startPos;
            this.texture = tex;
            this.color = color;
            //rec logic, aspectratio
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(texture, rec, color);
            }
        }
    }
}
