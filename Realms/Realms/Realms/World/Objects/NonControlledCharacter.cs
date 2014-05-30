using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class NonControlledCharacter : AdvancedSprite
    {
        int id;

        public NonControlledCharacter(Texture2D texture, float speed, Location startLoc, int id)
            : base(texture, speed, startLoc)
        {
            this.id = id;
        }

        public override void update(GameTime gameTime, Grid gr)
        {
            //listen
            base.update(gameTime, gr);
        }
    }
}
