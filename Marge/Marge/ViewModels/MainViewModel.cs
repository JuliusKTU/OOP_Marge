using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.ViewModels
{
    class MainViewModel
    {
        public BoardCoordinatesViewModel BoardCoordinatesViewModel { get; }
        public Board Board {get;}

        public MainViewModel(BoardCoordinatesViewModel chatViewModel, Board board)
        {
            BoardCoordinatesViewModel = chatViewModel;
            Board = board;
        }
    }
}
