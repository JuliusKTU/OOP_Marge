using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGrid.Interfaces
{
    public interface IDynamicGridViewModel
    {
        /// <summary>
        /// 2-dimensional collections for CellViewModels.
        /// </summary>
        ObservableCollection<ObservableCollection<ICellViewModel>>
          Cells
        { get; }

        /// <summary>
        /// Number of grid columns.
        /// </summary>
        int GridWidth { get; }

        /// <summary>
        /// Number of grid rows.
        /// </summary>
        int GridHeight { get; }

        /// <summary>
        /// Start, the lightest, color of cells.
        /// </summary>s
        Color StartColor { get; set; }

        /// <summary>
        /// Finish, the darkest, color of cells.
        /// </summary>
        Color FinishColor { get; set; }

        /// <summary>
        /// Color of borders around cells.
        /// </summary>
        Color BorderColor { get; set; }
    }
}
