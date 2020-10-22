using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    public class ColorSplash : Buff
    {
        public ColorSplash()
        {

        }

        public override void SendFreeze()
        {
            throw new NotImplementedException();
        }
    }
}
