using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.GameObjects
{
    public class Board
    {
        public int Lenght { get; set; }
        public int Width { get; set; }

        public void Draw()
        {

        }

        public Board(int lenght, int width)
        {
            Lenght = lenght;
            Width = width;
        }
       


        

        //deep copies all the object
        //return new board parameters - current variables
    }
}
