using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.VisitorPattern
{
    public abstract class Visitor
    {
        public abstract void EasyGameMode(Easy easyGameMode);
        public abstract void MediumGameMode(Medium mediumGameMode);
        public abstract void HardGameMode(Hard hardGameMode);
    }
}
