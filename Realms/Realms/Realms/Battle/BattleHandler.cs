using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Realms
{
    public class BattleHandler : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Battle b;
        Grid currentGrid;

        public BattleHandler(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            //b = new Battle(bc);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (b == null)
            {
                Game1.changeState(GameState.PLAYING);
                return;
            }
            b.update(gameTime);
            if (b.Over)
            {
                Game1.changeWorld(currentGrid);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (b != null)
            {
                b.draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void activateBattle(Battle b, Grid g)
        {
            this.b = b;
            this.currentGrid = g;
        }
    }
}
