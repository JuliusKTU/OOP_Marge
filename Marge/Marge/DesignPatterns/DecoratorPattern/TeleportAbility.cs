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
    public class TeleportAbility : Decorator
    {
        public override void Operation(int x, int y,ConnectionProxy _chatService)
        {
            _chatService.SendMessage("Teleportation ability", 1, "255 0 0", MessageType.teleportPlayerEnemy, x, y);
            
            enemy.Operation(x, y, _chatService);
        }
    }
}
