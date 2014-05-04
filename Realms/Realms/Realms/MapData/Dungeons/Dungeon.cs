using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms.MapData.Dungeons
{
    public abstract class Dungeon : Grid
    {
        List<BaseObject> objects;

        public Dungeon(int rows, int columns)
            : base(rows, columns)
        {

        }

        public abstract void generateLandscape();

        public abstract void generateEnemies();

        public override Tile[] getObjects()
        {
            
            
            return base.getObjects();
        }
    }
}
