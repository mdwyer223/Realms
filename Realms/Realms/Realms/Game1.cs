using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MySql.Data.MySqlClient;

namespace Realms
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Server server;

        string connectionTest = "";

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //string cs = "server=mysql.chung.special-topics.net;uid=dchung14;pwd=Chung6616;database=chungdb";

            server = new Server("dchung14", "Chung6616", "chungdb", "mysql.chung.special-topics.net");

            //server.changeDB("chungdb");

            this.connectionTest = server.Message;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(Content.Load<SpriteFont>("Normal"), connectionTest, new Vector2(5, 5), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
