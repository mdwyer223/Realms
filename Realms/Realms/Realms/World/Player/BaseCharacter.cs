using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Realms
{
    //This class allows code reuse between different ...classes
    public abstract class BaseCharacter : AdvancedSprite
    {
        protected Stats stats;
        protected Equips equips;
        protected Inventory invent;
        protected int ID, level = 1, exp = 0, nextLevel = 0, money = 0,
            currentHealth, currentMP;
        protected Location destLoc;
        protected Vector2 oldPosition;

        protected List<Quest> currentQuests, completedQuests;

        protected const int EXP_AT_MAX = 15000000;
        private const int expVelo = 2000, expAccel = 1480;

        public Stats Stats
        {
            get {
                stats = new Stats(this);
                return stats; 
            }
        }

        public Equips Equipment
        {
            get { return equips; }
        }

        public Inventory Inventory
        {
            get { return invent; }
        }

        public List<Quest> QuestList
        {
            get { return currentQuests; }
        }

        public int Level
        {
            get { return level; }
        }

        public int Experience
        {
            get { return exp; }
        }

        public int CurrentHP
        {
            get { return currentHealth; }
        }

        public int CurrentMP
        {
            get { return currentMP; }
        }

        public Vector2 ChangeInPos
        {
            get { return Position - oldPosition; }
        }

        public BaseCharacter(Texture2D texture, float secondsToCrossScreen, Location startLoc, int level, int ID)
            : base(texture, secondsToCrossScreen, startLoc)
        {
            this.ID = ID;
            currentQuests = new List<Quest>();
            completedQuests = new List<Quest>();
            color = Color.Purple;
            this.level = level;
            equips = new Equips();
            invent = new Inventory();

            invent.addItem(new Potion());
            invent.addItem(new Elixer());
            invent.addItem(new Ether());
            invent.addItem(new LifeEssence());
            invent.addItem(new DamagePlus());

            destLoc = startLoc;
        }

        public override void update(GameTime gameTime, Grid map)
        {
            if (destLoc == null)
                destLoc = loc;
            Game1.Camera.MoveSpeed = Speed;
            Game1.Camera.Focus = Position;
            Position += velocity;
            velocity = new Vector2(destLoc.Column * 32, destLoc.Row * 32) - this.Position;
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            else
            {
                loc = destLoc;
            }
            velocity *= Speed;
            if (loc == destLoc)
            {
                if (Input.rightDown())
                {
                    Location test = new Location(loc.Row, loc.Column);
                    test.Column++;
                    if (map.isValid(test))
                    {
                        destLoc = test;
                        //play anime right
                    }
                }
                else if (Input.leftDown())
                {
                    Location test = new Location(loc.Row, loc.Column);
                    test.Column--;
                    if (map.isValid(test))
                    {
                        destLoc = test;
                        //play anime left
                    }
                }
                else if (Input.upDown())
                {
                    Location test = new Location(loc.Row, loc.Column);
                    test.Row--;
                    if (map.isValid(test))
                    {
                        destLoc = test;
                        //play anime up
                    }
                }
                else if (Input.downDown())
                {
                    Location test = new Location(loc.Row, loc.Column);
                    test.Row++;
                    if (map.isValid(test))
                    {
                        destLoc = test;
                        //play anime down
                    }
                }
            }
            oldPosition = Position;
            oldLoc = loc;

            Tile[] objects = map.getObjects();
            foreach (Tile t in objects)
            {
                if (t != null)
                {
                    if (t.GetType() == typeof(BaseEnemy))
                    {
                        if (!t.IsDead)
                        {
                            if (this.measureDis(t.Position) <= (32 * Math.Sqrt(2)))
                            {
                                BaseEnemy b = (BaseEnemy)t;
                                this.engageBattle(b);
                                b.battled();
                            }
                        }
                    }

                    else if (t.GetType() == typeof(TripWire))
                    {
                        if (!t.IsDead)
                        {
                            if (this.isColliding(t.Rec))
                            {
                                TripWire tw = (TripWire)t;
                                tw.trigger();
                                destLoc = null;
                            }
                        }
                    }
                }
            }
        }

        public bool giveQuest(Quest q)
        {
            if (q != null)
            {
                bool finished = false;
                foreach (Quest cQ in completedQuests)
                {
                    if (cQ.GetType() == q.GetType())
                    {
                        finished = true;
                    }
                }
                bool started = false;
                foreach (Quest pQ in currentQuests)
                {
                    if (pQ.GetType() == q.GetType())
                    {
                        started = true;
                    }
                }

                if (!finished && !started)
                {
                    this.currentQuests.Add(q);
                    return true;
                }
            }

            return false;
        }

        public void completedQuest(Quest q)
        {
            if (q != null)
            {
                bool haveQuest = false;
                int questIndex = 0;
                for (int i = 0; i < currentQuests.Count; i++)
                {
                    if (q.GetType() == currentQuests[i].GetType())
                    {
                        haveQuest = true;
                        questIndex = i;
                        break;
                    }
                }

                if (haveQuest)
                {
                    completedQuests.Add(q);
                    awardExp(q.AwardedExp);
                    increaseFunds(q.AwardedMoney);
                    if (q.AwardedItems != null)
                    {
                        foreach (Item i in q.AwardedItems)
                        {
                            if (i != null)
                            {
                                invent.addItem(i);
                            }
                        }
                    }
                    currentQuests.RemoveAt(questIndex);
                }
            }
        }

        public bool increaseFunds(int num)
        {
            //check max
            money += num;
            return true;
        }

        public bool decreaseFunds(int num)
        {
            if (money - num < 0)
                return false;
            else
            {
                money -= num;
                return true;
            }
        }

        public bool awardExp(int expPoints)
        {
            exp += expPoints;
            return checkLevel();
        }

        public bool checkLevel()
        {
            if (exp > nextLevel && level != 100)
            {
                level++;
                if (level < 100)
                {
                    nextLevel = (int)((expAccel * Math.Pow(level + 1, 2)) + (expVelo * level + 1));
                    checkLevel();
                }
                return true;
            }

            return false;
        }

        public void engageBattle(BaseEnemy enemy)
        {
            //check for party members on the server
            List<BattleEnemy> enemies = enemy.spawnEnemies();
            BattleCharacter bc = new BattleCharacter(Image.Particle, .07f, this);

            Game1.activateBattle(new Battle(bc, enemies));
        }

        public Stats calcStats()
        {
            //calc base stats first
            stats = new Stats(this);
            if (currentHealth == 0)
            {
                currentHealth = stats.Health;
                currentMP = stats.Mana;
            }


            //then add equips and skill trees
            if (equips.wep != null)
            {
                stats.increaseAccuracy(equips.wep.WepStats.Accuracy);
                stats.increaseDefense(equips.wep.WepStats.Defense);
                stats.increaseCritChance(equips.wep.WepStats.CritChance);
                stats.increaseDodge(equips.wep.WepStats.Dodge);
                stats.increaseMagicStrength(equips.wep.WepStats.MagicStrength);
                stats.increaseSpeed(equips.wep.WepStats.Speed);
                stats.increaseStrength(equips.wep.WepStats.Strength);
            }

            if (equips.armor != null)
            {
                stats.increaseAccuracy(equips.armor.Stats.Accuracy);
                stats.increaseDefense(equips.armor.Stats.Defense);
                stats.increaseCritChance(equips.armor.Stats.CritChance);
                stats.increaseDodge(equips.armor.Stats.Dodge);
                stats.increaseMagicStrength(equips.armor.Stats.MagicStrength);
                stats.increaseSpeed(equips.armor.Stats.Speed);
                stats.increaseStrength(equips.armor.Stats.Strength);
            }

            return stats;
        }

        public void setLocation(Grid gr, Location newLoc)
        {
            if (gr == null)
            {
                this.Location = Location.Zero;
                return;
            }

            if (gr.isValid(newLoc))
            {
                this.Location = newLoc;
            }
            else
            {
                this.Location = Location.Zero;
            }
        }

        public void equipWeapon(Weapon newWep)
        {
            Weapon temp = equips.wep;
            equips.wep = newWep;
            List<Item> inventList = invent.ItemList;
            for (int i = 0; i < inventList.Count; i++)
            {
                if (inventList[i] == newWep)
                {
                    inventList[i] = temp;
                    break;
                }
            }
        }

        public void equipArmor(Armor newArmor)
        {
            Armor temp = equips.armor;
            equips.armor = newArmor;
            List<Item> inventList = invent.ItemList;
            for (int i = 0; i < inventList.Count; i++)
            {
                if (inventList[i] == newArmor)
                {
                    inventList[i] = temp;
                    break;
                }
            }
        }

        public void equipAccessory(Accessory newAccessory)
        {
            Accessory temp = equips.accessory;
            equips.accessory = newAccessory;
            List<Item> inventList = invent.ItemList;
            for (int i = 0; i < inventList.Count; i++)
            {
                if (inventList[i] == newAccessory)
                {
                    inventList[i] = temp;
                    break;
                }
            }
        }

        public void setHealthMana(int h, int m)
        {
            if (h < 0)
                currentHealth = 0;
            else
                currentHealth = h;

            if (m < 0)
                currentMP = 0;
            else
                currentMP = m;
        }
    }
}
