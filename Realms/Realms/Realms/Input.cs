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
  
        public static bool rightPressed()
        {
            return (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
                || (keys.IsKeyDown(Keys.Right) && oldKeys.IsKeyUp(Keys.Right));
        }

        public static bool leftPressed()
        {
            return (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
                || (keys.IsKeyDown(Keys.Left) && oldKeys.IsKeyUp(Keys.Left));
        }

        public static bool upPressed()
        {
            return (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                || (keys.IsKeyDown(Keys.Up) && oldKeys.IsKeyUp(Keys.Up));
        }

        public static bool downPressed()
        {
            return (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                || (keys.IsKeyDown(Keys.Down) && oldKeys.IsKeyUp(Keys.Down));
        }

        public static bool escapePressed()
        {
            return (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape));
        }

        public static string getKeysPressed()
        {
            string pressedKeys = "";

            return pressedKeys;
        }

        public static bool actionBarPressed()
        {
            return (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                || (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter));
        }

        public static bool leftMouseClick()
        {
            return (mouse.LeftButton == ButtonState.Pressed 
                && oldMouse.LeftButton == ButtonState.Released);
        }

        public static bool rightMouseClick()
        {
            return (mouse.RightButton == ButtonState.Released 
                && oldMouse.RightButton == ButtonState.Released);
        }

        public static Vector2 mousePos()
        {
            return (new Vector2(mouse.X, mouse.Y));
        }

        public void Update()
        {
            keys = Keyboard.GetState();
            mouse = Mouse.GetState();

            oldKeys = keys;
            oldMouse = mouse;
        }
    } 
}
