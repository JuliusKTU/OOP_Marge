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
    public class ThiefDamage : DamageDealer
    {
        public override void ProcessRequest(DamageDealerType damageDealer, Player mainPlayer)
        {
            if (damageDealer == DamageDealerType.ThiefDamage)
            {
                mainPlayer.PlayerCalculateScore(-15);
                MessageBox.Show("Damage from Thief");
            }
            else
            {
                damage.ProcessRequest(damageDealer, mainPlayer);
            }
        }
    }
}
