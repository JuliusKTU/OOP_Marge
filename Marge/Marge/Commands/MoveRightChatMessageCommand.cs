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
    class MoveRightChatMessageCommand : ICommand
    {
        private readonly BoardCoordinatesViewModel _viewModel;
        private readonly SignalRChatService _chatService;

        Player CurrentPlayer;

        public MoveRightChatMessageCommand(BoardCoordinatesViewModel viewModel, SignalRChatService chatService, Player player)
        {
            _viewModel = viewModel;
            _chatService = chatService;
            CurrentPlayer = player;

        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
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
                    x = _viewModel.x + 1,
                    y = _viewModel.y
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

        }
    }
}
