using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGrid.Interfaces
{
    interface ICellViewModel
    {
        ICell Cell { get; set; }
        ICommand ChangeCellStateCommand { get; }
    }
}
