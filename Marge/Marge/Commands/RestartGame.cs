using Marge.DesignPatterns.ProxyPattern;
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
        private readonly ConnectionProxy _chatService;

        public RestartGame(BoardCoordinatesViewModel viewModel, ConnectionProxy chatService)
        {
            _viewModel = viewModel;
            _chatService = chatService;
           

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _chatService.SendMessage(_viewModel.UniqueID.ToString(), _viewModel.UniqueID, _viewModel.playerColor, MessageType.reset, _viewModel.MainPlayer.PosX, _viewModel.MainPlayer.PosY);
        }
    }
}
