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
using Marge.DesignPatterns.StrategyPattern;
using Marge.DesignPatterns.AdapterPattern;

namespace Marge.ViewModels
{
    public class BoardCoordinatesViewModel : ViewModelBase
    {
        SignalRChatService _chatService;
        public string playerColor = "";
        private string _message { get; set; }
        public int UniqueID { get; }
        public int UniqueID2 { get; }
        public int UniqueID3 { get; }

        private int _x { get; set; }
        private int _y { get; set; }

        private int StepsCount = 0;
        private int FreezeStepCount = 0;
        private int EnemyCount = 0;

        public Player MainPlayer { get; set; }
        public Enemy MainEnemy { get; set; }

        public IScore Score = new AdapterScore();

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
        public int CurrentPlayerScore
        {
            get
            {
                return MainPlayer.Score;
            }
            set
            {
                MainPlayer.Score = value;
                OnPropertyChanged(nameof(CurrentPlayerScore));
            }
        }


        //public ICommand SendCoordinatesCommand { get; }
        public ICommand MoveDownChatMessageCommand { get; }
        public ICommand MoveLeftChatMessageCommand { get; }
        public ICommand MoveRightChatMessageCommand { get; }
        public ICommand MoveUpChatMessageCommand { get; }


        public BoardCoordinatesViewModel(SignalRChatService chatService, Player mainPlayer, Enemy mainEnemy)
        {
            MainPlayer = mainPlayer;
            MainEnemy = mainEnemy;
            Random randNum = new Random();
            UniqueID = randNum.Next(100, 255);
            UniqueID2 = randNum.Next(100, 255);
            UniqueID3 = randNum.Next(100, 255);

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    TilesSet.AddTile(x, y, new Tile(false, true, TileType.Neutral));


                }
            }

            //playerColor = UniqueID.ToString() + " " + UniqueID2.ToString() + " " + UniqueID3.ToString();
            //playerColor.Color = Color.FromArgb(255, 255, 255, 0);

            //SendCoordinatesCommand = new SendCoordinatesChatMessageCommand(this, chatService);
            MoveDownChatMessageCommand = new MoveDownChatMessageCommand(this, chatService, MainPlayer);
            MoveLeftChatMessageCommand = new MoveLeftChatMessageCommand(this, chatService, MainPlayer);
            MoveRightChatMessageCommand = new MoveRightChatMessageCommand(this, chatService, MainPlayer);
            MoveUpChatMessageCommand = new MoveUpChatMessageCommand(this, chatService, MainPlayer);

            _message = "Waiting for response";

            x = MainPlayer.PosX;
            y = MainPlayer.PosY;
            playerColor = MainPlayer.Color;

            CurrentPlayerCoordinates.x = x;
            CurrentPlayerCoordinates.y = y;
            chatService.CoordinatesReceived += ChatService_CoordinatesMessageReceived;
            _chatService = chatService;

        }

        public static BoardCoordinatesViewModel CreateConnectedViewModel(SignalRChatService chatService, Player mainPlayer, Enemy mainEnemy)
        {

            BoardCoordinatesViewModel viewModel = new BoardCoordinatesViewModel(chatService, mainPlayer, mainEnemy);
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
                if (coordinates.id == UniqueID)
                {
                    MainPlayer.Score++;
                }

                StepsCount++;
                FreezeStepCount++;
                EnemyCount++;

                if (StepsCount >= 10)
                {
                    var a = new BonusFactory();
                    Random randNum = new Random();
                    int BonusNumber = randNum.Next(1, 4);
                    a.CreateBonus(BonusNumber, _chatService).SendBonus();
                    StepsCount = 0;
                }

                if (FreezeStepCount >= 7)
                {
                    var a = new FreezeFactory();
                    a.CreateDebuff(_chatService).SendFreeze();
                    a.CreateBuff(_chatService).SendFreeze();
                    FreezeStepCount = 0;
                }

                if (EnemyCount >= 15)
                {
                    MainEnemy.ChangePossition();
                    EnemyCount = 0;
                }

                //jei turi str count bet nedaro
            }

            if (coordinates.messageType != MessageType.buff && coordinates.id == UniqueID)
            {
                _message = coordinates.message;
                _x = coordinates.x;
                _y = coordinates.y;

                if (TilesSet.GetTile(_x, _y).TileType != TileType.Neutral)
                {
                    if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusJackPot)
                    {
                        MainPlayer.PlayerCalculateScore(Score.AddPoints(new BonusFactory().CreateBonus(1, _chatService)));
                        // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " +25 Points");
                        MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                    }
                    else if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusNormal)
                    {
                        MainPlayer.PlayerCalculateScore(Score.AddPoints(new BonusFactory().CreateBonus(3, _chatService)));
                        // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " +10 Points");
                        MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                    }
                    else if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusJoke)
                    {
                        MainPlayer.PlayerCalculateScore(Score.ReducePoints(new BonusFactory().CreateBonus(2, _chatService)));
                        // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " HaHa -5 Points");
                        MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                    }
                    else
                    {
                        MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString());
                    }
                }

                if (TilesSet.GetTile(_x, _y).TileType == TileType.DebuffFreezeYourself)
                {
                    MainPlayer.RequestStrategy(StrategyType.Frozen);
                }

                if (TilesSet.GetTile(_x, _y).TileType == TileType.Enemy)
                {
                    MainPlayer.RequestStrategy(StrategyType.Confused);
                }

                TilesSet.AddTile(_x, _y, new Tile(true, true, TileType.Neutral));
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(x));
                OnPropertyChanged(nameof(y));
                OnPropertyChanged(nameof(CurrentPlayerScore));
            }

        }
    }
}
