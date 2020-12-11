using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using Marge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Marge.Commands
{
    public class RestartGame : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly BoardCoordinatesViewModel _viewModel;
        private readonly SignalRChatService _chatService;
        Player CurrentPlayer;

        public RestartGame(BoardCoordinatesViewModel viewModel, SignalRChatService chatService)
        {
            _viewModel = viewModel;
            _chatService = chatService;
           

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {

            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                message = _viewModel.UniqueID.ToString(),
                id = _viewModel.UniqueID,
                color = _viewModel.playerColor,
                messageType = MessageType.reset,
                x = _viewModel.MainPlayer.PosX,
                y = _viewModel.MainPlayer.PosY
            });
        }
    }
}
