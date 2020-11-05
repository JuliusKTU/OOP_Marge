using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.BridgePattern
{
    public class YellowColor : ColorSet
    {
        public override string ReceiveColorCode()
        {
            //yellow
            return "255 255 0";
        }
    }
}
