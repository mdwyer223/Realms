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
    public class PauseMenu : DrawableGameComponent
    {
        enum PauseState
        {
            MAIN, SOCIAL, EQUIPMENT, INVENTORY
        }

        PauseState state = PauseState.MAIN;
        SpriteBatch spriteBatch;

        BaseCharacter bc;

        List<Button> options;
        bool delay = false;

        PauseEquipment equipScreen;

        public bool Quit
        {
            get;
            protected set;
        }

        public PauseMenu(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            options = new List<Button>();//resume, equipment, invent, skills, social, logout, quit game
            int numOptions = 7;
            float spacingY = (Game1.View.Height - (Game1.View.Height * .13f)) / numOptions;
            Vector2 pos = new Vector2((Game1.View.Width/2f), (float)(Game1.View.Height * .075f));
            options.Add(new Button(pos, .05f, "Resume"));
            pos.Y += spacingY;
            options.Add(new Button(pos, .05f, "Equipment"));
            pos.Y += spacingY;
            options.Add(new Button(pos, .05f, "Inventory"));
            pos.Y += spacingY;
            options.Add(new Button(pos, .05f, "Skills"));
            pos.Y += spacingY;
            options.Add(new Button(pos, .05f, "Social"));
            pos.Y += spacingY;
            options.Add(new Button(pos, .05f, "Logout"));
            pos.Y += spacingY;
            options.Add(new Button(pos, .05f, "Quit"));


            for (int i = 0; i < options.Count; i++)
            {
                if (options[i] != null)
                {
                    options[i].Position = new Vector2(options[i].Position.X - (options[i].CurrentFont.MeasureString(options[i].Label).X / 2f),
                        options[i].Position.Y);
                }
            }

            equipScreen = new PauseEquipment();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (delay)
            {
                if (Input.escapePressed())
                {
                    if (state == PauseState.MAIN)
                    {
                        Game1.changeState(GameState.PLAYING);
                        delay = false;
                    }
                    else
                    {
                        if(equipScreen != null)
                        {
                            if(equipScreen.State == EquipScreen.EQUIPMENT)
                                state = PauseState.MAIN;
                        }
                        else 
                            state = PauseState.MAIN;
                    }
                }
            }
            else
            {
                delay = true;
            }

            if (state == PauseState.MAIN)
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i] != null)
                    {
                        options[i].update(gameTime);
                        if (options[i].HasFocus)
                        {
                            if (options[i].Label.Equals("Resume"))
                            {
                                Game1.changeState(GameState.PLAYING);
                                options[i].offClick();
                            }
                            else if (options[i].Label.Equals("Quit"))
                            {
                                Quit = true;
                            }
                            else if (options[i].Label.Equals("Equipment"))
                            {
                                state = PauseState.EQUIPMENT;
                                equipScreen.initialSetUp(this.bc);
                                options[i].offClick();
                            }
                        }
                    }
                }
            }
            else if (state == PauseState.EQUIPMENT)
            {
                equipScreen.update(gameTime, this.bc);
            }
            else if (state == PauseState.INVENTORY)
            {

            }
            else if (state == PauseState.SOCIAL)
            {

            }
            else
                Game1.changeState(GameState.PLAYING);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (state == PauseState.MAIN)
            {
                foreach (Button b in options)
                {
                    if (b != null)
                        b.draw(spriteBatch);
                }
            }
            else if (state == PauseState.EQUIPMENT)
            {
                equipScreen.draw(spriteBatch);
            }
            else if (state == PauseState.INVENTORY)
            {

            }
            else if (state == PauseState.SOCIAL)
            {

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void setCharacter(BaseCharacter currentPlayer)
        {
            this.bc = currentPlayer;
        }
    }
}
