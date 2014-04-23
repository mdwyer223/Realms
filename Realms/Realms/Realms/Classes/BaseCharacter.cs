using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Realms
{
    //This class allows code reuse between different ...classes
    public abstract class BaseCharacter : AdvancedSprite
    {
        public BaseCharacter(Texture2D texture, float secondsToCrossScreen, Location startLoc)
            : base(texture, secondsToCrossScreen, startLoc)
        {
        }

        public override void update(GameTime gameTime, Grid map)
        {
            if (Input.rightPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Column++;
                if (map.isValid(test))
                {
                    loc = test;
                    //play anime right
                }
            }
            else if (Input.leftPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Column--;
                if (map.isValid(test))
                {
                    loc = test;
                    //play anime left
                }
            }
            else if (Input.upPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Row--;
                if (map.isValid(test))
                {
                    loc = test;
                    //play anime up
                }
            }
            else if (Input.downPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Row++;
                if (map.isValid(test))
                {
                    loc = test;
                    //play anime down
                }
            }
            base.update(gameTime, map);

            oldLoc = loc;
        }
    }
}
