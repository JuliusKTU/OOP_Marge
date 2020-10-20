using Marge.Domain;
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
    class MoveUpChatMessageCommand : ICommand
    {
        private readonly BoardCoordinatesViewModel _viewModel;
        private readonly SignalRChatService _chatService;

        public MoveUpChatMessageCommand(BoardCoordinatesViewModel viewModel, SignalRChatService chatService)
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

                await _chatService.SendCoordinatesMessage(new BoardCoordinates()
                {
                    messageType = MessageType.playerMovement,
                    color = _viewModel.playerColor,
                    id = _viewModel.UniqueID,
                    message = _viewModel.UniqueID + " has sent a message",
                    x = _viewModel.x,
                    y = _viewModel.y - 1
                });
            }
            catch
            {

            }
        }
    }
}
