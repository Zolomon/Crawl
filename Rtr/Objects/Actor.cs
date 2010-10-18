using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa.Objects
{
    public abstract class Actor
    {
        public int Attack(Attack attack)
        {
            return attack.Damage();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Mana { get; set; }
        public int Stamina { get; set; }
        public int Speed { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}
