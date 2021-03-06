﻿using Marge.DesignPatterns.ProxyPattern;
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
        public abstract Buff CreateBuff(ConnectionProxy chatService);

        public abstract Debuff CreateDebuff(ConnectionProxy chatService);

    }
}
