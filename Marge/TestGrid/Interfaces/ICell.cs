using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGrid.Interfaces
{
    interface ICell
    {
        /// <summary>
        /// State of the cell.
        /// </summary>
        bool State { get; set; }
    }
}
