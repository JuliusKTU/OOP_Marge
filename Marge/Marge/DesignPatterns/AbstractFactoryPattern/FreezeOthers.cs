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
    class FreezeOthers : Buff
    {
        private SignalRChatService _chatService;

        public FreezeOthers(SignalRChatService chatService)
        {
            _chatService = chatService;
        }

        public override async void SendBuff()
        {

            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buffFreezeOthers,
                message = "buff",
                color = "100 100 200",
                x = 11,
                y = 11
            });

            if (TilesSet.GetTile(11,11).IsColored)
            {
                TilesSet.AddTile(11, 11, new Tile(true, true, TileType.BuffFreezeOthers));
            } 
            else
            {
                TilesSet.AddTile(11, 11, new Tile(false, true, TileType.BuffFreezeOthers));

            }
            

        }
    }
}
