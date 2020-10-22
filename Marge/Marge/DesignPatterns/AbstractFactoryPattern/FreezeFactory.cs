using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    class FreezeFactory : AbstractFactory
    {
        public override Buff CreateBuff( SignalRChatService chatService)
        {
       
            return new FreezeOthers(chatService);
        }

        public override Debuff CreateDebuff(SignalRChatService chatService)
        {
          
            return new FreezeYourself(chatService);
        }

    }
}
