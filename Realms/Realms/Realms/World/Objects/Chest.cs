using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Chest : BaseObject
    {
        bool looted = false;

        public Chest(Texture2D tex, Location loc, int drawnWidth, int drawnHeight)
            : base(tex, loc, 1, 1, drawnWidth, drawnHeight)
        {
            color = Color.Gold;
        }

        public override void update(GameTime gameTime, Grid gr)
        {
            float distance = this.measureDis(gr.Player.Position);

            if(isColliding(Input.mouseCollisionRec()))
            {
                if(distance <= (32 * Math.Sqrt(2)))
                {
                    if(Input.leftMouseClick())
                    {
                        if (!looted)
                        {
                            this.openChest(gr.Player);
                        }
                    }
                }
            }

            base.update(gameTime, gr);
        }

        private void openChest(BaseCharacter player)
        {
            //open menu showing what you have
            color = Color.Brown;
            looted = true;
        }
    }
}
