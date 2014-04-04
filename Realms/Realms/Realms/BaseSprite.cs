using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MySql.Data.MySqlClient;

namespace Realms
{
    public class BaseSprite
    {
        protected Texture2D texture;
        protected Color color;

        protected Rectangle rec;
        public Rectangle Rec
        {
            get { return rec; }
            protected set { rec = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(Rec.X, Rec.Y); }
            protected set 
            { 
                rec.X = (int)(value.X + .5); 
                rec.Y = (int)(value.Y + .5); 
            }
        }

        public Vector2 Center
        {
            get { return new Vector2(Rec.Center.X, Rec.Center.Y); }
        }



        public BaseSprite(Texture2D texture, float scaleFactor)
        {
            this.texture = texture;
            color = Color.White;

            int inDisplayWidth = 800;//TODO: Remove
            int blah = Game.GraphicsDevice.Viewport;

            if (texture != null)
            {
                rec.Width = (int)(inDisplayWidth * scaleFactor + 0.5f);
                float aspectRatio = (float)texture.Width / texture.Height;
                rec.Height = (int)(Rec.Width / aspectRatio + 0.5f);
            }
        }

        public virtual void update(GameTime gameTime)
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rec, color);
        }



        public float measureDis(Vector2 point1, Vector2 point2)
        {
            return (float)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) - Math.Pow(point1.Y - point2.Y, 2));
        }

        public float measureDis(Vector2 point1)
        {
            return measureDis(this.Position, point1);
        }

        public bool isColliding(Rectangle inRec)
        {
            return this.Rec.Intersects(inRec);
        }

    }
}
