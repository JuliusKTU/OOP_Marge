﻿using Marge.DesignPatterns.StrategyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.GameObjects
{
    class Player
    {

        //atributai
        Dictionary<string, IMovingStatusStrategy> strategyContext = new Dictionary<string, IMovingStatusStrategy>();

        public Player()
        {
            strategyContext.Add(nameof(Confusion), new Confusion());
            strategyContext.Add(nameof(Freeze), new Freeze());
            strategyContext.Add(nameof(Reverse), new Reverse());
            strategyContext.Add(nameof(Move), new Move());
        }

        public void GetStrategy(IMovingStatusStrategy strategy)
        {
          
        }
        public IMovingStatusStrategy RequestStrategy(string strategy)
        {
            return strategyContext[strategy];
        }


    }
}
