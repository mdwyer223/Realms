using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BattleMessage
    {
        protected Vector2 position, targetPos;
        protected Color color;
        protected string message;

        bool setPos = false;
        float alpha;

        public bool Over
        {
            get { return alpha < 100f; }
        }

        public BattleMessage(string message, Color color, Vector2 target)
        {
            this.message = message;
            this.color = color;
            targetPos = target;
            position = new Vector2(Game1.View.Width, Game1.View.Height);
            alpha = 255f;
        }

        public void update(GameTime gameTime)
        {
            if (!setPos)
            {
                position = targetPos;
                setPos = true;
            }

            position.Y -= .75f;
            alpha -= 2f;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Fonts.BattleMessage, message, position,
             color * ((float)Math.Abs(alpha) / 255f));
        }
    }
}
