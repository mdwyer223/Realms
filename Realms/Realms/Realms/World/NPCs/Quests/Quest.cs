using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realms
{
    public abstract class Quest
    {
        protected List<Item> itemsToGive;
        protected int exp, money;
        protected string name;

        public List<Item> AwardedItems
        {
            get { return itemsToGive; }
        }

        public int AwardedExp
        {
            get { return exp; }
        }

        public int AwardedMoney
        {
            get { return money; }
        }

        public string QuestName
        { 
            get { return name; } 
        }

        public bool Completed
        {
            get;
            protected set;
        }

        public Quest(string name, int expGained, int money, List<Item> items)
        {
            exp = expGained;
            this.money = money;
            this.itemsToGive = items;
            this.name = name;
        }

        public virtual void checkQuest(Form currentForm, Battle battle, BaseCharacter player)
        {

        }
    }
}
