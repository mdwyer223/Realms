using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public abstract class Item : BaseSprite
    {
        protected List<string> options;
        protected string name;
        protected int count, capacity;
        protected bool battleItem;

        public string Name
        { 
            get { return name; }
            protected set
            {
                name = value;
            }
        }

        public int ItemCount
        {
            get { return count; }
            protected set { count = value; }
        }

        public int MaxCount
        {
            get { return capacity; }
        }

        public bool Maxed
        {
            get { return count == capacity; }
        }

        public bool BattleItem
        {
            get { return battleItem; }
        }

        public Item(Texture2D texture, float scaleFactor, string name, bool battleItem)
            : base(texture, scaleFactor, 0, Vector2.Zero)
        {
            this.name = name;
            this.battleItem = battleItem;
            options = new List<string>();

            options.Add("Use");
            options.Add("Drop");

            capacity = 99;
            count = 1;
        }

        public virtual int increaseCount(int num)
        {
            if (num + count <= capacity)
            {
                count += num;
                return 0;
            }
            else
            {
                int leftover = (count + num) - capacity;
                count = capacity;
                return leftover;
            }
        }

        public virtual void setCount(int n)
        {
            count = n;
            if (count >= capacity)
                count = capacity;
        }

        public List<string> getOptions()
        {
            return options;
        }

        public virtual void chooseOption(string option, BaseCharacter player)//needs a player
        {
            switch (option)
            {
                case "Drop":
                    this.count--;
                    break;
            }
        }
    }
}
