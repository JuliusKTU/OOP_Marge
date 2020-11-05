using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.PrototypePattern
{
    public interface  IPrototype 
    {
        object Clone();
    }
}
