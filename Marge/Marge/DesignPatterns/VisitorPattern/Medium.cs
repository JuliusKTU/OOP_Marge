using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.VisitorPattern
{
    public class Medium : GameMode
    {
        public override void ChangeMode(Visitor visitor)
        {
            visitor.MediumGameMode(this);
        }
    }
}
