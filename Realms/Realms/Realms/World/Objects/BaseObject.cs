using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public abstract class BaseObject : Tile
    {
        int width, height;

        protected Rectangle drawnRec;
        public virtual Rectangle DrawnRec
        {
            get { return drawnRec; }
        }

        public BaseObject(Texture2D tex, Location loc, int width, int height, int drawnWidth, int drawnHeight)
            : base(tex, 0, loc)
        {
            this.width = width;
            this.height = height;

            rec.Width = (width * Tile.T_WIDTH);
            rec.Height = (height * Tile.T_HEIGHT);

            drawnRec.Width = drawnWidth * Tile.T_WIDTH;
            drawnRec.Height = drawnHeight * Tile.T_HEIGHT;

            rec.X = loc.Column * Tile.T_WIDTH;
            if (loc.Row != 0)
            {
                rec.Y = (loc.Row * Tile.T_HEIGHT) - (height * Tile.T_HEIGHT);
            }
            else
            {
                rec.Y = 0;
            }

            drawnRec.X = rec.X;
            drawnRec.Y = rec.Y - (drawnRec.Height - rec.Height);
        }

        public override void update(GameTime gameTime, Grid gr)
        {
            float distance = this.measureDis(gr.Player.Position);

            if (isColliding(Input.mouseCollisionRec()))
            {
                if (distance <= (32 * Math.Sqrt(2)))
                {
                    mouseHover = true;
                    if (Input.leftMouseClick())
                    {
                        //choose default option
                        chooseInteraction(0, gr.Player);
                    }
                }
            }
            else
            {
                mouseHover = false;
            }
            
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (mouseHover)
            {
                spriteBatch.DrawString(Fonts.FormSubTitle, "Choose option: " + interactions[0],
                    new Vector2(0, 0) - Game1.Camera.Origin + Game1.Camera.Position, color);
            }
            spriteBatch.Draw(texture, DrawnRec, color);
        }
    }
}
