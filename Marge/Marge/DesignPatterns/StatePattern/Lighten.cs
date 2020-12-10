using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Marge.DesignPatterns.StatePattern
{
    public class Lighten : State
    {
        public override void Change(Board board)
        {
            if (board.colorCode < 254)
            {
                board.colorCode += 3;
                board.BackgroundColor = Color.FromRgb(255, board.colorCode, board.colorCode).ToString();
                board.UpdateColorVisual();
            }
        }
    }
}
