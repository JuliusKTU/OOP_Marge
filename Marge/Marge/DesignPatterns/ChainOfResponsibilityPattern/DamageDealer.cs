using Marge.Enumerators;
using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.ChainOfResponsibilityPattern
{
    public abstract class DamageDealer
    {
        protected DamageDealer damage;

        public void SetSuccessor (DamageDealer successor)
        {
            this.damage = successor;
        }

        public abstract void ProcessRequest(DamageDealerType damageDealer, Player mainPlayer);
    }
}
