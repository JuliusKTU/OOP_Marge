using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    public class BlackSplash : Debuff
    {
        public BlackSplash()
        {

        }

        public override void SendFreeze()
        {
            throw new NotImplementedException();
        }
    }
}
