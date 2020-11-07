using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    public class ColorSplash : Buff
    {
        private SignalRChatService _chatService;
        public ColorSplash(SignalRChatService chatService)
        {

            _chatService = chatService;
        }

        public override async void SendFreeze()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buffSplashBomb,
                message = "SplashBomb",
                color = "255 51 51",
                x = 5,
                y = 5
            });

            if (TilesSet.GetTile(5, 5).IsColored)
            {
                TilesSet.AddTile(5, 5, new Tile(true, true, TileType.BuffColorSplash));
            }
            else
            {
                TilesSet.AddTile(5, 5, new Tile(false, true, TileType.BuffColorSplash));
            }
        }
    }
}
