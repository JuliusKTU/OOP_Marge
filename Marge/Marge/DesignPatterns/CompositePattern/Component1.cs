using Marge.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.CompositePattern
{
    public abstract class Component1
    {
        public ComponentType name { get; private set; }

        public Component1(ComponentType name)
        {
            this.name = name;
        }

        public abstract void Add(Component1 c);
        public abstract void Remove(Component1 c);
        public abstract string Display(int depth);
        public abstract void AddPoint(ComponentType leafName);
    }
}
