using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.DecoratorPattern
{
    public abstract class AbstractEnemy
    {
        public abstract EnemyEffectOnPlayer Operation(Enemy enemy);
    }
}
