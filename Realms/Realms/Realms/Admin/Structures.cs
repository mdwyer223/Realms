using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realms
{
    public struct MaxPercent
    {
        public float strength;
        public float defense;
        public float speed;
        public float accuracy;
        public float dodge;
        public float critChance;
        public float magicDamage;
        public float health;
        public float mana;
    }

    public struct InitialValues
    {
        public float strength;
        public float defense;
        public float speed;
        public float accuracy;
        public float dodge;
        public float critChance;
        public float magicDamage;
        public float health;
        public float mana;
    }

    public struct QuadraticValues
    {
        public float accel;
        public float velo;
        public float initial;
    }

    public struct Equips
    {
        public Weapon wep;
        public Armor armor;
        public Accessory accessory;
    }
}
