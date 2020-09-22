using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Marge.Services;

namespace Marge.ViewModels
{
    public class BorderCoordinates
    {

        ICommand SendCoordinatesChatMessageCommand { get; }

        public BorderCoordinates(SignalRChatService chatService)
        {

        }
    }
}
