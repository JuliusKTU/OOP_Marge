using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.Factory
{
    class Normal : Bonus
    {
        public Normal(SignalRChatService chatService) : base(chatService)
        {
        }

        public override async void SendBonus()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buff,
                message = "buff",
                x = 8,
                y = 8
            });

            
            if (Board.GetTile(8, 8).IsColored)
            {
                Board.AddTile(8, 8, new Tile(true, true, TileType.BonusNormal));
            }
            else
            {
                Board.AddTile(8, 8, new Tile(false, true, TileType.BonusNormal));

            }
        }

    }
}
