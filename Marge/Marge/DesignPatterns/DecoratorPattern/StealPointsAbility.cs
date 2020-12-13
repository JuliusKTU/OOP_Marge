using Marge.DesignPatterns.ProxyPattern;
using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.DecoratorPattern
{
    public class StealPointsAbility : Decorator
    {
        public override async void Operation(int x, int y, ConnectionProxy _chatService)
        {
            _chatService.SendMessage("Steal points ability", 1, "255 0 0", MessageType.stealPointEnemy, x, y);
            
            enemy.Operation(x, y, _chatService);
        }
    }
}
