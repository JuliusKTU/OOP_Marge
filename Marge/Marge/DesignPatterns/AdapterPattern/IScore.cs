using Marge.DesignPatterns.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AdapterPattern
{
    public interface IScore
    {
        int ReducePoints(Bonus adaptee);
        int AddPoints(Bonus adaptee);
    }
}
