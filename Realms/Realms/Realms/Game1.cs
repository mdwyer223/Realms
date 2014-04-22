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
        World world;

        string connectionTest = "", keyPress = "";

        NonControlledBlock nB;
        ControlledBlock b;

        static ContentManager otherContent;
        public static ContentManager GameContent
        {
            get { return otherContent; }
        }

        static GraphicsDevice otherDevice;
        public static Viewport View
        {
            get { return otherDevice.Viewport; }
        }

        static bool active = true;
        public static bool Active
        {
            get { return active; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            otherContent = new ContentManager(Content.ServiceProvider);
            otherContent.RootDirectory = "Content";

            //string cs = "server=mysql.chung.special-topics.net;uid=dchung14;pwd=Chung6616;database=chungdb";

            server = new Server("dchung14", "Chung6616", "chungdb", "mysql.chung.special-topics.net");

            this.connectionTest = server.Message;
        }

        protected override void Initialize()
        {
            otherDevice = GraphicsDevice;
            nB = new NonControlledBlock(server);
            b = new ControlledBlock(server);

            world = new World(this);
            Components.Add(world);
            world.Enabled = world.Visible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            keyPress += Input.getRecentKeys();
            if (Input.backPressed())
            {
                if (keyPress.Length > 0)
                    keyPress = keyPress.Remove(keyPress.Length - 1, 1);
            }
            b.Update(gameTime);
            nB.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(Content.Load<SpriteFont>("Normal"), connectionTest, new Vector2(5, 5), Color.White);
            spriteBatch.DrawString(Content.Load<SpriteFont>("Normal"), keyPress, new Vector2(5, 15), Color.Red);
            nB.Draw(spriteBatch);
            b.Draw(spriteBatch);
            spriteBatch.End();  
        }

        public static void changeActive()
        {
            active = !active;
        }

        
    }
}
