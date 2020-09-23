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

        public MainViewModel(BoardCoordinatesViewModel chatViewModel)
        {
            BoardCoordinatesViewModel = chatViewModel;
        }
    }
}
