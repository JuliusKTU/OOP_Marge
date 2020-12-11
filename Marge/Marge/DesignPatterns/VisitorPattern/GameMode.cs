using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.VisitorPattern
{
    public abstract class GameMode
    {
        public abstract void ChangeMode(Visitor visitor);
    }
}
