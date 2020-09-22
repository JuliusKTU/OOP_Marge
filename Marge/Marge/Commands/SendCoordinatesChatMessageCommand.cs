using Marge.Services;
using Marge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Marge.Commands
{
    public class SendCoordinatesChatMessageCommand : ICommand
    {
        private readonly BorderCoordinates _viewModel;
        private readonly SignalRChatService _chatService;

        public SendCoordinatesChatMessageCommand(BorderCoordinates viewModel, SignalRChatService chatService)
        {
            _viewModel = viewModel;
            _chatService = chatService;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _chatService.SendCoordinatesMessage(new BorderCoordinates());
            }
        }
    }
}
