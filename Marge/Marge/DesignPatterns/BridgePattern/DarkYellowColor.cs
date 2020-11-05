using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.BridgePattern
{
    public class DarkYellowColor : ColorSet
    {
        public override string ReceiveColorCode()
        {
            //dark yellow
            return "255 255 102";
        }
    }
}
