using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Grid
    {
        List<List<Tile>> map;
        List<BaseSprite> everything;
        List<AdvancedSprite> advanced;

        BaseCharacter player;

        int rows, columns;

        public int Rows
        {
            get{return rows;}
        }

        public int Columns
        {
            get{return columns;}
        }

        public Grid(int rows, int columns)
        {
            everything = new List<BaseSprite>();
            advanced = new List<AdvancedSprite>();
            map = new List<List<Tile>>();

            player = new BaseCharacter(Location.Zero, .03f, Game1.GameContent.Load<Texture2D>("Particle"));

            everything.Add(player);

            this.rows = rows;
            this.columns = columns;

            for (int y = 0; y < rows; y++)
            {
                map.Add(new List<Tile>());
                for (int x = 0; x<columns; x++)
                {
                    map[y].Add(new Tile(new Location(y,x), Game1.GameContent.Load<Texture2D>("Particle")));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime, this);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x] != null)
                    {
                        map[y][x].Update(gameTime, everything);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x] != null)
                    {
                        map[y][x].Draw(spriteBatch);
                    }
                }
            }

            player.Draw(spriteBatch);
        }

        public bool validLocation(Location loc)
        {
            if (loc.Row < 0 || loc.Column < 0)
                return false;
            else if (loc.Row > Rows || loc.Column > Columns)
                return false;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x].Location == loc && !map[y][x].Open)
                        return false;
                }
            }

            return true;
        }
    }
}
