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
        const int T_HEIGHT = 32, T_WIDTH = 32;

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

        public Tile(Location loc, Texture2D tex)
            :base  (loc.Position, 0, tex)
        {
            rec = new Rectangle((int)loc.Column * T_WIDTH, (int)loc.Row * T_HEIGHT, T_WIDTH, T_HEIGHT);
            position = new Vector2((int)loc.Column * T_WIDTH, (int)loc.Row * T_HEIGHT);
            this.loc = loc;
            open = true;

            //load texture
        }

        public Tile(Location loc, Texture2D tex, bool open)
            : base(loc.Position, 0, tex)
        {
            rec = new Rectangle((int)loc.Column * T_WIDTH, (int)loc.Row * T_HEIGHT, T_WIDTH, T_HEIGHT);
            position = new Vector2((int)loc.Column * T_WIDTH, (int)loc.Row * T_HEIGHT);
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
                        if (this.isColliding(objects[i].Rec))
                        {
                            intersect = true;
                        }
                    }
                }
                if (intersect)
                {
                    open = false;
                    this.color = Color.Red;
                }
                else
                {
                    open = true;
                    this.color = Color.Green;
                }
            }
        }
    }
}
