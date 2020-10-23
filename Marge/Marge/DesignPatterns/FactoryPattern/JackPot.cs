using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Marge.DesignPatterns.Factory
{
    class JackPot : Bonus
    {
        public JackPot(SignalRChatService chatService) : base(chatService)
        {
        }

        public override async void SendBonus()
        {

            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buff,
                message = "buff",
                color = "229 255 204",
                x = 8,
                y = 8
            }) ;


            if (Board.GetTile(8, 8).IsColored)
            {
                Board.AddTile(8, 8, new Tile(true, true, TileType.BonusJackPot));
            }
            else
            {
                Board.AddTile(8, 8, new Tile(false, true, TileType.BonusJackPot));

            }
        }

    }
}
