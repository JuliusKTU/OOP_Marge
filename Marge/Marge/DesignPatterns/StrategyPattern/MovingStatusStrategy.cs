using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.StrategyPattern
{
    public interface IMovingStatusStrategy
    {
        StrategyType MovementChange();
        
    }

    public enum StrategyType
    {
        Move,
        Frozen,
        Reversed,
        Confused
    }
}
