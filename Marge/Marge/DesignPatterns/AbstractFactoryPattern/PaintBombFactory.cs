using Marge.DesignPatterns.ProxyPattern;
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
        public override Buff CreateBuff(ConnectionProxy chatService)
        {
            return new ColorSplash(chatService);
        }

        public override Debuff CreateDebuff(ConnectionProxy chatService)
        {
            return new BlackSplash(chatService);
        }
    }
}
