using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public enum Zone
    {

    }

    public enum Planet
    {

    }

    public class Grid
    {
        List<List<Tile>> map;
        BaseCharacter player;
        //TODO: list of enemies, npcs, players
        int rows, columns;

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public Grid()
        {
            map = new List<List<Tile>>();

            player = new Assassin(Image.Particle, 5, Location.Zero, 1);//TODO: remove
         
            this.rows = (int)(Game1.View.Height / Tile.T_HEIGHT + .5f);
            this.columns = (int)(Game1.View.Width / Tile.T_WIDTH + .5f);            

            for (int y = 0; y < rows; y++)
            {
                map.Add(new List<Tile>());
                for (int x = 0; x < columns; x++)
                {
                    map[y].Add(new Tile(Image.Particle, 0, new Location(y, x)));
                }
            }
        }

        public void update(GameTime gameTime)
        {
            player.update(gameTime, this);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x] != null)
                    {
                        map[y][x].update(gameTime, this);
                    }
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x] != null)
                    {
                        map[y][x].draw(spriteBatch);
                    }
                }
            }

            player.draw(spriteBatch);
        }

        public Tile[] getObjects()
        {
            List<Tile> objs = new List<Tile>();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x].GetType() != typeof(Tile) && map[y][x].Open)
                        objs.Add(map[y][x]);
                }
            }
            return objs.ToArray();
        }

        public bool isValid(Location loc)
        {
            if (loc.Row < 0 || loc.Column < 0)
                return false;
            else if (loc.Row >= Rows || loc.Column >= Columns)
                return false;
            else if (!map[loc.Row][loc.Column].Open)
                return false;             

            return true;
        }

        public Tile get(Location loc)
        {
            if (loc.Row < 0 || loc.Column < 0)
                return null;
            else if (loc.Row > Rows || loc.Column > Columns)
                return null;

            return map[loc.Row][loc.Column];
        }

        public void add(Location loc, Tile obj)
        {
            if (!isValid(loc))
                return;

            map[loc.Row][loc.Column] = obj;
        }


    }
}
