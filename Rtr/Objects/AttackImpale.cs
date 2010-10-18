using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa.Objects
{
    public class AttackImpale : Attack
    {
        private int _extraCost;
        public AttackImpale(Actor actor)
            : base(actor)
        {
            _extraCost = 20;
        }

        protected override int CalculateDamage()
        {
            return base.CalculateDamage() + _actor.Dexterity * _rand.Next(15);
        }
        public override int Cost()
        {
            return base.Cost() + _extraCost;
        }
    }
}
