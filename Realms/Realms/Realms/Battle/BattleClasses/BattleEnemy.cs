using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BattleEnemy : BattleSprite
    {
        BattleState state;
        List<string> battleOptions;

        public BattleEnemy(Texture2D texture, float scaleFactor, BaseEnemy enemy)
            : base(texture, scaleFactor, enemy.Stats)
        {
            color = Color.Red;
            state = BattleState.WAITING;

            this.stats = enemy.Stats;
            wait = (float)(MAX_WAIT) - (MAX_WAIT * (stats.Speed / 100f));
        }

        public override void update(GameTime gameTime, List<BattleSprite> battleField)
        {
            if (state == BattleState.WAITING)
            {
                color = Color.DarkBlue;
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
                //pick from the skills given to the enemy
                color = Color.Yellow;
                state = BattleState.SELECTING_SPRITE;
            }
            else if (state == BattleState.SELECTING_SPRITE)
            {
                //choose random player on the other battleField
                List<BattleSprite> characters = new List<BattleSprite>() ;
                for (int i = 0; i < battleField.Count; i++)
                {
                    if (battleField[i] != null)
                    {
                        if (!battleField[i].IsDead && 
                            (battleField[i].GetType() == typeof(BattleCharacter) || 
                             battleField[i].GetType() == typeof(BattleSprite)))
                        {
                            characters.Add(battleField[i]);
                        }
                    }
                }
                int choice = rand.Next(0, characters.Count);
                if(characters.Count > 0)
                {
                    characters[choice].damage(this.stats, new WoodenSword());
                }
                color = Color.Green;
                state = BattleState.MOVING;
                waitTimer = 0;  
            }
            else if (state == BattleState.MOVING)
            {
                //sit here till the animation is over, then trigger waiting
                state = BattleState.WAITING;
            }
            base.update(gameTime, battleField);
        }
    }
}
