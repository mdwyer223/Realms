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
    public class PauseMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        enum PauseState
        {
            MAIN, SOCIAL, ITEMS, LIMIT, STATS, MATERIA, EQUIPMENT //Materia is just a place holder for gems
        }

        PauseState state = PauseState.MAIN;
        SpriteBatch spriteBatch;

        public PauseMenu(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            base.Draw(gameTime);
        }
    }
}
