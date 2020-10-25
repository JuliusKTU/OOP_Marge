﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.StrategyPattern
{
    class Reverse : IMovingStatusStrategy
    {
        public StrategyType MovementChange()
        {
            return StrategyType.Reversed;
        }
    }
}
