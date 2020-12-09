using Marge.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.CompositePattern
{
    public class Composite : Component1
    {
        private List<Component1> _children = new List<Component1>();

        public Composite(ComponentType name) : base(name)
        {
        }

        public override void Add(Component1 c)
        {
            _children.Add(c);
        }

        public override string Display(int depth)
        {
            string result = name +" \n";

            // Recursively display child nodes
            
            foreach (Component1 component in _children)
            {
                result += component.Display(depth + 2)+" \n";
            }

            return result;
        }

        public override void Remove(Component1 c)
        {
            _children.Remove(c);
        }

        public override void AddPoint(ComponentType leafName)
        {
            foreach (Component1 component in _children)
            {
                component.AddPoint(leafName);
            }
        }
    }
}
