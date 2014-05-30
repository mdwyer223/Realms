using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Fonts
    {
        public static SpriteFont Normal
        {
            get { return Game1.GameContent.Load<SpriteFont>("Normal"); }
        }

        public static SpriteFont BattleMenu
        {
            get { return Game1.GameContent.Load<SpriteFont>("BattleFonts/BattleMenu"); }
        }

        public static SpriteFont BattleMessage
        {
            get { return Game1.GameContent.Load<SpriteFont>("BattleFonts/BattleMessage"); }
        }

        public static SpriteFont FormBody
        {
            get { return Game1.GameContent.Load<SpriteFont>("FormFonts/FormBody"); }
        }

        public static SpriteFont FormSubTitle
        {
            get { return Game1.GameContent.Load<SpriteFont>("FormFonts/FormSubTitle"); }
        }

        public static SpriteFont FormTitle
        {
            get { return Game1.GameContent.Load<SpriteFont>("FormFonts/FormTitle"); }
        }

        // other Spritefonts...
    }
}
