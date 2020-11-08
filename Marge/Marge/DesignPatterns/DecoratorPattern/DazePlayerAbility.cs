using Marge.DesignPatterns.StrategyPattern;
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
    public class DazePlayerAbility : Decorator
    {
        public async override void Operation(int x, int y,SignalRChatService _chatService)
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                color = "255 0 0",
                messageType = MessageType.dazePlayerEnemy,
                x = x,
                y = y
            });
            enemy.Operation(x, y, _chatService);
        }
    }
}
