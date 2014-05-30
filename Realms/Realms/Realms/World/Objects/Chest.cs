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
        public Chest(Texture2D tex, Location loc, int drawnWidth, int drawnHeight)
            : base(tex, loc, 1, 1, drawnWidth, drawnHeight)
        {
            color = Color.Gold;
            interactions.Insert(0, "Open");
        }

        public override void update(GameTime gameTime, Grid gr)
        {
            //float distance = this.measureDis(gr.Player.Position);

            //if(isColliding(Input.mouseCollisionRec()))
            //{
            //    if(distance <= (32 * Math.Sqrt(2)))
            //    {
            //        if(Input.leftMouseClick())
            //        {
            //            if (!looted)
            //            {
            //                chooseInteraction(0, gr.Player);
            //            }
            //        }
            //    }
            //}

            base.update(gameTime, gr);
        }

        public override void draw(SpriteBatch spriteBatch)
        {           
            base.draw(spriteBatch);
        }

        protected override void decideInteraction(string interaction, BaseCharacter player)
        {
            switch (interaction)
            {
                case "Open":
                    this.openChest(player);
                    interactions.RemoveAt(0);
                    break;
            }

            base.decideInteraction(interaction, player);
        }

        private void openChest(BaseCharacter player)
        {
            //open menu showing what you have
            if (!player.Inventory.Full)
            {
                color = Color.Brown;
            }
        }
    }
}
