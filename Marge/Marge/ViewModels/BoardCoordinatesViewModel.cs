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
using Marge.GameObjects;
using Marge.DesignPatterns.AbstractFactoryPattern;

namespace Marge.ViewModels
{
    public class BoardCoordinatesViewModel : ViewModelBase
    {
        SignalRChatService _chatService;
        public string playerColor = "";
        private string _message { get; set; }
        public int UniqueID{ get; }
        public int UniqueID2 { get; }
        public int UniqueID3 { get; }

        private int _x { get; set; }
        private int _y { get; set; }

        private int StepsCount = 0;
        private int FreezeStepCount = 0;
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
                CurrentPlayerCoordinates.x = value;
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
                CurrentPlayerCoordinates.y = value;
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
            UniqueID = randNum.Next(100, 255);
            UniqueID2 = randNum.Next(100, 255);
            UniqueID3 = randNum.Next(100, 255);
            
            playerColor = UniqueID.ToString() + " " + UniqueID2.ToString() + " " + UniqueID3.ToString();
            //playerColor.Color = Color.FromArgb(255, 255, 255, 0);

            //SendCoordinatesCommand = new SendCoordinatesChatMessageCommand(this, chatService);
            MoveDownChatMessageCommand = new MoveDownChatMessageCommand(this, chatService);
            MoveLeftChatMessageCommand = new MoveLeftChatMessageCommand(this, chatService);
            MoveRightChatMessageCommand = new MoveRightChatMessageCommand(this, chatService);
            MoveUpChatMessageCommand = new MoveUpChatMessageCommand(this, chatService);

            _message = "Waiting for response";
            x = randNum.Next(1, 20);//UniqueID = randNum.Next(1, 9); 
            y = randNum.Next(1, 20);//UniqueID = randNum.Next(1, 9); 
            CurrentPlayerCoordinates.x = x;
            CurrentPlayerCoordinates.y = y;
            chatService.CoordinatesReceived += ChatService_CoordinatesMessageReceived;
            _chatService = chatService;

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
            if (coordinates.messageType == MessageType.playerMovement)
            {
                
                StepsCount++;
                FreezeStepCount++;

                if(StepsCount >= 10)
                {
                    var a = new BonusFactory();
                    a.CreateBonus(1, _chatService).SendBonus();
                    StepsCount = 0;
                }

                if (StepsCount >= 5)
                {
                    var a = new FreezeFactory();
                    a.CreateDebuff(_chatService).SendFreeze();
                    FreezeStepCount = 0;
                }

            }
            if(coordinates.messageType != MessageType.buff && coordinates.id == UniqueID)
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
}
