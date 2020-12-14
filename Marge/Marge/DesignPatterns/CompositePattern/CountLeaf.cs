using Marge.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.CompositePattern
{
    class CountLeaf : GameResults
    {
        private int count;
        // Constructor
        public CountLeaf(ComponentType name): base(name)
        {
        }
        public override void Add(GameResults c)
        {
            MessageBox.Show("Cannot add to a leaf");
        }

        public override string Display(int depth)
        {
            return name + " " + count;
        }

        public override void Remove(GameResults c)
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
