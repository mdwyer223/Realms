using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class TestDungeon : Dungeon
    {
        public TestDungeon(int rows, int columns)
            : base(rows, columns, Location.Zero)
        {
            objects.Add(new Building(Image.Particle, new Location(3, 3), 2, 2, 2, 4));
            objects.Add(new Chest(Image.Particle, new Location(5, 5), 1, 1));
            objects.Add(new TripWire(Image.Particle, new Location(5, 7), "testgrid"));
            enemies.Add(new BaseEnemy(Image.Particle, 0, new Location(7, 5), EnemyType.LIGHT, 10));
        }

        public override void generateEnemies()
        {
            throw new NotImplementedException();
        }

        public override void generateLandscape()
        {
            throw new NotImplementedException();
        }
    }
}
