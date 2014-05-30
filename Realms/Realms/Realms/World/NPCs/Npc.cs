using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Npc : AdvancedSprite
    {
        protected Texture2D headShot;
        protected Form myForm;
        protected string NPCName, NPCTitle;

        public Texture2D HeadShot
        {
            get { return headShot; }
        }

        public Npc(Texture2D texture, Location startLoc)
            : base(texture, 0, startLoc)
        {
            NPCName = "Jill Reed";
            NPCTitle = "Awesome cat";
            interactions.Insert(0, "Talk");
            myForm = new Form(this, NPCName, NPCTitle, "Hey whats the big idea");
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (mouseHover)
            {
                spriteBatch.DrawString(Fonts.FormSubTitle, "Choose option: " + interactions[0],
                    new Vector2(0, 0) - Game1.Camera.Origin + Game1.Camera.Position, color);
            }
            base.draw(spriteBatch);
        }

        protected override void decideInteraction(string interaction, BaseCharacter player)
        {
            switch (interaction)
            {
                case "Talk":
                    myForm.changeOpen(true);
                    FormHandler.openForm(myForm);
                    break;
            }
            base.decideInteraction(interaction, player);
        }
    }
}
