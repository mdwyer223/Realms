using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms.Menu
{
    public class Button
    {
        protected bool hasFocus, hasHover;
        protected Rectangle collision, hoverRec;
        protected Texture2D blank;

        public bool HasFocus
        {
            get { return hasFocus; }
        }

        public bool HasHover
        {
            get { return hasHover; }
        }

        public virtual void click()
        {
            hasFocus = true;
        }

        public void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
            {

            }
            else
            {
            }

            if (hasHover)
            {

            }
            else
            {
            }
        }
    }
}
