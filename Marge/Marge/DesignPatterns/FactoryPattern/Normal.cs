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

        public override int ReturnAmount()
        {
            return 10;
        }

        public override async void SendBonus()
        {
            Random randNum = new Random();
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.bonusNormal,
                message = "buff",
                color = ColorOptions[YellowColorShades.Dark].ReceiveColorCode(),
                x = Randx,
                y = Randy
            });

            
            if (TilesSet.GetTile(Randx, 15).IsColored)
            {
                TilesSet.AddTile(Randx, Randy, new Tile(true, true, TileType.BonusNormal, Randx, Randy));
            }
            else
            {
                TilesSet.AddTile(Randx, Randy, new Tile(false, true, TileType.BonusNormal, Randx, Randy));

            }
        }

    }
}
