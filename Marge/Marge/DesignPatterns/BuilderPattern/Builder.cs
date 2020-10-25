using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.BuilderPattern
{
    public abstract class Builder
    {
        public abstract void BuildPlayerName();
        public abstract void BuildPlayerColor();
        public abstract void BuildPlayerPos();
        public abstract Player GetPlayer();
        public abstract Enemy GetEnemy();

    }
}
