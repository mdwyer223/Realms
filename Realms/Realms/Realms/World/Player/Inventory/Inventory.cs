using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realms
{
    public class Inventory
    {
        List<Item> items;
        const int MAX = 200;

        public List<Item> ItemList
        {
            get {
                this.cleanUp();
                return items;
            }
        }

        public bool Full
        {
            get {return items.Count == MAX;}
        }

        public Inventory()
        {
            items = new List<Item>();
        }

        public Inventory(List<Item> prevItems)
        {
            items = prevItems;
        }

        public void cleanUp()
        {
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ItemCount <= 0)
                    {
                        items.RemoveAt(i);
                    }
                }
            }
        }

        public bool addItem(Item item)
        {
            if (items.Count == 0)
            {
                items.Add(item);
                return true;
            }

            bool sameItem = false;
            int currentIndex = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (item.GetType() == items[i].GetType())
                {
                    if (!items[i].Maxed)
                    {
                        sameItem = true;
                        currentIndex = i;
                    }
                }
            }

            if (sameItem)
            {
                int count = items[currentIndex].increaseCount(item.ItemCount);
                if (count != 0)
                {
                    item.setCount(count);
                    if (items.Count < MAX)
                    {
                        items.Add(item);
                    }
                }
                return true;
            }
            else
            {
                if (items.Count < MAX)
                {
                    items.Add(item);
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
