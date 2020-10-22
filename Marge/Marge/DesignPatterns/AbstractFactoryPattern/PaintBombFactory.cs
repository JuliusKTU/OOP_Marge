using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    class PaintBombFactory : AbstractFactory
    {
        public override Buff CreateBuff(SignalRChatService chatService)
        {
            return new ColorSplash();
        }

        public override Debuff CreateDebuff(SignalRChatService chatService)
        {
            return new BlackSplash();
        }
    }
}
