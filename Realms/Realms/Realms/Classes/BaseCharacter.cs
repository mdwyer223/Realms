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
    public class BaseCharacter : AdvancedSprite
    {
        public BaseCharacter(Location loc, float scaleFactor, Texture2D tex)
            : base(loc, scaleFactor, tex)
        {

        }

        public override void Update(GameTime gameTime, Grid map)
        {
            position = new Vector2(loc.Column * 32, loc.Row * 32);
            if (Input.rightPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Column++;
                if (map.validLocation(test))
                {
                    loc = test;
                    //play anime right
                }
            }
            else if (Input.leftPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Column--;
                if (map.validLocation(test))
                {
                    loc = test;
                    //play anime right
                }
            }
            else if (Input.upPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Row--;
                if (map.validLocation(test))
                {
                    loc = test;
                    //play anime right
                }
            }
            else if (Input.downPressed())
            {
                Location test = new Location(loc.Row, loc.Column);
                test.Row++;
                if (map.validLocation(test))
                {
                    loc = test;
                    //play anime right
                }
            }
            base.Update(gameTime, map);

            oldLoc = loc;
        }
    }
}
