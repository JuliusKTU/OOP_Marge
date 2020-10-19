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
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using Marge.DesignPatterns.FactoryPattern;

namespace Marge.ViewModels
{
    public class BoardCoordinatesViewModel : ViewModelBase
    {
        private string _message { get; set; }
        public int UniqueID { get; }

        private int _x { get; set; }
        private int _y { get; set; }


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
        public int x
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged(nameof(x));
            }
        }
        public int y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                OnPropertyChanged(nameof(y));
            }
        }


        //public ICommand SendCoordinatesCommand { get; }
        public ICommand MoveDownChatMessageCommand { get; }
        public ICommand MoveLeftChatMessageCommand { get; }
        public ICommand MoveRightChatMessageCommand { get; }
        public ICommand MoveUpChatMessageCommand { get; }


        public BoardCoordinatesViewModel(SignalRChatService chatService)
        {

            Random randNum = new Random();
            UniqueID = randNum.Next(1, 100);

            //SendCoordinatesCommand = new SendCoordinatesChatMessageCommand(this, chatService);
            MoveDownChatMessageCommand = new MoveDownChatMessageCommand(this, chatService);
            MoveLeftChatMessageCommand = new MoveLeftChatMessageCommand(this, chatService);
            MoveRightChatMessageCommand = new MoveRightChatMessageCommand(this, chatService);
            MoveUpChatMessageCommand = new MoveUpChatMessageCommand(this, chatService);

            _message = "Waiting for response";
            x = 3;//UniqueID = randNum.Next(1, 9); 
            y = 3;//UniqueID = randNum.Next(1, 9); 

            

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

            BonusFactory a = new BonusFactory();
            a.CreateBonus(1, chatService).SendBonus();

            return viewModel;
        }


        private void ChatService_CoordinatesMessageReceived(BoardCoordinates coordinates)
        {
            _message = coordinates.message;
            _x = coordinates.x;
            _y = coordinates.y;
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(x));
            OnPropertyChanged(nameof(y));

        }
    }
}
