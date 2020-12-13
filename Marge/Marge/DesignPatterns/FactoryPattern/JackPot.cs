using Marge.DesignPatterns.ProxyPattern;
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
        public JackPot(ConnectionProxy chatService) : base(chatService)
        {
        }

        public override void SendBonus()
        {
            Random randNum = new Random();
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);
            _chatService.SendMessage("buff", 1, ColorOptions[YellowColorShades.Normal].ReceiveColorCode(), MessageType.bonusJackPot, Randx, Randy);

        }
        public override int ReturnAmount()
        {
            return 25;
        }
    }
}
