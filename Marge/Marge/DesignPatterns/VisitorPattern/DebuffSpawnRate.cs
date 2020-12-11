using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.VisitorPattern
{
    public class DebuffSpawnRate : Visitor
    {
        public override void EasyGameMode(Easy easyGameMode)
        {
            SpawnRates.BlackSplashCount = 40;
            SpawnRates.FreezeYourselfStepCount = 39;
            ;
        }

        public override void MediumGameMode(Medium mediumGameMode)
        {
            SpawnRates.BlackSplashCount = 30;
            SpawnRates.FreezeYourselfStepCount = 29;
        }
        public override void HardGameMode(Hard hardGameMode)
        {
            SpawnRates.BlackSplashCount = 20;
            SpawnRates.FreezeYourselfStepCount = 19;
        }
    }
}
