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
    public class FormHandler : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        static Form currentForm;

        public Form FormOpen
        {
            get { return currentForm; }
        }

        public FormHandler(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.Enabled = true;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentForm != null)
            {
                this.Visible = true;
                currentForm.update(gameTime);
            }

            base.Update(gameTime);

            if (currentForm != null)
            {
                if (!currentForm.Open)
                {
                    currentForm = null;
                    this.Visible = false;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (currentForm != null)
                currentForm.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void openForm(Form newForm)
        {
            currentForm = newForm;
        }
    }
}
