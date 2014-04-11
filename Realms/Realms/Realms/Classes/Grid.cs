using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Grid
    {
        List<List<Tile>> map;
        List<BaseSprite> everything;

        int rows, columns;

        public int Rows
        {
            get{return rows;}
        }

        public int Columns
        {
            get{return columns;}
        }

        public Grid(int rows, int columns)
        {
            float scale = .03f; // to be determined
            for (int y = 0; y < rows; y++)
            {
                map[y] = new List<Tile>();
                for (int x = 0; x<columns; x++)
                {
                    map[y][x] = new Tile(new Location(y,x), scale, null);
                }
            }
        }
    }
}
