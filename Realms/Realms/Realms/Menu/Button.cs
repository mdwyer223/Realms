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
            : base(startPos, scaleFactor, Game1.GameContent.Load<Texture2D>("Partcle"))
        {
            this.hasFocus = focus;
            this.hasHover = false;
            this.label = name;
        }

        public Button(Vector2 startPos, float scaleFactor, string name)
            : base(startPos, scaleFactor, Game1.GameContent.Load<Texture2D>("Partcle"))
        {
            this.hasFocus = hasHover = false;
            this.label = name;
        }

        public virtual void click()
        {
            hasFocus = true;
        }

        public virtual void offClick()
        {
            hasFocus = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Input.leftMouseClick())
            {
                if (this.Rec.Intersects(Input.mouseRec()))
                {
                    click();
                }
                else
                {
                    offClick();
                }
            }

            if (this.Rec.Intersects(Input.mouseRec()))
            {
                hasHover = true;
            }
            else
            {
                hasHover = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
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

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            //draw with the new font
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
