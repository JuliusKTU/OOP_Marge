using Marge.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.CompositePattern
{
    public abstract class GameResults
    {
        public ComponentType name { get; private set; }

        public GameResults(ComponentType name)
        {
            this.name = name;
        }

        public abstract void Add(GameResults c);
        public abstract void Remove(GameResults c);
        public abstract string Display(int depth);
        public abstract void AddPoint(ComponentType leafName);
    }
}
