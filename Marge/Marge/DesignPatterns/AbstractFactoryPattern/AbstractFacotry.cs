using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    public abstract class AbstractFactory
    {
       
        //siuncia tipa buffo
        public abstract Buff CreateBuff(SignalRChatService chatService); //pakeisti tipa

        public abstract Debuff CreateDebuff(SignalRChatService chatService);

    }
}
