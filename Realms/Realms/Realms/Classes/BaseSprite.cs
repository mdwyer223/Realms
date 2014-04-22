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
            IsDead = false;
            IsVisible = true;
            this.position = startPos;
            this.texture = tex;//check to see if the texture is null first
            color = Color.White;


            if (texture != null)
            {
                rec.Width = (int)(Game1.View.Width * scaleFactor + 0.5f);
                float aspectRatio = (float)texture.Width / texture.Height;
                rec.Height = (int)(rec.Width / aspectRatio + 0.5f);
            }
        }

        public BaseSprite(Vector2 startPos, float scaleFactor, Texture2D tex, Color color)
        {
            IsDead = false;
            IsVisible = true;
            this.position = startPos;
            this.texture = tex;//check to see if the texture is null first
            this.color = color;


            if (texture != null)
            {
                rec.Width = (int)(Game1.View.Width * scaleFactor + 0.5f);
                float aspectRatio = (float)texture.Width / texture.Height;
                rec.Height = (int)(rec.Width / aspectRatio + 0.5f);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(texture, Rec, color);
            }
        }

        public bool isColliding(Rectangle target)
        {
            return Rec.Intersects(target);
        }

        public float measureDistance(Vector2 point1, Vector2 point2)
        {
            return (float)Math.Sqrt((Math.Pow(point1.X - point2.X, 2)) + (Math.Pow(point1.Y - point2.Y, 2)));
        }

        public float measureDistance(Vector2 point1)
        {
            return measureDistance(this.Position, point1);
        }
    }
}
