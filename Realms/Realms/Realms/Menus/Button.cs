using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Button : BaseSprite
    {
        protected bool hasFocus, hasHover;
        protected string label;
        protected Rectangle hoverRec;
        protected SpriteFont standard;

        public bool HasFocus
        {
            get { return hasFocus; }
        }

        public bool HasHover
        {
            get { return hasHover; }
        }

        public string Label
        {
            get { return label; }
        }

        public Button(bool focus, Vector2 startPos, float scaleFactor, string name)
            : base(Image.Particle, scaleFactor, 0 , startPos)
        {
            this.hasFocus = focus;
            this.hasHover = false;
            this.label = name;
        }

        public Button(Vector2 startPos, float scaleFactor, string name)
            : base(Image.Particle, scaleFactor, 0, startPos)
        {
            this.hasFocus = hasHover = false;
            this.label = name;
        }


        public Button(Vector2 startPos, float scaleFactor, string name, SpriteFont font)
            : base(Image.Particle, scaleFactor, 0, startPos)
        {
            this.hasFocus = hasHover = false;
            this.label = name;
        }

        public override void update(GameTime gameTime)
        {
            hasHover = this.Rec.Intersects(Input.mouseRec());

            if (Input.leftMouseClick() && hasHover)
            {
                click();
            }
            else
            {
                offClick();
            }   
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            //draw the label no matter what
            //should be just over top of the button/box itself
            //draw with standard here
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


        public virtual void click()
        {
            hasFocus = true;
        }

        public virtual void offClick()
        {
            hasFocus = false;
        }
    }
}
