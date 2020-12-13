using Marge.DesignPatterns.ProxyPattern;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.DecoratorPattern
{
    public abstract class AbstractEnemy
    {
        public abstract void Operation(int x, int y, ConnectionProxy _chatService);
    }
}
