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
    class MoveUpChatMessageCommand : ICommand
    {
        private readonly BoardCoordinatesViewModel _viewModel;
        private readonly SignalRChatService _chatService;

        //this class player to check strategy
        Player CurrentPlayer;

        public MoveUpChatMessageCommand(BoardCoordinatesViewModel viewModel, SignalRChatService chatService, Player player)
        {
            _viewModel = viewModel;
            _chatService = chatService;
            CurrentPlayer = player;
            
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (!_viewModel.GamePaused)
            {
                if (_viewModel.y -1 >= 0)
                return true;
            else
                return false;
            }
            else
            {
                return false;
            }
        }

        public async void Execute(object parameter)
        {
           
            if (CurrentPlayer.Strategy == StrategyType.Move)
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

                await _chatService.SendCoordinatesMessage(new BoardCoordinates()
                {
                    message = _viewModel.UniqueID.ToString(),
                    id = _viewModel.UniqueID,
                    color = _viewModel.playerColor,
                    messageType = MessageType.playerMovement,
                    x = _viewModel.x + 1,
                    y = _viewModel.y - 1
                });

                CurrentPlayer.AffectedCount--;

                if (CurrentPlayer.AffectedCount <= 0)
                {
                    CurrentPlayer.RequestStrategy(StrategyType.Move);
                }
            }

        }
    }
}
