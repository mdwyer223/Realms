using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    //This class used to draw an image on the screen
    public class BaseSprite
    {
        protected Texture2D texture;
        protected Color color;

        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            protected set { velocity = value; }
        }

        protected Rectangle rec;
        public Rectangle Rec
        {
            get { return rec; }
            protected set { rec = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(Rec.X, Rec.Y); }
            set
            {
                rec.X = (int)(value.X + .5);
                rec.Y = (int)(value.Y + .5);
            }
        }

        public Vector2 Center
        {
            get { return new Vector2(Rec.Center.X, Rec.Center.Y); }
        }

        public virtual bool IsVisible
        {
            get;
            protected set;
        }

        public int Speed
        {
            get;
            private set;
        }

        public BaseSprite(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 startPos)
        {
            this.texture = texture;
            color = Color.White;
            IsVisible = true;

            int inDisplayWidth = Game1.View.Width;

            if (texture != null)
            {
                rec.Width = (int)(inDisplayWidth * scaleFactor + 0.5f);
                float aspectRatio = (float)texture.Width / texture.Height;
                rec.Height = (int)(Rec.Width / aspectRatio + 0.5f);
            }

            // Shorthand condition logic
            Speed = (secondsToCrossScreen > 0) ? (int)(inDisplayWidth / (secondsToCrossScreen * 60)) : 0;

            Position = startPos;
        }

        public virtual void update(GameTime gameTime)
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
                spriteBatch.Draw(texture, Rec, color);
        }



        public float measureDis(Vector2 point1, Vector2 point2)
        {
            return (float)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public float measureDis(Vector2 point1)
        {
            return measureDis(this.Position, point1);
        }

        public bool isColliding(Rectangle inRec)
        {
            return this.Rec.Intersects(inRec);
        }

        public bool isColliding(BaseSprite obj)
        {
            return this.Rec.Intersects(obj.Rec);
        }
    }
}
