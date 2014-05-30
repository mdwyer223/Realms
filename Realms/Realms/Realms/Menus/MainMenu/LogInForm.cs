using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class LogInForm : Form
    {
        public LogInForm()
            :base(null, "Log into the Game", "Enter your username and password", "")
        {
            headShotRec = new Rectangle(0, 0, 0, 0);
            titlePos.X = (displayRec.X + displayRec.Width - Fonts.FormTitle.MeasureString(title).X) / 2;
            subtitlePos.X = (displayRec.X + displayRec.Width - Fonts.FormTitle.MeasureString(subtitle).X) / 2;

            Textbox userName = new Textbox(true, new Vector2(displayRec.X + ((displayRec.Width / 2) - (displayRec.Width * .25f)), displayRec.Y + (displayRec.Height / 3)), .1f, "Username:", 30);
            controls.Add(userName);
            Textbox password = new Textbox(new Vector2(userName.Position.X, displayRec.Y + ((displayRec.Height / 3) * 2)), .1f, "Password:", 30);
            controls.Add(password);
        }
    }
}
