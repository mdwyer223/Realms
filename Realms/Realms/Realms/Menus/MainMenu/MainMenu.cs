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
    public class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        LogInForm logInForm;
        int loginID;

        public MainMenu(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            logInForm = new LogInForm();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (logInForm != null)
                logInForm.update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            logInForm.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
