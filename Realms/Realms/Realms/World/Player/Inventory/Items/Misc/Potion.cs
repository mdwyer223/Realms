using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Potion : Miscellanious
    {
        protected int healingPower;
        public Potion()
            : base(Image.Particle, .02f, "Potion", true)
        {
            healingPower = 500;
            options.Add("Use");
        }

        public override void chooseOption(string option, BaseCharacter player)
        {
            switch (option)
            {
                case "Use":
                    //heal the player
                    break;
            }
            base.chooseOption(option, player);
        }

        public override void chooseOption(string option, BattleSprite target)
        {
            switch (option)
            {
                case "Use":
                    target.heal(healingPower);
                    count--;
                    break;
            }
            base.chooseOption(option, target);
        }
    }
}
