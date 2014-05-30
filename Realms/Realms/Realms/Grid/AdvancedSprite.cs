using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Realms
{
    //This class is to allow code reuse between player, enemies
    public abstract class AdvancedSprite : Tile
    {
        protected Rectangle drawnRec;
        public virtual Rectangle DrawnRec
        {
            get { return drawnRec; }
        }


        public AdvancedSprite(Texture2D texture, float secondsToCrossScreen, Location startLoc)
            : base(texture, secondsToCrossScreen, startLoc)
        {            
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
    }
}
