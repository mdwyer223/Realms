using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        protected MySqlConnection connection;
        protected int ID;

        public Block(MySqlConnection currentConnection)
        {
            texture = Game1.GameContent.Load<Texture2D>("Particle");
            this.connection = currentConnection;
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

        public NonControlledBlock(MySqlConnection current)
            : base(current)
        {
            color = Color.Red;
            ID = 1;

            cmd = new MySqlCommand("getPos", current);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Prepare();
            cmd.Parameters.Add(new MySqlParameter("idForProcess",ID));
            cmd.Parameters.Add(new MySqlParameter("x", x));
            cmd.Parameters["x"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters.Add(new MySqlParameter("y", y));
            cmd.Parameters["y"].Direction = System.Data.ParameterDirection.InputOutput;
            //cmd.ExecuteNonQuery();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            x = (int)dataReader[0];
            y = (int)dataReader[1];
            dataReader.Close();
            rec.X = x;
            rec.Y = y;
        }

        public override void Update(GameTime gametime)
        {
            //get position from the database itself

            base.Update(gametime);
        }
    }

    public class ControlledBlock : Block
    {
        protected KeyboardState keys, oldKeys;
        protected Vector2 pos, oldPos;
        int delay = 40, delayTimer = 0;
        bool separate;

        public ControlledBlock(MySqlConnection current)
            : base(current)
        {
            color = Color.Green;
            keys = oldKeys = Keyboard.GetState();
            pos = oldPos = new Vector2(rec.X, rec.Y);
            ID = 2;
        }
        
        public override void Update(GameTime gametime)
        {
            if (!separate)
            {
                rec.X += 200;
                separate = true;
            }

            keys = Keyboard.GetState();
            pos = new Vector2(rec.X, rec.Y);

            if (delayTimer < delay)
            {
                delayTimer++;
                if (delayTimer > delay)
                {
                    delayTimer = delay;
                }
            }

            if (pos != oldPos && delayTimer == delay)
            {
                delayTimer = 0;
                //update on the server/db
                MySqlCommand cmd = new MySqlCommand("setPos", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("idForP", ID));
                cmd.Parameters.Add(new MySqlParameter("x", rec.X));
                cmd.Parameters.Add(new MySqlParameter("y", rec.Y));
                cmd.ExecuteNonQuery();
            }

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
    }
}
