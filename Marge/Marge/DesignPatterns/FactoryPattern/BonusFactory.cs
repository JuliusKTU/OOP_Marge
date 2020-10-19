using Marge.DesignPatterns.Factory;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.FactoryPattern
{
    public class BonusFactory : Factory
    {
        public override Bonus CreateBonus(int bonusId, SignalRChatService chatService)
        {
            switch (bonusId)
            {
                case 1:
                    return new JackPot(chatService);
                case 2:
                    return new Joke(chatService);

                default:
                    return new Normal(chatService);
            }
        }
    }
}
