using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.DecoratorPattern
{
    public class StealPointsAbility : Decorator
    {
        public override EnemyEffectOnPlayer Operation(Enemy enemy)
        {
            return EnemyEffectOnPlayer.StealPoints;
        }
    }
}
