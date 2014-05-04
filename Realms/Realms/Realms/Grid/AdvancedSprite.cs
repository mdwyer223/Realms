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
            base.update(gameTime, gr);
        }
    }
}
