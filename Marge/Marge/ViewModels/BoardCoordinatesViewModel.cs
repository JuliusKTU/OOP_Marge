using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Marge.Commands;
using Marge.Domain;
using Marge.Services;

namespace Marge.ViewModels
{
    public class BoardCoordinatesViewModel : ViewModelBase
    {
        private string _message { get; set; }
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        public ICommand SendCoordinatesCommand { get; }

        public BoardCoordinatesViewModel(SignalRChatService chatService)
        {
            SendCoordinatesCommand = new SendCoordinatesChatMessageCommand(this, chatService);
            _message = "Waiting for response";

            chatService.CoordinatesReceived += ChatService_CoordinatesMessageReceived;

        }

        public static BoardCoordinatesViewModel CreateConnectedViewModel(SignalRChatService chatService)
        {
            BoardCoordinatesViewModel viewModel = new BoardCoordinatesViewModel(chatService);
            chatService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    ;
                }
            });
            return viewModel;
        }

        private void ChatService_CoordinatesMessageReceived(BoardCoordinates coordinates)
        {
            _message = coordinates.message;
            OnPropertyChanged(nameof(Message));
            MessageBox.Show("message received");
        }
    }
}
