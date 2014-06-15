using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Realms
{
    public class Input
    {
        protected static KeyboardState keys, oldKeys;
        protected static MouseState mouse, oldMouse;
        protected static string currentString;
        protected static int mouseWidth = 15, mouseHeight = 15,
            doubleClickTimer = 0, clickCounter = 0;

        const int MAX_CLICK_WAIT = 15;
        static bool timing;

        private static bool actionBar, right, left, up, down, escape, back, leftClick, rightClick,
            doubleClicked, rightHit, leftHit, upHit, downHit;
  
        public static bool rightDown()
        {
            return right;
        }

        public static bool leftDown()
        {
            return left;
        }

        public static bool upDown()
        {
            return up;
        }

        public static bool downDown()
        {
            return down;
        }

        public static bool rightPressed()
        {
            return rightHit;
        }

        public static bool leftPressed()
        {
            return leftHit;
        }

        public static bool upPressed()
        {
            return upHit;
        }

        public static bool downPressed()
        {
            return downHit;
        }

        public static bool escapePressed()
        {
            return escape;
        }

        public static bool backPressed()
        {
            return back;
        }

        public static string getKeysPressed()
        {
            string pressedKeys = "";
            string keyString = "";
            Keys[] press = keys.GetPressedKeys(), oldPress = oldKeys.GetPressedKeys();

            for (int i = 0; i < keys.GetPressedKeys().Length; i++)
            {
                bool foundCharacter = false;

                for (int j = 0; j < oldKeys.GetPressedKeys().Length; j++)
                {
                    if (oldPress != null)
                    {
                        if (press[i] == oldPress[j])
                        {
                            foundCharacter = true;
                            break;
                        }
                    }
                }
                if (foundCharacter == false)
                {
                    switch (press[i])
                    {
                        case Keys.D0:
                            keyString = "0";
                            break;
                        case Keys.D1:
                            keyString = "1";
                            break;
                        case Keys.D2:
                            keyString = "2";
                            break;
                        case Keys.D3:
                            keyString = "3";
                            break;
                        case Keys.D4:
                            keyString = "4";
                            break;
                        case Keys.D5:
                            keyString = "5";
                            break;
                        case Keys.D6:
                            keyString = "6";
                            break;
                        case Keys.D7:
                            keyString = "7";
                            break;
                        case Keys.D8:
                            keyString = "8";
                            break;
                        case Keys.D9:
                            keyString = "9";
                            break;
                        case Keys.A:
                            keyString = "A";
                            break;
                        case Keys.B:
                            keyString = "B";
                            break;
                        case Keys.C:
                            keyString = "C";
                            break;
                        case Keys.D:
                            keyString = "D";
                            break;
                        case Keys.E:
                            keyString = "E";
                            break;
                        case Keys.F:
                            keyString = "F";
                            break;
                        case Keys.G:
                            keyString = "G";
                            break;
                        case Keys.H:
                            keyString = "H";
                            break;
                        case Keys.I:
                            keyString = "I";
                            break;
                        case Keys.J:
                            keyString = "J";
                            break;
                        case Keys.K:
                            keyString = "K";
                            break;
                        case Keys.L:
                            keyString = "L";
                            break;
                        case Keys.M:
                            keyString = "M";
                            break;
                        case Keys.N:
                            keyString = "N";
                            break;
                        case Keys.O:
                            keyString = "O";
                            break;
                        case Keys.P:
                            keyString = "P";
                            break;
                        case Keys.Q:
                            keyString = "Q";
                            break;
                        case Keys.R:
                            keyString = "R";
                            break;
                        case Keys.S:
                            keyString = "S";
                            break;
                        case Keys.T:
                            keyString = "T";
                            break;
                        case Keys.U:
                            keyString = "U";
                            break;
                        case Keys.W:
                            keyString = "W";
                            break;
                        case Keys.V:
                            keyString = "V";
                            break;
                        case Keys.X:
                            keyString = "X";
                            break;
                        case Keys.Y:
                            keyString = "Y";
                            break;
                        case Keys.Z:
                            keyString = "Z";
                            break;
                        case Keys.Space:
                            keyString = " ";
                            break;
                        case Keys.OemComma:
                            keyString = ",";
                            break;
                        case Keys.OemPeriod:
                            keyString = ".";
                            break;
                    }
                    if (keys.IsKeyUp(Keys.LeftShift) && keys.IsKeyUp(Keys.RightShift))
                    {
                        keyString = keyString.ToLower();
                    }
                }
            }

            pressedKeys = keyString;

             
            return pressedKeys;
        }

        public static string getRecentKeys()
        {
            return currentString;
        }

        public static bool actionBarPressed()
        {
            return actionBar;
        }

        public static bool leftMouseClick()
        {
            return leftClick;
        }

        public static bool rightMouseClick()
        {
            return rightClick;
        }

        public static bool doubleClick()
        {
            return doubleClicked;
        }

        public static Vector2 mousePos()
        {
            return (new Vector2(mouse.X, mouse.Y));
        }

        public static Rectangle mouseDrawnRec()
        {
            return (new Rectangle((int)mousePos().X, (int)mousePos().Y, mouseWidth, mouseHeight));
        }

        public static Rectangle mouseCollisionRec()
        {
            return (new Rectangle((int)(mousePos().X - Game1.Camera.Origin.X + Game1.Camera.Position.X), 
                (int)(mousePos().Y - Game1.Camera.Origin.Y + Game1.Camera.Position.Y), mouseWidth, mouseHeight));
        }

        public static void Update()
        {
            keys = Keyboard.GetState();
            mouse = Mouse.GetState();

            if ((keys.IsKeyDown(Keys.D) //&& oldKeys.IsKeyUp(Keys.D))
                || (keys.IsKeyDown(Keys.Right)))) //&& oldKeys.IsKeyUp(Keys.Right)))
            {
                right = true;
            }
            else
                right = false;

            if ((keys.IsKeyDown(Keys.A) //&& oldKeys.IsKeyUp(Keys.A))
                || (keys.IsKeyDown(Keys.Left)))) //&& oldKeys.IsKeyUp(Keys.Left)))
                left = true;
            else
                left = false;

            if ((keys.IsKeyDown(Keys.W) //&& oldKeys.IsKeyUp(Keys.W))
                || (keys.IsKeyDown(Keys.Up)))) //&& oldKeys.IsKeyUp(Keys.Up)))
                up = true;
            else
                up = false;

            if ((keys.IsKeyDown(Keys.S) //&& oldKeys.IsKeyUp(Keys.S))
                || (keys.IsKeyDown(Keys.Down)))) //&& oldKeys.IsKeyUp(Keys.Down)))
                down = true;
            else
                down = false;

            if ((keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
                || (keys.IsKeyDown(Keys.Right) && oldKeys.IsKeyUp(Keys.Right)))
            {
                rightHit = true;
            }
            else
                rightHit = false;

            if ((keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
                || (keys.IsKeyDown(Keys.Left) && oldKeys.IsKeyUp(Keys.Left)))
                leftHit = true;
            else
                leftHit = false;

            if ((keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                || (keys.IsKeyDown(Keys.Up) && oldKeys.IsKeyUp(Keys.Up)))
                upHit = true;
            else
                upHit = false;

            if ((keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                || (keys.IsKeyDown(Keys.Down) && oldKeys.IsKeyUp(Keys.Down)))
                downHit = true;
            else
                downHit = false;

            if ((keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape)))
                escape = true;
            else
                escape = false;

            if ((keys.IsKeyDown(Keys.Back) && oldKeys.IsKeyUp(Keys.Back)))
                back = true;
            else
                back = false;

            if ((keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                || (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter)))
                actionBar = true;
            else
                actionBar = false;

            if ((mouse.LeftButton == ButtonState.Pressed
                && oldMouse.LeftButton == ButtonState.Released))
                leftClick = true;
            else
                leftClick = false;

            if ((mouse.RightButton == ButtonState.Pressed
                && oldMouse.RightButton == ButtonState.Released))
                rightClick = true;
            else
                rightClick = false;

            if (leftClick && !timing)
            {
                timing = true;
            }

            if (timing)
            {
                doubleClickTimer++;
                if (leftClick)
                    clickCounter++;

                if (clickCounter >= 2)
                {
                    doubleClicked = true;
                    doubleClickTimer += 100;
                }

                if (doubleClickTimer > MAX_CLICK_WAIT)
                {
                    timing = false;
                    doubleClickTimer = 0;
                    clickCounter = 0;
                }
            }
            else
                doubleClicked = false;

            currentString = getKeysPressed();
            oldKeys = keys;
            oldMouse = mouse;
        }
    } 
}
