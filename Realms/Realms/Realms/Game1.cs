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
        FormHandler forms;
        MainMenu mainMenu;
        PauseMenu pauseMenu;
        BattleHandler battleComp;
        QuestHandler qHandler;

        string connectionTest = "", keyPress = "";

        NonControlledBlock nB;
        ControlledBlock b;

        static Battle nextBattle;
        static Grid nextMap;
        static bool changedState = false;

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

        static GameState mainGameState = GameState.MAINMENU;
        public static GameState State
        {
            get { return mainGameState; }
        }

        static Camera2D camera;
        public static Camera2D Camera
        {
            get { return camera; }
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
            //nB = new NonControlledBlock(server);
            //b = new ControlledBlock(server);

            world = new World(this);
            Components.Add(world);

            forms = new FormHandler(this);
            Components.Add(forms);

            mainMenu = new MainMenu(this);
            Components.Add(mainMenu);

            pauseMenu = new PauseMenu(this);
            Components.Add(pauseMenu);

            battleComp = new BattleHandler(this);
            Components.Add(battleComp);

            qHandler = new QuestHandler(this);
            Components.Add(qHandler);

            camera = new Camera2D(this);
            Components.Add(camera);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            qHandler.getBattle(battleComp.CurrentBattle);
            qHandler.getForm(forms.FormOpen);
            qHandler.getPlayer(world.Grid.Player);

            if (changedState)
            {
                if (State == GameState.BATTLE)
                {
                    world.Grid.changeStartLoc(world.Grid.Player.Location);
                    battleComp.activateBattle(nextBattle, world.Grid);
                }
                else if (State == GameState.PLAYING)
                {
                    world.changeMap(nextMap);
                }

                changedState = false;
            }

            if (mainGameState == GameState.PLAYING)
            {
                world.Enabled = world.Visible = true;
                mainMenu.Enabled = mainMenu.Visible = false;
                pauseMenu.Enabled = pauseMenu.Visible = false;
                battleComp.Visible = battleComp.Enabled = false;
            }
            else if (mainGameState == GameState.MAINMENU)
            {
                mainMenu.Enabled = mainMenu.Visible = true;
                world.Enabled = world.Visible = false;
                pauseMenu.Enabled = pauseMenu.Visible = false;
                battleComp.Visible = battleComp.Enabled = false;
            }
            else if (mainGameState == GameState.DEATH)
            {
                // not sure here.. gotta figure it out
            }
            else if (mainGameState == GameState.PAUSE)
            {
                pauseMenu.Enabled = pauseMenu.Visible = true;
                world.Enabled = world.Visible = false;
                mainMenu.Enabled = mainMenu.Visible = false;
                battleComp.Visible = battleComp.Enabled = false;
            }
            else if (mainGameState == GameState.BATTLE)
            {
                battleComp.Visible = battleComp.Enabled = true;
                world.Enabled = world.Visible = false;
                mainMenu.Enabled = mainMenu.Visible = false;
                pauseMenu.Enabled = pauseMenu.Visible = false;
            }

            //keyPress += Input.getRecentKeys();
            //if (Input.backPressed())
            //{
                //if (keyPress.Length > 0)
                    //keyPress = keyPress.Remove(keyPress.Length - 1, 1);
            //}
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);


            spriteBatch.Begin();
            spriteBatch.Draw(Image.Cursor, Input.mouseDrawnRec(), Color.White);
            //nB.Draw(spriteBatch);
            //b.Draw(spriteBatch);
            spriteBatch.End();  
        }

        public static void activateBattle(Battle b)
        {
            // world change to battle scene
            changeState(GameState.BATTLE);
            nextBattle = b;
        }

        public static void changeWorld(Grid g)
        {
            changeState(GameState.PLAYING);
            nextMap = g;
        }

        public static void changeActive()
        {
            active = !active;
        }

        public static void changeState(GameState newState)
        {
            mainGameState = newState;
            changedState = true;
        }
        
    }
}
