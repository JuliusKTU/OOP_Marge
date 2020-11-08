using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.DecoratorPattern
{
    public abstract class Decorator : AbstractEnemy
    {
        protected AbstractEnemy enemy;

        public void SetEnemy(AbstractEnemy enemy)
        {
            this.enemy = enemy;
        }

    }
}
