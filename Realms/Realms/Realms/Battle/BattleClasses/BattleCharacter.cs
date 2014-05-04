using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BattleCharacter : BattleSprite
    {
        Equips equipment;
        BattleState state;
        Rectangle waitRec, backgroundWait;
        int waitWidth, waitHeight;
        List<string> battleOptions;

        public BattleCharacter(Texture2D texture, float scaleFactor, BaseCharacter character)
            : base(texture, scaleFactor, character.calcStats())
        {
            color = Color.Green;
            battleOptions = new List<string>();
            this.stats = character.calcStats();
            this.equipment = character.Equipment;//check materia for any options to add on

            wait = (float)(MAX_WAIT) - (MAX_WAIT * (stats.Speed / 100f));
            state = BattleState.WAITING;

            waitRec.X = backgroundWait.X = (int)this.Position.X;
            waitRec.Y = backgroundWait.Y = 300;
            backgroundWait.Width = waitWidth = 50;
            backgroundWait.Height = waitRec.Height = waitHeight = 25;

            battleOptions.Add("Attack");
        }

        public override void update(GameTime gameTime, List<BattleSprite> battleField)
        {
            waitRec.Width = (int)(waitWidth * (waitTimer / wait));
            if (state == BattleState.WAITING)
            {
                if (waitTimer < wait)
                {
                    waitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    state = BattleState.WAITING;
                }
                else
                {
                    waitTimer = wait;
                    state = BattleState.SELECTING_OPTION;
                }
            }
            else if (state == BattleState.SELECTING_OPTION)
            {
                //display box, show battle options
                //using attack for now
                if (Input.actionBarPressed())
                {
                    chooseBattleOption(battleOptions[0], battleField[1]);//just record the option here, dont choose the option
                    state = BattleState.SELECTING_SPRITE;
                }
            }
            else if (state == BattleState.SELECTING_SPRITE)
            {
                //choose option here after picking someone on the field
                waitTimer = 0;
                state = BattleState.MOVING;
            }
            else if (state == BattleState.MOVING)
            {
                //sit here till the animation is over, then trigger waiting
                state = BattleState.WAITING;
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image.Particle, backgroundWait, Color.Purple);
            spriteBatch.Draw(Image.Particle, waitRec, Color.Blue);
            base.draw(spriteBatch);
        }

        public void chooseBattleOption(string s, BattleSprite target)
        {
            switch(s)
            {
                case "Attack":
                    attack(target);
                    state = BattleState.MOVING;
                    break;
            }
        }

        public void attack(BattleSprite target)
        {
            target.damage(this.stats, this.equipment.wep);
        }
    }
}
