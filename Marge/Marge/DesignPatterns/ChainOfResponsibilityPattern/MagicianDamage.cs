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
    class MagicianDamage : DamageDealer
    {
        public override void ProcessRequest(DamageDealerType damageDealer, Player mainPlayer)
        {
            if (damageDealer == DamageDealerType.MagitianDamage)
            {
                mainPlayer.PlayerCalculateScore(-10);
                MessageBox.Show("Damage from magician");
            }
            else
            {
                damage.ProcessRequest(damageDealer, mainPlayer);
            }
        }
    }
}
