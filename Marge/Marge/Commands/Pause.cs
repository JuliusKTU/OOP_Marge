using Marge.DesignPatterns.ProxyPattern;
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
    class Pause : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private BoardCoordinatesViewModel _viewModel;
        ConnectionProxy _chatService;
        public Pause(BoardCoordinatesViewModel viewModel, ConnectionProxy chatService)
        {
            _viewModel = viewModel;
            _chatService = chatService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public  void Execute(object parameter)
        {
            _chatService.SendMessage("", 1, "", MessageType.gamePause, 1, 1);
            
        }

        public  void Undo()
        {
            _chatService.SendMessage("", 1, "", MessageType.gamePauseUndo, 1, 1);
            
        }
    }
}
