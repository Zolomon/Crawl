using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa.Objects
{
    public class AttackStab : Attack
    {
        private int _extraCost;
        public AttackStab(Actor actor)
            : base(actor)
        {
            _extraCost = 7;
        }
        protected override int CalculateDamage()
        {
            return base.CalculateDamage() + _actor.Dexterity * _rand.Next(10);
        }
        public override int Cost()
        {
            return base.Cost() + _extraCost;
        }
    }
}
