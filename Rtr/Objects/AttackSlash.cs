using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa.Objects
{
    public class AttackSlash : Attack
    {
        private int _extraCost;
        public AttackSlash(Actor actor)
            : base(actor)
        {
            _extraCost = 2;
        }
        protected override int CalculateDamage()
        {
            return base.CalculateDamage() + _actor.Strength * _rand.Next(7);
        }
        public override int Cost()
        {
            return base.Cost() + _extraCost;
        }
    }
}
