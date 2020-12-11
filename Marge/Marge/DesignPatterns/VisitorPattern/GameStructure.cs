using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.VisitorPattern
{
    class GameStructure
    {
        private GameMode _gameMode;

        public void Attach(GameMode gamemode)
        {
            _gameMode = gamemode;
        }

        public void Accept(Visitor gamemode)
        {
           _gameMode.ChangeMode(gamemode);
        }
    }
}
