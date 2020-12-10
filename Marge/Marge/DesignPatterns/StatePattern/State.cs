using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.StatePattern
{
    public abstract class State
    {
        public abstract void Change(Board board);
    }
}
