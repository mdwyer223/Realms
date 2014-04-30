using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    /// <summary>
    /// This will trigger from the grid, go into game1 then set up a new battle scene there
    /// </summary>
    public class Battle
    {
        List<BattleSprite> battleField;
        List<BattleSprite> enemies;
        List<BattleSprite> partyMembers;
        BattleCharacter character;

        public Battle(BattleCharacter b)
        {
            enemies.Add(new BattleEnemy());
            createBattleField();
        }

        public Battle(BattleCharacter b, List<BattleSprite> party)
        {
            createBattleField();
        }

        public Battle(BattleCharacter b, List<BattleSprite> enemyList)
        {
            createBattleField();
        }

        public Battle(BattleCharacter b, List<BattleSprite> party, List<BattleSprite> enemyList)
        {
            createBattleField();
        }

        private void createBattleField()
        {
            battleField.Add(character);

            for (int i = 0; i < partyMembers.Count; i++)
            {
                if (partyMembers[i] != null)
                {
                    battleField.Add(partyMembers[i]);
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    battleField.Add(enemies[i]);
                }
            }
            //positioning
            battleField[0].Position = new Vector2(240, 50);//temp
            battleField[1].Position = new Vector2(240, 750);
        }

        public void update(GameTime gameTime)
        {
            //constantly seek party's health, enemy's health, update damage dealt, update health/mana on server
        }

        public void draw(SpriteBatch spriteBatch)
        {

        }
    }
}
