using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.BridgePattern
{
    public class LightYellowColor : ColorSet
    {
        public override string ReceiveColorCode()
        {
            //light yellow
            return "255 255 204";
        }
    }
}
