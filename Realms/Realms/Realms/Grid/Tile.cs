using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    //This class is the parent for all thing in the 
    public class Tile : AnimatedSprite
    {
        public const int T_HEIGHT = 32, T_WIDTH = 32;
        protected Location loc, oldLoc;
        int width = 1, height = 1;

        public bool IsDead
        {
            get;
            protected set;
        }

        public bool Open
        {
            get;
            set;
        }

        public int Width
        {
            get { return width; }
            protected set
            {
                width = value;
                rec.Width = width * T_WIDTH;
            }
        }

        public int Height
        {
            get { return height; }
            protected set
            {
                height = value;
                rec.Height = height * T_HEIGHT;
            }

        }

        public Location Location
        {
            get { return loc; }
            protected set
            {
                loc = value;
                Position = new Vector2((int)loc.Column * T_WIDTH, (int)loc.Row * T_HEIGHT);
            }
        }

        public Tile(Texture2D tex, float secondsToCrossScreen, Location loc)
            : base(tex, 0, secondsToCrossScreen, Vector2.Zero)
        {
            Rec = new Rectangle((int)loc.Column * T_WIDTH, (int)loc.Row * T_HEIGHT, T_WIDTH, T_HEIGHT);
            this.loc = this.oldLoc = loc;
            Open = this.GetType() == typeof(Tile);
        }

        public Tile(Texture2D tex, float secondsToCrossScreen, Location loc, bool open)
            : base(tex, 0, secondsToCrossScreen, Vector2.Zero)
        {
            Rec = new Rectangle((int)loc.Column * T_WIDTH, (int)loc.Row * T_HEIGHT, T_WIDTH, T_HEIGHT);
            this.loc = this.oldLoc = loc;
            this.Open = open;
        }

        public sealed override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }

        public virtual void update(GameTime gameTime, Grid gr)
        {
            Tile[] objects = gr.getObjects();

            if (!IsDead)
            {
                bool intersect = false; 
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i] != null && !objects[i].IsDead && objects[i].GetType() != typeof(TripWire))
                    {
                        if (this.isColliding(objects[i].Rec))
                        {
                            intersect = true;
                        }
                    }
                }
                if (intersect)
                {
                    Open = false;
                    this.color = Color.Red;
                }
                else
                {
                    Open = true;
                    this.color = Color.Green;
                }
            }
        }
    }
}
