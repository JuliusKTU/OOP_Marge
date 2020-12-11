using Marge.DesignPatterns.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.MementoPattern
{
    public class MementoCareTaker
    {
        private PlayerMemento _memento;
        public PlayerMemento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}
