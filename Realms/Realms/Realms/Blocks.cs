using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MySql.Data.MySqlClient;

namespace Realms
{
    public class Block
    {
        private Texture2D texture;
        protected Rectangle rec;
        protected Color color;
        protected Server server;
        protected int ID;

        public Block(Server currentConnection)
        {
            texture = Game1.GameContent.Load<Texture2D>("Particle");
            this.server = currentConnection;
            color = Color.White;
            this.rec = new Rectangle(0,0, 10,10);
        }

        public virtual void Update(GameTime gametime)
        {
        }
        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rec, color);
        }
    }

    public class NonControlledBlock : Block
    {
        protected MySqlCommand cmd;
        protected MySqlDataReader reader;
        protected int x, y;

        Vector2 velo;

        Vector2 oldPos, newPos, currentPos;

        Thread tGetPos;

        int delay = 40, delayTimer = 0;

        public NonControlledBlock(Server current)
            : base(current)
        {
            color = Color.Red;
            ID = 2;

            newPos = oldPos = currentPos = getInitialPos();
            velo = Vector2.Zero;

            rec.X = (int)(oldPos.X);
            rec.Y = (int)(oldPos.Y);

            tGetPos = new Thread(new ThreadStart(getPos));

            tGetPos.Start();
        }

        public override void Update(GameTime gametime)
        {
            //if (Math.Abs(currentPos.X - newPos.X) >= 3 && Math.Abs(currentPos.Y - newPos.Y) >= 3)
                currentPos += velo;

            rec.X = (int)(currentPos.X + .5f);
            rec.Y = (int)(currentPos.Y + .5f);
            base.Update(gametime);
        }

        private void getPos()
        {
            while (Game1.Active)
            {
                if (currentPos != oldPos)
                {
                    //velo = (currentPos - oldPos);
                    //if (velo != Vector2.Zero)
                    //    velo.Normalize();
                }
                oldPos = newPos;
                MySqlConnection c = server.getConn();
                c.Open();
                cmd = new MySqlCommand("getPos", c);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Prepare();
                cmd.Parameters.Add(new MySqlParameter("idForProcess", ID));
                cmd.Parameters.Add(new MySqlParameter("x", x));
                cmd.Parameters["x"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters.Add(new MySqlParameter("y", y));
                cmd.Parameters["y"].Direction = System.Data.ParameterDirection.InputOutput;

                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                newPos.X = (int)dataReader[0];
                newPos.Y = (int)dataReader[1];
                dataReader.Close();
                c.Close();
                //newPos = new Vector2(0, 60);
                //oldPos = Vector2.Zero;
                if (Math.Abs(currentPos.X - newPos.X) >= 5|| Math.Abs(currentPos.Y - newPos.Y) >= 5)
                {
                    velo = (newPos - currentPos);
                    if (velo != Vector2.Zero)
                    {
                        velo.Normalize();
                    }
                }
                else
                    velo = Vector2.Zero;

                Thread.Sleep(50);
            }
        }
        private Vector2 getInitialPos()
        {
            MySqlConnection c = server.getConn();
            c.Open();
            cmd = new MySqlCommand("getPos", c);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Prepare();
            cmd.Parameters.Add(new MySqlParameter("idForProcess", ID));
            cmd.Parameters.Add(new MySqlParameter("x", x));
            cmd.Parameters["x"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters.Add(new MySqlParameter("y", y));
            cmd.Parameters["y"].Direction = System.Data.ParameterDirection.InputOutput;
               
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            x = (int)dataReader[0];
            y = (int)dataReader[1];           
            dataReader.Close();                
            c.Close();
                
            return new Vector2(x, y);
        }
    }

    public class ControlledBlock : Block
    {
        protected MySqlCommand cmd;
        protected KeyboardState keys, oldKeys;
        protected Vector2 pos, oldPos;
        //int delay = 60, delayTimer = 0;

        Thread posThread;
        int problems;


        public ControlledBlock(Server current)
            : base(current)
        {
            color = Color.Green;
            keys = oldKeys = Keyboard.GetState();
            
            ID = 1;

            getPos();

            pos = oldPos = new Vector2(rec.X, rec.Y);

            posThread = new Thread(new ThreadStart(updatePos));
            posThread.Start();
        }
        
        public override void Update(GameTime gametime)
        {
            keys = Keyboard.GetState();
            pos = new Vector2(rec.X, rec.Y);

            if (keys.IsKeyDown(Keys.W))
                rec.Y -= 1;
            if (keys.IsKeyDown(Keys.S))
                rec.Y += 1;
            if (keys.IsKeyDown(Keys.D))
                rec.X += 1;
            if (keys.IsKeyDown(Keys.A))
                rec.X -= 1;

            oldKeys = keys;
            oldPos = pos;
            base.Update(gametime);
        }

        private void updatePos()
        {
            while (Game1.Active)
            {
                MySqlConnection c = server.getConn();
                c.Open();
                MySqlCommand cmd = new MySqlCommand("setPos", c);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("idForP", ID));
                cmd.Parameters.Add(new MySqlParameter("x", rec.X));
                cmd.Parameters.Add(new MySqlParameter("y", rec.Y));
                
                cmd.ExecuteNonQuery();
                
                c.Close();
                Thread.Sleep(50);
            }
        }

        private void getPos()
        {
            int x = 0, y = 0;

            MySqlConnection c = server.getConn();
            c.Open();
            cmd = new MySqlCommand("getPos", c);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Prepare();
            cmd.Parameters.Add(new MySqlParameter("idForProcess", ID));
            cmd.Parameters.Add(new MySqlParameter("x", x));
            cmd.Parameters["x"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters.Add(new MySqlParameter("y", y));
            cmd.Parameters["y"].Direction = System.Data.ParameterDirection.InputOutput;
            
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            x = (int)dataReader[0];
            y = (int)dataReader[1];
            dataReader.Close();
            
            rec.X = x;
            rec.Y = y;
            c.Close();
        }
    }
}
