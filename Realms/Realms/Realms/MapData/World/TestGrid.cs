using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class TestGrid : Grid
    {
        public TestGrid()
            : base(100, 100, new Location(0, 5))
        {
            objects.Add(new TripWire(Image.Particle, new Location(20, 40), "testdungeon"));
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
}
