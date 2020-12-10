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
    public class EnemyDamage : DamageDealer
    {
        public override void ProcessRequest(DamageDealerType damageDealer, Player mainPlayer)
        {
            if(damageDealer == DamageDealerType.EnemyDamage)
            {
                mainPlayer.PlayerCalculateScore(-20);
                MessageBox.Show("Damage from enemy");
            }
            else
            {
                MessageBox.Show("Unknown damage");
            }
        }
    }
}
