using Marge.DesignPatterns.Factory;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.FactoryPattern
{
     public abstract class Factory
    {
        public abstract Bonus CreateBonus(int bonusId, SignalRChatService chatService);
    }
}
