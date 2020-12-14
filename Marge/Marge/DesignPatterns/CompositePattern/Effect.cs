using Marge.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.CompositePattern
{
    public class Effect : GameResults
    {
        private List<GameResults> _children = new List<GameResults>();

        public Effect(ComponentType name) : base(name)
        {
        }

        public override void Add(GameResults c)
        {
            _children.Add(c);
        }

        public override string Display(int depth)
        {
            string result = name +" \n";

            // Recursively display child nodes
            
            foreach (GameResults component in _children)
            {
                result += component.Display(depth + 2)+" \n";
            }

            return result;
        }

        public override void Remove(GameResults c)
        {
            _children.Remove(c);
        }

        public override void AddPoint(ComponentType leafName)
        {
            foreach (GameResults component in _children)
            {
                component.AddPoint(leafName);
            }
        }
    }
}
