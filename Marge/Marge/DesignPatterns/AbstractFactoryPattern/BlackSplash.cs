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
    public class BlackSplash : Debuff
    {

        private SignalRChatService _chatService;
        public BlackSplash(SignalRChatService chatService)
        {
            _chatService = chatService;
        }

        public override async void SendDebuff()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.debuffBlackSplash,
                message = "BlackSplash",
                color = "0 0 0",
                x = 4,
                y = 4
            });

            if (TilesSet.GetTile(4, 4).IsColored)
            {
                TilesSet.AddTile(4, 4, new Tile(true, true, TileType.DebuffBlackSplash));
            }
            else
            {
                TilesSet.AddTile(4, 4, new Tile(false, true, TileType.DebuffBlackSplash));
            }
        }
    }
}
