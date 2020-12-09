using Marge.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.CompositePattern
{
    class Leaf : Component1
    {
        private int count;
        // Constructor
        public Leaf(ComponentType name): base(name)
        {
        }
        public override void Add(Component1 c)
        {
            MessageBox.Show("Cannot add to a leaf");
        }

        public override string Display(int depth)
        {
            return name + " " + count;
        }

        public override void Remove(Component1 c)
        {
            MessageBox.Show("Cannot remove from a leaf");
        }

        public override void AddPoint(ComponentType leafName)
        {
            if (name.Equals(leafName))
            {
                count++;
            }
        }
    }
}
