using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public abstract class Dungeon : Grid
    {
        public Dungeon(int rows, int columns, Location locForPlayer)
            : base(rows, columns, locForPlayer)
        {
            objects = new List<BaseObject>();
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }

        public abstract void generateLandscape();

        public abstract void generateEnemies();
    }
}
