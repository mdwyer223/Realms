using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class Tile : BaseSprite
    {
        Location loc;
        bool open;

        public bool Open
        {
            get { return open; }
            protected set { open = value; }
        }

        public Location Location
        {
            get { return loc; }
        }

        public Tile(Location loc, float scaleFactor, Texture2D tex)
            :base  (loc.Position, scaleFactor, tex)
        {
            this.loc = loc;
            open = true;

            //load texture
        }

        public Tile(Location loc, float scaleFactor, Texture2D tex, bool open)
            : base(loc.Position, scaleFactor, tex)
        {
            this.loc = loc;
            this.open = open;
        }

        public virtual void Update(GameTime gameTime, List<BaseSprite> objects)
        {
            if (!IsDead)
            {
                bool intersect = false; 
                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i] != null && !objects[i].IsDead)
                    {
                        //decide if open or not
                        //location?
                    }
                }
                if (intersect)
                {
                    open = false;
                }
            }
        }
    }
}
