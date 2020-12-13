using Marge.DesignPatterns.ProxyPattern;
using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.FlyweightPattern
{
    abstract class AbstractNeutralTile
    {
        protected string color;
        protected bool IsColored { get; set; }
        protected bool IsActive { get; set; }
        protected TileType TileType { get; set; }

        public abstract void Display(ConnectionProxy chatService);
    }
}
