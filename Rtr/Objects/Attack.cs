using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa.Objects
{
    public class Attack
    {
        protected int _cost;
        protected Actor _actor;
        protected Random _rand;
        public Attack(Actor actor)
        {
            _actor = actor;
            _rand = new Random();
            _cost = 3;
        }

        protected virtual int CalculateDamage()
        {
            return _actor.Strength * _rand.Next(10) + _actor.Dexterity * _rand.Next(7);
        }

        public int Damage()
        {
            return CalculateDamage();
        }

        public virtual int Cost()
        {
            return _cost;
        }
    }
}
