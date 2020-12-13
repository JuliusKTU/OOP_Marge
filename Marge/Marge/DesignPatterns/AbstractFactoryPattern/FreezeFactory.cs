using Marge.DesignPatterns.ProxyPattern;
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
        public override Buff CreateBuff( ConnectionProxy chatService)
        {
       
            return new FreezeOthers(chatService);
        }

        public override Debuff CreateDebuff(ConnectionProxy chatService)
        {
          
            return new FreezeYourself(chatService);
        }

    }
}
