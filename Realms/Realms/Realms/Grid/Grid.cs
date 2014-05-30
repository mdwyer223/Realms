using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public abstract class Grid
    {
        Thread mapUpdate;
        protected List<List<Tile>> map;
        protected List<NonControlledCharacter> people;
        protected List<AdvancedSprite> NPCs, enemies;
        protected List<BaseObject> objects;
        protected BaseCharacter player;
        Vector2 gridPos, gridBotRight, gridBotLeft, gridTopRight;
        protected int rows, columns;
        protected Location startLocPlayer;

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public List<List<Tile>> Map
        {
            get { return map; }
        }

        public BaseCharacter Player
        {
            get { return player; }
            set { player = value; }
        }

        public Location StartLoc
        {
            get { return startLocPlayer; }
        }

        public Grid()
        {
            map = new List<List<Tile>>();
            enemies = new List<AdvancedSprite>();
            NPCs = new List<AdvancedSprite>();
            people = new List<NonControlledCharacter>();
            objects = new List<BaseObject>();
            gridPos = Vector2.Zero;
            //player = new Engineer(Image.Particle, Location.Zero, 100);//TODO: remove
         
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

            startLocPlayer = Location.Zero;

            mapUpdate = new Thread(new ThreadStart(updateMap));
            mapUpdate.Start();
        }

        public Grid(int rows, int columns, Location locForPlayer)
        {
            map = new List<List<Tile>>();
            enemies = new List<AdvancedSprite>();
            NPCs = new List<AdvancedSprite>();
            people = new List<NonControlledCharacter>();
            gridPos = Vector2.Zero;
            //player = new Assassin(Image.Particle, Location.Zero, 100);//TODO: remove

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

            if (player != null)
            {
                player.setLocation(this, locForPlayer);
                startLocPlayer = locForPlayer;
            }

            mapUpdate = new Thread(new ThreadStart(updateMap));
            mapUpdate.Start();
        }

        public virtual void update(GameTime gameTime)
        {
            setCorners();

            player.update(gameTime, this);

            //pull info from the server for noncontrolled block

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].update(gameTime, this);
                }
            }

            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].update(gameTime, this);
                }
            }

            for (int i = 0; i < NPCs.Count; i++)
            {
                if (NPCs[i] != null)
                {
                    NPCs[i].update(gameTime, this);
                }
            }
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

            foreach (BaseObject b in objects)
            {
                if (b != null)
                    b.draw(spriteBatch);
            }
        }

        private void updateMap()
        {
            GameTime gameTime = new GameTime();

            while (Game1.Active)
            {
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        if (map[y][x] != null)
                        {
                            if (Game1.Camera != null)
                            {
                                if (Game1.Camera.IsInView(map[y][x].Position, map[y][x].Rec))
                                {
                                    map[y][x].update(gameTime, this);
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(16);
            }
        }

        public void changeStartLoc(Location newLoc)
        {
            startLocPlayer = newLoc;
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

            if (this != null)
            {
                for (int o = 0; o < objects.Count; o++)
                {
                    if (objects[o] != null)
                        objs.Add(objects[o]);
                }
            }

            return objs.ToArray();
        }

        public bool isValid(Location loc)
        {
            if (loc == null)
                return false;
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
