using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.VisitorPattern
{
    public class BonusSpawnRate : Visitor
    {
        public override void EasyGameMode(Easy easyGameMode)
        {
            SpawnRates.BonusCount = 1;
        }
        public override void MediumGameMode(Medium mediumGameMode)
        {
            SpawnRates.BonusCount = 10;
        }

        public override void HardGameMode(Hard hardGameMode)
        {
            SpawnRates.BonusCount = 60;
        }

        
    }
}
