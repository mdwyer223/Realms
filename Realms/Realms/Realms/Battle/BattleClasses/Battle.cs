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
        List<BattleEnemy> enemies;
        List<BattleSprite> partyMembers;
        BattleCharacter character;

        BattleMenu menu;

        bool over;

        public bool Over
        {
            get { return over; }
        }

        public Battle(BattleCharacter b)
        {
            init();
            BaseEnemy be = new BaseEnemy(Image.Particle, 5, Location.Zero, EnemyType.LIGHT, 1);
            enemies.Add(new BattleEnemy(Image.Particle, .07f, be));
            character = b;
            createBattleField();
        }

        public Battle(BattleCharacter b, List<BattleSprite> party)
        {
            init();
            createBattleField();
        }

        public Battle(BattleCharacter b, List<BattleEnemy> enemyList)
        {
            init();
            character = b;
            enemies = enemyList;
            createBattleField();
        }

        public Battle(BattleCharacter b, List<BattleSprite> party, List<BattleSprite> enemyList)
        {
            init();
            createBattleField();
        }

        private void init()
        {
            battleField = new List<BattleSprite>();
            partyMembers = new List<BattleSprite>();
            enemies = new List<BattleEnemy>();
        }

        private void createBattleField()
        {
            menu = new BattleMenu(character);
            battleField.Add(character);

            if (partyMembers.Count == 0)
            {
                character.Position = new Vector2(Game1.View.Width * .075f, Game1.View.Height / 2);
            }
            else
            {
                //decide positioning here
                for (int i = 0; i < partyMembers.Count; i++)
                {
                    if (partyMembers[i] != null)
                    {
                        battleField.Add(partyMembers[i]);
                    }
                }
            }

            float x = 0, y = 0;
            if (enemies.Count == 1)
            {
                x = Game1.View.Width * .85f;
                y = Game1.View.Height / 2;
                enemies[0].Position = new Vector2(x, y);
            }
            else if (enemies.Count == 2)
            {
                x = Game1.View.Width * .85f;
                y = Game1.View.Height / 3;
                enemies[0].Position = new Vector2(x, y);
                enemies[1].Position = new Vector2(x, y + y);
            }
            else if (enemies.Count == 3)
            {
                x = Game1.View.Width * .85f;
                y = Game1.View.Height / 4;
                enemies[0].Position = new Vector2(x, y);
                enemies[1].Position = new Vector2(x, y + y);
                enemies[2].Position = new Vector2(x, y + y + y);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    battleField.Add(enemies[i]);
                }
            }
        }

        public void update(GameTime gameTime)
        {
            menu.update();
            //constantly seek party's health, enemy's health, update damage dealt, update health/mana on server
            for (int i = 0; i < battleField.Count; i++)
            {
                if (battleField[i] != null && !battleField[i].IsDead)
                {
                    battleField[i].update(gameTime, battleField);
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null && enemies[i].IsDead)
                {
                    enemies.RemoveAt(i);
                    break;
                }
            }

            if (enemies.Count == 0 || character.IsDead)
            {
                //check to see if the player is done selecting items
                    over = true;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            menu.draw(spriteBatch);
            foreach (BattleSprite b in battleField)
            {
                if (b != null && b.IsVisible)
                {
                    b.draw(spriteBatch);
                }
            }
        }
    }
}
