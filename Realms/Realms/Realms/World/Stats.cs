using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realms
{
    public class Stats
    {
        const float MAX_STRENGTH = 125f, MAX_DEFENSE = 125f, MAX_SPEED = 75f, MAX_ACCURACY = 75f, MAX_DODGE = 75f, MAX_CRIT_CHANCE = 100f, MAX_MAGIC_DAMAGE = 125f;
        const int MAX_HEALTH = 15000, MAX_MANA = 1500, MAX_LEVEL = 100;

        float strength = 0f, defense = 0f, speed = 0f, accuracy = 0f, dodge = 0f, critChance = 0f, magicDamage = 0f; //magicdamage = magicStrength
        int healthPoints = 0, manaPoints = 0, level = 1;

        public Stats(BaseCharacter character)
        {
            if (character != null)
            {
                this.level = character.Level;
                if (character.GetType() == typeof(Assassin))
                {
                    calcStats(assassinStruct(), defaultAssassin());
                }
                else if (character.GetType() == typeof(Assault))
                {
                    calcStats(assaultStruct(), defaultAssault());
                }
                else if (character.GetType() == typeof(Scientist))
                {
                    calcStats(scientistStruct(), defaultScientist());
                }
                else if (character.GetType() == typeof(Engineer))
                {
                    calcStats(engineerStruct(), defaultEngineer());
                }
            }
        }

        public Stats(EnemyType type, int tier)
        {
            if (tier == 0)
            {
                level = 1;
            }
            else if (tier == 1)
            {
                level = 10;
            }
            else if (tier == 2)
            {
                level = 20;
            }
            else if (tier == 3)
            {
                level = 30;
            }
            else if (tier == 4)
            {
                level = 40;
            }
            else if (tier == 5)
            {
                level = 50;
            }
            else if (tier == 6)
            {
                level = 60;
            }
            else if (tier == 7)
            {
                level = 70;
            }
            else if (tier == 8)
            {
                level = 80;
            }
            else if (tier == 9)
            {
                level = 90;
            }
            else
            {
                level = 100;
            }

            if (type == EnemyType.CASTER)
            {
                calcStats(scientistStruct(), defaultScientist());
            }
            else if (type == EnemyType.LIGHT)
            {
                calcStats(assassinStruct(), defaultAssassin());
            }
            else if (type == EnemyType.HEAVY)
            {
                calcStats(assaultStruct(), defaultAssault());
            }
            else
            {
                calcStats(assaultStruct(), defaultAssault());
            }
        }

        #region Max properties

        public int MaxHealth
        {
            get { return MAX_HEALTH; }
        }

        public int MaxMana
        {
            get { return MAX_MANA; }
        }

        public int MaxLevel
        {
            get { return MAX_LEVEL; }
        }

        public float MaxStrength
        {
            get { return MAX_STRENGTH; }
        }

        public float MaxDefense
        {
            get { return MAX_DEFENSE; }
        }

        public float MaxSpeed
        {
            get { return MAX_SPEED; }
        }

        public float MaxAccuracy
        {
            get { return MAX_ACCURACY; }
        }

        public float MaxDodge
        {
            get { return MAX_DODGE; }
        }

        public float MaxCritChance
        {
            get { return MAX_CRIT_CHANCE; }
        }

        public float MaxMagicDamage
        {
            get { return MAX_MAGIC_DAMAGE; }
        }

        #endregion

        #region Stats
        public int Health
        {
            get { return healthPoints; }
        }

        public int Mana
        {
            get { return manaPoints; }
        }

        public int Level
        {
            get { return level; }
        }

        public float Strength
        {
            get { return strength; }
        }

        public float Defense
        {
            get { return defense; }
        }

        public float Speed
        {
            get { return speed; }
        }

        public float Accuracy
        {
            get { return accuracy; }
        }

        public float Dodge
        {
            get { return dodge; }
        }

        public float MagicStrength
        { 
            get { return magicDamage; } 
        }

        public float CritChance
        {
            get { return critChance; }
        }
        #endregion

        #region Upgrade methods

        public void upgradeHealth(int numPoints)
        {
            if (numPoints + healthPoints > MAX_HEALTH)
            {
                healthPoints = MAX_HEALTH;
            }
            else
            {
                healthPoints += numPoints;
            }
        }

        public void decreaseHealth(int numPoints)
        {
            healthPoints -= numPoints;
            if (healthPoints <= 0)
                healthPoints = 1;
        }

        public void upgradeMana(int numPoints)
        {
            if (numPoints + manaPoints > MAX_MANA)
            {
                manaPoints = MAX_MANA;
            }
            else
            {
                manaPoints += numPoints;
            }
        }

        public void decreaseMana(int numPoints)
        {
            manaPoints -= numPoints;
            if (manaPoints <= 0)
                manaPoints = 1;
        }

        public void increaseStrength(float numPoints)
        {
            if (numPoints + strength > MAX_STRENGTH)
            {
                strength = MAX_STRENGTH;
            }
            else
            {
                strength += numPoints;
            }
        }

        public void decreaseStrength(float numPoints)
        {
            strength -= numPoints;
            if (strength < 0)
                strength = 0;
        }

        public void increaseDefense(float numPoints)
        {
            if (numPoints + defense > MAX_DEFENSE)
            {
                defense = MAX_DEFENSE;
            }
            else
            {
                defense += numPoints;
            }
        }

        public void decreaseDefense(float numPoints)
        {
            defense -= numPoints;
            if (defense < 0)
                defense = 0;
        }

        public void increaseSpeed(float numPoints)
        {
            if (numPoints + speed > MAX_SPEED)
            {
                speed = MAX_SPEED;
            }
            else
            {
                speed += numPoints;
            }
        }

        public void decreaseSpeed(float numPoints)
        {
            speed -= numPoints;
            if (speed < 0)
                speed = 0;
        }

        public void increaseAccuracy(float numPoints)
        {
            if (accuracy + numPoints > MAX_ACCURACY)
            {
                accuracy = MAX_ACCURACY;
            }
            else
            {
                accuracy += numPoints;
            }
        }

        public void decreaseAccuracy(float numPoints)
        {
            accuracy -= numPoints;
            if (accuracy < 0)
                accuracy = 0;
        }

        public void increaseDodge(float numPoints)
        {
            if (numPoints + dodge > MAX_DODGE)
            {
                dodge = MAX_DODGE;
            }
            else
            {
                dodge += numPoints;
            }
        }

        public void decreaseDodge(float numPoints)
        {
            dodge -= numPoints;
            if (dodge < 0)
                dodge = 0;
        }

        public void increaseMagicStrength(float numPoints)
        {
            if (numPoints + magicDamage > MAX_MAGIC_DAMAGE)
            {
                numPoints = MAX_MAGIC_DAMAGE;
            }
            else
            {
                magicDamage += numPoints;
            }
        }

        public void decreaseMagicStrength(float numPoints)
        {
            magicDamage -= numPoints;
            if (magicDamage < 0)
                magicDamage = 0;
        }

        public void increaseCritChance(float numPoints)
        {
            if (numPoints + critChance > MAX_CRIT_CHANCE)
            {
                critChance = MAX_CRIT_CHANCE;
            }
            else
            {
                critChance += numPoints;
            }
        }

        public void decreaseCritChance(float numPoints)
        {
            critChance -= numPoints;
            if (critChance < 1)
                critChance = 1;
        }
        #endregion

        #region Defaults
        private InitialValues defaultAssassin()
        {
            InitialValues i = new InitialValues();
            i.strength = strength = 2f;
            i.defense = defense = 1.5f;
            i.health = healthPoints = 140;
            i.speed = speed = 5.0f;
            i.accuracy = accuracy = 2.0f;
            i.dodge = dodge = 1.0f;
            i.critChance = critChance = 5.0f;
            i.mana = manaPoints = 25;
            return i;
        }

        private InitialValues defaultAssault()
        {
            InitialValues i = new InitialValues();
            i.strength = strength = 3.0f;
            i.defense = defense = 3.0f;
            i.health = healthPoints = 250;
            i.speed = speed = 0.0f;
            i.accuracy = accuracy = 1.0f;
            i.dodge = dodge = 2.0f;
            i.critChance = critChance = 2.0f;
            i.mana = manaPoints = 50;
            return i;
        }

        private InitialValues defaultEngineer()
        {
            InitialValues i = new InitialValues();
            i.strength = strength = 1.5f;
            i.defense = defense = 1.5f;
            i.health = healthPoints = 180;
            i.speed = speed = 2.5f;
            i.accuracy = accuracy = 2.0f;
            i.dodge = dodge = 1.0f;
            i.critChance = critChance = 1.0f;
            i.mana = manaPoints = 50;
            return i;
        }

        private InitialValues defaultScientist()
        {
            InitialValues i = new InitialValues();
            i.strength = strength = 1.0f;
            i.defense = defense = 2.0f;
            i.health = healthPoints = 140;
            i.speed = speed = 1.0f;
            i.accuracy = accuracy = 3.0f;
            i.dodge = dodge = 2.0f;
            i.critChance = critChance = 1.0f;
            i.mana = manaPoints = 75;
            return i;
        }
        #endregion

        private MaxPercent assassinStruct()
        {
            MaxPercent structure = new MaxPercent();

            structure.strength = .75f;
            structure.defense = .5f;
            structure.health = .75f;
            structure.speed = .9f;
            structure.accuracy = .55f;
            structure.dodge = .65f;
            structure.critChance = .75f;
            structure.mana = .33f;
            structure.magicDamage = .1f;

            return structure;
        }

        private MaxPercent assaultStruct()
        {
            MaxPercent structure = new MaxPercent();

            structure.strength = .55f;
            structure.defense = .85f;
            structure.health = .95f;
            structure.speed = .35f;
            structure.accuracy = .35f;
            structure.dodge = .7f;
            structure.critChance = .35f;
            structure.mana = .50f;
            structure.magicDamage = .4f;

            return structure;
        }

        private MaxPercent engineerStruct()
        {
            MaxPercent structure = new MaxPercent();

            structure.strength = .55f;
            structure.defense = .65f;
            structure.health = .80f;
            structure.speed = .60f;
            structure.accuracy = .40f;
            structure.dodge = .50f;
            structure.critChance = .25f;
            structure.mana = .75f;
            structure.magicDamage = .55f;

            return structure;
        }

        private MaxPercent scientistStruct()
        {
            MaxPercent structure = new MaxPercent();

            structure.strength = .65f;
            structure.defense = .55f;
            structure.health = .70f;
            structure.speed = .60f;
            structure.accuracy = .40f;
            structure.dodge = .50f;
            structure.critChance = .15f;
            structure.mana = .95f;
            structure.magicDamage = .75f;

            return structure;
        }

        private void calcStats(MaxPercent p, InitialValues i)
        {
            //strength
            QuadraticValues equation = new QuadraticValues();
            equation.velo = .2f;
            equation.initial = i.strength;
            equation.accel = ((p.strength * MaxStrength) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            strength = (equation.accel * (float)(Math.Pow(Level,2))) + (equation.velo * Level) + (equation.initial);

            //defense
            equation = new QuadraticValues();
            equation.velo = .3f;
            equation.initial = i.defense;
            equation.accel = ((p.defense * MaxDefense) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            defense = (equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial);

            //health
            equation = new QuadraticValues();
            equation.velo = 50;
            equation.initial = i.health;
            equation.accel = ((p.health * MaxHealth) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            healthPoints = (int)((equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial));

            //mana
            equation = new QuadraticValues();
            equation.velo = 25;
            equation.initial = i.mana;
            equation.accel = ((p.mana * MaxMana) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            manaPoints = (int)((equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial));

            //speed
            equation = new QuadraticValues();
            equation.velo = .05f;
            equation.initial = i.speed;
            equation.accel = ((p.speed * MaxSpeed) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            speed = (equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial);

            //accuracy
            equation = new QuadraticValues();
            equation.velo = .1f;
            equation.initial = i.accuracy;
            equation.accel = ((p.accuracy * MaxAccuracy) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            accuracy = (equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial);

            //dodge
            equation = new QuadraticValues();
            equation.velo = .03f;
            equation.initial = i.dodge;
            equation.accel = ((p.dodge * MaxDodge) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            dodge = (equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial);

            //critChance
            equation = new QuadraticValues();
            equation.velo = .1f;
            equation.initial = i.critChance;
            equation.accel = ((p.critChance * MaxMagicDamage) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            critChance = (equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial);

            //magicDamage
            equation = new QuadraticValues();
            equation.velo = .3f;
            equation.initial = i.magicDamage;
            equation.accel = ((p.magicDamage * MaxStrength) - equation.initial - (equation.velo * MaxLevel)) / (float)(Math.Pow((MaxLevel), 2));
            magicDamage = (equation.accel * (float)(Math.Pow(Level, 2))) + (equation.velo * Level) + (equation.initial);
        }

        public override string ToString()
        {
            string s = "HP: " + healthPoints + "  MP: " + manaPoints + "\nStr: " + (int)strength + "  Def: " + (int)defense + 
                "\nSpd: " + (int)speed + "  Dodge: " + (int)dodge + "\nAcc: " + (int)accuracy + "  Crit: " + (int)critChance + 
                "\nMagic: " + (int)magicDamage;

            return s;
        }
    }
}
