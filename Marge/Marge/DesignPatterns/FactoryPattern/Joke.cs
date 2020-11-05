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
    class Joke : Bonus
    {
        public Joke(SignalRChatService chatService) : base(chatService)
        {
        }

        public override async void SendBonus()
        {
            Random randNum = new Random();
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buff,
                message = "buff",
                color = ColorOptions[YellowColorShades.Light].ReceiveColorCode(),
                x = Randx,
                y = Randy
            });


            if (TilesSet.GetTile(Randx, Randy).IsColored)
            {
                TilesSet.AddTile(Randx, Randy, new Tile(true, true, TileType.BonusJoke));
            }
            else
            {
                TilesSet.AddTile(Randx, Randy, new Tile(false, true, TileType.BonusJoke));

            }
            
        }
        public override int ReturnAmount()
        {
            return 5;
        }
    }
}
