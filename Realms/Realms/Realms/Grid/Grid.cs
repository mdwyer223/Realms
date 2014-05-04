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

    public abstract class Grid
    {
        List<List<Tile>> map;
        List<NonControlledCharacter> people;
        List<AdvancedSprite> NPCs, enemies;
        BaseCharacter player;
        Vector2 gridPos, gridBotRight, gridBotLeft, gridTopRight;
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
            enemies = new List<AdvancedSprite>();
            NPCs = new List<AdvancedSprite>();
            people = new List<NonControlledCharacter>();
            gridPos = Vector2.Zero;
            player = new Assassin(Image.Particle, 5, Location.Zero, 1);//TODO: remove
         
            this.rows = (int)(Game1.View.Height / Tile.T_HEIGHT + .5f);
            this.columns = (int)(Game1.View.Width / Tile.T_WIDTH + .5f);

            rows = 17;

            for (int y = 0; y < rows; y++)
            {
                map.Add(new List<Tile>());
                for (int x = 0; x < columns; x++)
                {
                    map[y].Add(new Tile(Image.Particle, 0, new Location(y, x)));
                }
            }
        }

        public Grid(int rows, int columns)
        {
            map = new List<List<Tile>>();
            enemies = new List<AdvancedSprite>();
            NPCs = new List<AdvancedSprite>();
            people = new List<NonControlledCharacter>();
            gridPos = Vector2.Zero;
            player = new Assassin(Image.Particle, 5, Location.Zero, 1);//TODO: remove

            this.rows = rows;
            this.columns = columns;

            for (int y = 0; y < rows; y++)
            {
                map.Add(new List<Tile>());
                for (int x = 0; x < columns; x++)
                {
                    map[y].Add(new Tile(Image.Particle, 0, new Location(y, x)));
                }
            }
        }

        public virtual void update(GameTime gameTime)
        {
            setCorners();

            player.update(gameTime, this);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x] != null)
                    {
                        if (Game1.Camera.IsInView(map[y][x].Position, map[y][x].Rec))
                        {
                            map[y][x].update(gameTime, this);
                        }
                        
                    }
                }
            }

            //GridPos = -player.ChangeInPos;
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (map[y][x] != null)
                    {
                        if (Game1.Camera.IsInView(map[y][x].Position, map[y][x].Rec))
                        {
                            map[y][x].draw(spriteBatch);
                        }
                    }
                }
            }

            foreach (NonControlledCharacter c in people)
            {
                if (c != null)
                    c.draw(spriteBatch);
            }

            foreach (AdvancedSprite a in NPCs)
            {
                if (a != null)
                    a.draw(spriteBatch);
            }

            foreach (AdvancedSprite e in enemies)
            {
                if (e != null)
                    e.draw(spriteBatch);
            }

            player.draw(spriteBatch);
        }

        private void setCorners()
        {
            gridBotRight = new Vector2(gridPos.X + (Tile.T_WIDTH * columns), gridPos.Y + (Tile.T_HEIGHT * rows));
            gridBotLeft = new Vector2(gridPos.X, gridPos.Y + (Tile.T_HEIGHT * rows));
            gridTopRight = new Vector2(gridPos.X + (Tile.T_WIDTH * columns), gridPos.Y);
        }

        public virtual Tile[] getObjects()
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

            for (int e = 0; e < enemies.Count; e++)
            {
                if (enemies[e] != null && !enemies[e].IsDead)
                    objs.Add(enemies[e]);
            }

            for (int n = 0; n < NPCs.Count; n++)
            {
                if (NPCs[n] != null)
                    objs.Add(NPCs[n]);
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
