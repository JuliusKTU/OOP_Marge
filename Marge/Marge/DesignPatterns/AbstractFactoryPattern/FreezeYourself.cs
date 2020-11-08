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
    class FreezeYourself : Debuff
    {
        private SignalRChatService _chatService;
        public FreezeYourself(SignalRChatService chatService)
        {
            _chatService = chatService;
        }
      

        public override async void SendDebuff()
        {

            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.debuffFreezeYourself,
                message = "playerFreeze",
                color = "100 149 237",
                x = 10,
                y = 10
            });

            if (TilesSet.GetTile(10, 10).IsColored)
            {
                TilesSet.AddTile(10, 10, new Tile(true, true, TileType.DebuffFreezeYourself));
            }
            else
            {
                TilesSet.AddTile(10, 10, new Tile(false, true, TileType.DebuffFreezeYourself));
            }

        }
    }
}
