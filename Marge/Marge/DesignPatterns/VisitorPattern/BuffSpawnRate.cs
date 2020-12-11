using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.VisitorPattern
{
    public class BuffSpawnRate : Visitor
    {
        public override void EasyGameMode(Easy easyGameMode)
        {
            SpawnRates.ColorSplashCount = 20;
            SpawnRates.FreezeOthersStepCount = 19;
;
        }

        public override void MediumGameMode(Medium mediumGameMode)
        {
            SpawnRates.ColorSplashCount = 30;
            SpawnRates.FreezeOthersStepCount = 29;
        }
        public override void HardGameMode(Hard hardGameMode)
        {
            SpawnRates.ColorSplashCount = 40;
            SpawnRates.FreezeOthersStepCount = 39;
        }
    }
}
