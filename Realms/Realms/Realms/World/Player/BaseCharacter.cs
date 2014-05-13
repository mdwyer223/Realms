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
        protected int level = 1, exp = 0, nextLevel;
        protected Location destLoc;
        protected Vector2 oldPosition;

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

        public int Level
        {
            get { return level; }
        }

        public int Experience
        {
            get { return exp; }
        }

        public Vector2 ChangeInPos
        {
            get { return Position - oldPosition; }
        }

        public BaseCharacter(Texture2D texture, float secondsToCrossScreen, Location startLoc, int level)
            : base(texture, secondsToCrossScreen, startLoc)
        {
            color = Color.Purple;
            this.level = level;
            equips = new Equips();

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

            //then add equips and skill trees
            stats.increaseAccuracy(equips.wep.WepStats.Accuracy);
            stats.increaseDefense(equips.wep.WepStats.Defense);
            stats.increaseCritChance(equips.wep.WepStats.CritChance);
            stats.increaseDodge(equips.wep.WepStats.Dodge);
            stats.increaseMagicStrength(equips.wep.WepStats.MagicStrength);
            stats.increaseSpeed(equips.wep.WepStats.Speed);
            stats.increaseStrength(equips.wep.WepStats.Strength);

            stats.increaseAccuracy(equips.armor.Stats.Accuracy);
            stats.increaseDefense(equips.armor.Stats.Defense);
            stats.increaseCritChance(equips.armor.Stats.CritChance);
            stats.increaseDodge(equips.armor.Stats.Dodge);
            stats.increaseMagicStrength(equips.armor.Stats.MagicStrength);
            stats.increaseSpeed(equips.armor.Stats.Speed);
            stats.increaseStrength(equips.armor.Stats.Strength);

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
    }
}
