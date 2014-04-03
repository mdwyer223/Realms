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

        public Block(MySqlConnection currentConnection)
        {
            Game1.GameContent.Load<Texture2D>("Particle");
            this.connection = currentConnection;
            color = Color.White;
            this.rec = new Rectangle(100,100, 10,10);
        }

        public virtual void Update(GameTime gametime);
        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rec, color);
        }
    }

    public class NonControlledBlock : Block
    {
        protected MySqlCommand cmd;

        public NonControlledBlock(MySqlConnection current)
            : base(current)
        {
            color = Color.Red;
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

        bool separate;

        public ControlledBlock(MySqlConnection current)
            : base(current)
        {
            color = Color.Green;
            keys = oldKeys = Keyboard.GetState();
            pos = oldPos = new Vector2(rec.X, rec.Y);
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

            if (pos != oldPos)
            {
                //update on the server/db
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
