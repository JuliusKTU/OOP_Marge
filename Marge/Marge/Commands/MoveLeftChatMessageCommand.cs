using Marge.DesignPatterns.ProxyPattern;
using Marge.DesignPatterns.StrategyPattern;
using Marge.Domain;
using Marge.GameObjects;
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
    class MoveLeftChatMessageCommand : ICommand
    {
        private readonly BoardCoordinatesViewModel _viewModel;
        private readonly ConnectionProxy _chatService;

        Player CurrentPlayer;

        public MoveLeftChatMessageCommand(BoardCoordinatesViewModel viewModel, ConnectionProxy chatService, Player player)
        {
            _viewModel = viewModel;
            _chatService = chatService;
            CurrentPlayer = player;

        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {

            if (!_viewModel.GamePaused && !_viewModel.gameHasEnded)
            {
                if (_viewModel.x - 1 >= 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }


        public void Execute(object parameter)
        {

            if (CurrentPlayer.Strategy == StrategyType.Move)
            {
                _chatService.SendMessage(_viewModel.UniqueID.ToString(), _viewModel.UniqueID, _viewModel.playerColor, MessageType.playerMovement, _viewModel.x - 1, _viewModel.y);
                
            }
            else if (CurrentPlayer.Strategy == StrategyType.Frozen && CurrentPlayer.AffectedCount != 0)
            {
                CurrentPlayer.AffectedCount--;

                if (CurrentPlayer.AffectedCount <= 0)
                {
                    CurrentPlayer.RequestStrategy(StrategyType.Move);
                }
            }
            else if (CurrentPlayer.Strategy == StrategyType.Confused && CurrentPlayer.AffectedCount != 0)
            {
                _chatService.SendMessage(_viewModel.UniqueID.ToString(), _viewModel.UniqueID, _viewModel.playerColor, MessageType.playerMovement, _viewModel.x - 1, _viewModel.y - 1);
                
                CurrentPlayer.AffectedCount--;

                if (CurrentPlayer.AffectedCount <= 0)
                {
                    CurrentPlayer.RequestStrategy(StrategyType.Move);
                }
            }

        }
    }
}
