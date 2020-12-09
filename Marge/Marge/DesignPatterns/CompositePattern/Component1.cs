using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.CompositePattern
{
    public abstract class Component1
    {
        protected string name;

        public Component1(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component1 c);
        public abstract void Remove(Component1 c);
        public abstract void Display(int depth);
    }
}
