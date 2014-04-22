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
    public class AdvancedSprite : BaseSprite
    {
        protected Location loc, oldLoc;

        public AdvancedSprite(Vector2 startPos, float scaleFactor, Texture2D tex)
            : base(startPos, scaleFactor, tex)
        {
        }

        public AdvancedSprite(Location loc, float scaleFactor, Texture2D tex)
            : base(loc.Position, scaleFactor, tex)
        {
            this.loc = this.oldLoc = loc;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public virtual void Update(GameTime gameTime, Grid map)
        {
        }
    }
}
