using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class QuestHandler : DrawableGameComponent
    {
        List<Quest> currentQuests;

        BaseCharacter currentPlayer;
        Form formOpen;
        Battle currentBattle;

        public QuestHandler(Game1 game)
            : base(game)
        {
            currentQuests = new List<Quest>();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentPlayer != null)
            {
                currentQuests = currentPlayer.QuestList;
            }


            if (currentQuests != null)
            {
                foreach (Quest q in currentQuests)
                {
                    if (q != null)
                    {
                        q.checkQuest(formOpen, currentBattle, currentPlayer);
                        if (q.Completed)
                        {
                            currentPlayer.completedQuest(q);
                            break;
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void getPlayer(BaseCharacter bc)
        {
            currentPlayer = bc;
        }

        public void getForm(Form f)
        {
            this.formOpen = f;
        }

        public void getBattle(Battle b)
        {
            this.currentBattle = b;
        }
    }
}
