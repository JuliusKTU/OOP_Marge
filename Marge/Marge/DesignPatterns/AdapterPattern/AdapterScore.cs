using Marge.DesignPatterns.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AdapterPattern
{

    public class AdapterScore : IScore
    {
        Bonus Adaptee { get; set; }
        public int AddPoints(Bonus adaptee)
        {
            Adaptee = adaptee;
            return adaptee.ReturnAmount();
        }

        public int ReducePoints(Bonus adaptee)
        {
            Adaptee = adaptee;
            return adaptee.ReturnAmount() * -1;
        }
    }
}
