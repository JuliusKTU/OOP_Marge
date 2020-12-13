using Marge.DesignPatterns.ProxyPattern;
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
        public Joke(ConnectionProxy chatService) : base(chatService)
        {
        }

        public override void SendBonus()
        {
            Random randNum = new Random();
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);
            _chatService.SendMessage("buff", 1, ColorOptions[YellowColorShades.Light].ReceiveColorCode(), MessageType.bonusJoke, Randx, Randy);
            
            if (TilesSet.GetTile(Randx, Randy).IsColored)
            {
                TilesSet.AddTile(Randx, Randy, new Tile(true, true, TileType.BonusJoke, Randx, Randy));
            }
            else
            {
                TilesSet.AddTile(Randx, Randy, new Tile(false, true, TileType.BonusJoke, Randx, Randy));

            }
            
        }
        public override int ReturnAmount()
        {
            return 5;
        }
    }
}
