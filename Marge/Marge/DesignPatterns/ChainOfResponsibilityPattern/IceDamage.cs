using Marge.Enumerators;
using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.ChainOfResponsibilityPattern
{
    class IceDamage : DamageDealer
    {
        public override void ProcessRequest(DamageDealerType damageDealer, Player mainPlayer)
        {
            if (damageDealer == DamageDealerType.IceDamage)
            {
                mainPlayer.PlayerCalculateScore(-5);
                MessageBox.Show("Damage from ice");
            }
            else
            {
                damage.ProcessRequest(damageDealer, mainPlayer);
            }
        }
    }
}
