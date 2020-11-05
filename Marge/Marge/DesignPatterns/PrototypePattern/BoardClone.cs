using Marge.DesignPatterns.PrototypePattern;
using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Marge.DesignPatterns.ClonePattern
{
    public class BoardClone : IPrototype
    {
        
        private Board gameBoard;
        public BoardClone(Board board)
        {
            gameBoard = board;
        }

        object IPrototype.Clone()
        {
            //shallow
           return this.MemberwiseClone();
            //deep
            //return new Board(gameBoard);
        }
    }

    
}
