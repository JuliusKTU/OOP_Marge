﻿using System;
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
using Marge.DesignPatterns.FacadePattern;
using Marge.DesignPatterns.DecoratorPattern;
using Marge.DesignPatterns.Iterator;
using Marge.DesignPatterns.IteratorPattern;
using Marge.DesignPatterns.TemplatePattern;
using Marge.DesignPatterns.CompositePattern;
using Marge.Enumerators;
using System.Drawing;
using Marge.DesignPatterns.StatePattern;
using Marge.DesignPatterns.ChainOfResponsibilityPattern;

namespace Marge.ViewModels
{
    public class BoardCoordinatesViewModel : ViewModelBase
    {
        SignalRChatService _chatService;
        public string playerColor = "";

        private bool _gamePaused = false;
        private string _gamePauseTitle = "Pause";
        private string _message { get; set; }
        public int UniqueID { get; }
        public int UniqueID2 { get; }
        public int UniqueID3 { get; }

        private int _x { get; set; }
        private int _y { get; set; }
        private static Board board { get; set; }

        private int StepsCount = 0;
        private int FreezeStepCount = 0;
        private int EnemyCount = 0;
        private int SplashCount = 0;
        private int MasterThiefCount = 0;
        private int MagicianCount = 0;
        public bool gameHasEnded = false;
        public byte currByte = 255;

        public Facade facade;
        public Player MainPlayer { get; set; }
        public Enemy MainEnemy { get; set; }
        StealPointsAbility enemySteal;
        TeleportAbility teleportEnemy;
        DazePlayerAbility dazeEnemy;
        Composite root;

        DamageDealer EnemyDamageDealer;
        DamageDealer MagicianDamageDealer;
        DamageDealer ThiefDamageDealer;
        DamageDealer IceDamageDealer;

        public IScore Score = new AdapterScore();

        public bool GamePaused
        {
            get
            {
                return _gamePaused;
            }
            set
            {
                _gamePaused = value;
                OnPropertyChanged(nameof(GamePaused));
            }
        }

        public string GamePauseTitle
        {
            get
            {
                return _gamePauseTitle;
            }
            set
            {
                _gamePauseTitle = value;
                OnPropertyChanged(nameof(GamePauseTitle));
            }
        }

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
                CurrentPlayer.x = value;
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
                CurrentPlayer.y = value;
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
        public ICommand Pause { get; }


        public BoardCoordinatesViewModel(SignalRChatService chatService, Player mainPlayer, Enemy mainEnemy)
        {


            MainPlayer = mainPlayer;
            MainEnemy = mainEnemy;
            Random randNum = new Random();
            UniqueID = randNum.Next(100, 255);
            UniqueID2 = randNum.Next(100, 255);
            UniqueID3 = randNum.Next(100, 255);

            CurrentPlayer.color = UniqueID.ToString() + " " + UniqueID2.ToString() + " " + UniqueID3.ToString();

            enemySteal = new StealPointsAbility();
            teleportEnemy = new TeleportAbility();
            dazeEnemy = new DazePlayerAbility();

            enemySteal.SetEnemy(MainEnemy);
            teleportEnemy.SetEnemy(enemySteal);
            dazeEnemy.SetEnemy(teleportEnemy);


            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    TilesSet.AddTile(x, y, new Tile(false, true, TileType.Neutral, x, y));

                }
            }

            //playerColor = UniqueID.ToString() + " " + UniqueID2.ToString() + " " + UniqueID3.ToString();
            //playerColor.Color = Color.FromArgb(255, 255, 255, 0);

            //SendCoordinatesCommand = new SendCoordinatesChatMessageCommand(this, chatService);
            MoveDownChatMessageCommand = new MoveDownChatMessageCommand(this, chatService, MainPlayer);
            MoveLeftChatMessageCommand = new MoveLeftChatMessageCommand(this, chatService, MainPlayer);
            MoveRightChatMessageCommand = new MoveRightChatMessageCommand(this, chatService, MainPlayer);
            MoveUpChatMessageCommand = new MoveUpChatMessageCommand(this, chatService, MainPlayer);
            Pause = new Pause(this, chatService);

            _message = "Waiting for response";

            x = MainPlayer.PosX;
            y = MainPlayer.PosY;
            playerColor = MainPlayer.Color;

            EnemyDamageDealer = new EnemyDamage();
            MagicianDamageDealer = new MagicianDamage();
            ThiefDamageDealer = new ThiefDamage();
            IceDamageDealer = new IceDamage();

            IceDamageDealer.SetSuccessor(ThiefDamageDealer);
            ThiefDamageDealer.SetSuccessor(MagicianDamageDealer);
            MagicianDamageDealer.SetSuccessor(EnemyDamageDealer);

            CurrentPlayer.x = x;
            CurrentPlayer.y = y;
            chatService.CoordinatesReceived += ChatService_CoordinatesMessageReceived;
            _chatService = chatService;
            facade = new Facade(chatService);

            root = new Composite(ComponentType.Effect);
            //Buffai

            Composite buff = new Composite(ComponentType.Buff);
            Composite negativeBuff = new Composite(ComponentType.Negative); //negative
            Composite positiveBuff = new Composite(ComponentType.Positive); //positive
            positiveBuff.Add(new Leaf(ComponentType.ColorSplash));
            positiveBuff.Add(new Leaf(ComponentType.FreezeOthers));

            negativeBuff.Add(new Leaf(ComponentType.BlackSplash));
            negativeBuff.Add(new Leaf(ComponentType.FreezeYourself));

            buff.Add(negativeBuff);
            buff.Add(positiveBuff);

            // /Buffai
            // Bonusai

            Composite bonus = new Composite(ComponentType.Bonus);
            bonus.Add(new Leaf(ComponentType.JackPot));
            bonus.Add(new Leaf(ComponentType.Normal));
            bonus.Add(new Leaf(ComponentType.Joke));

            //Bonusai

            root.Add(buff);
            root.Add(bonus);

            //root.AddPoint(ComponentType.BlackSplash);
            //root.Display(1);

            

            //Bonusai

            //foreach (var item in BoardIter)
            //{
            //    MessageBox.Show(item.ToString());
            //}
        }

        public static BoardCoordinatesViewModel CreateConnectedViewModel(SignalRChatService chatService, Player mainPlayer, Enemy mainEnemy, Board currboard)
        {
            board = currboard;
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
            if (!gameHasEnded)
            {
                if (coordinates.messageType == MessageType.playerMovement)
                {
                    if(StepsCount < 17)
                    {
                        board.State = new Darken();
                        board.Request();
                    }
                    else
                    {
                        board.State = new Lighten();
                        board.Request();
                    }
                    
                    //currByte -= 1;
                    //BackgroundColor = Color.FromRgb(255, currByte, currByte).ToString();
                    //OnPropertyChanged(nameof(BackgroundColor));

                    if (coordinates.id == UniqueID)
                    {
                        MainPlayer.Score++;
                    }

                    if (StepsCount > 31)
                    {
                        //var a = new BonusFactory();
                        Random randNum = new Random();
                        int BonusNumber = randNum.Next(1, 4);
                        //a.CreateBonus(BonusNumber, _chatService).SendBonus();
                        facade.CreateBonus(BonusNumber);
                        StepsCount = 0;
                    }

                    if (FreezeStepCount >= 23)
                    {
                        //var a = new FreezeFactory();
                        //a.CreateDebuff(_chatService).SendFreeze();
                        // a.CreateBuff(_chatService).SendFreeze();
                        facade.CreateDeBuff(TileType.DebuffFreezeYourself);
                        facade.CreateBuff(TileType.BuffFreezeOthers);
                        FreezeStepCount = 0;
                    }

                    if (SplashCount >= 27)
                    {
                        facade.CreateDeBuff(TileType.DebuffBlackSplash);
                        facade.CreateBuff(TileType.BuffColorSplash);
                        SplashCount = 0;
                    }

                    StepsCount++;
                    FreezeStepCount++;
                    EnemyCount++;
                    SplashCount++;
                    MasterThiefCount++;
                    MagicianCount++;

                    if (EnemyCount >= 21)
                    {
                        //enemy call

                        MainEnemy.ChangePossition();
                        dazeEnemy.Operation(MainEnemy.PosX, MainEnemy.PosY, _chatService);
                        EnemyCount = 0;
                    }

                    if(MasterThiefCount > 5)
                    {
                        Thief newThief = new Magician();
                        newThief.Run(_chatService);
                        MasterThiefCount = 0;
                    }

                    if (MagicianCount > 7 )
                    {
                        Thief newThief2 = new MasterThief();
                        newThief2.Run(_chatService);
                        MagicianCount = 0;
                    }

                    //jei turi str count bet nedaro

                    if (coordinates.id == UniqueID)
                    {
                        _message = coordinates.message;
                        _x = coordinates.x;
                        _y = coordinates.y;


                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusJackPot)
                        {
                            root.AddPoint(ComponentType.JackPot);
                            MainPlayer.PlayerCalculateScore(Score.AddPoints(new BonusFactory().CreateBonus(1, _chatService)));
                            // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " +25 Points");
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusNormal)
                        {
                            root.AddPoint(ComponentType.Normal);
                            MainPlayer.PlayerCalculateScore(Score.AddPoints(new BonusFactory().CreateBonus(3, _chatService)));
                            // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " +10 Points");
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusJoke)
                        {
                            root.AddPoint(ComponentType.Joke);
                            MainPlayer.PlayerCalculateScore(Score.ReducePoints(new BonusFactory().CreateBonus(2, _chatService)));
                            // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " HaHa -5 Points");
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.DebuffFreezeYourself)
                        {
                            IceDamageDealer.ProcessRequest(DamageDealerType.IceDamage, MainPlayer);
                            root.AddPoint(ComponentType.FreezeYourself);
                            MainPlayer.RequestStrategy(StrategyType.Frozen);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BuffFreezeOthers)
                        {
                            root.AddPoint(ComponentType.FreezeOthers);
                            MainPlayer.RequestStrategy(StrategyType.Frozen);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BuffColorSplash)
                        {
                            root.AddPoint(ComponentType.ColorSplash);
                            MainPlayer.SendSteppedOnColorSplash(_chatService, _x, _y);
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString());
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.DebuffBlackSplash)
                        {
                            root.AddPoint(ComponentType.BlackSplash);
                            MainPlayer.SendSteppedOnBlackSplash(_chatService, _x, _y);
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString());
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.Magician)
                        {
                            IceDamageDealer.ProcessRequest(DamageDealerType.MagitianDamage, MainPlayer);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.MasterThief)
                        {
                            IceDamageDealer.ProcessRequest(DamageDealerType.ThiefDamage, MainPlayer);
                        }

                        if (TilesSet.GetTile(_x, _y).TileType == TileType.Enemy)
                        {
                            IceDamageDealer.ProcessRequest(DamageDealerType.EnemyDamage, MainPlayer);
                            MainPlayer.RequestStrategy(StrategyType.Confused);
                            
                        }

                        TilesSet.AddTile(_x, _y, new Tile(true, true, TileType.Neutral, _x, _y));
                        OnPropertyChanged(nameof(Message));
                        OnPropertyChanged(nameof(x));
                        OnPropertyChanged(nameof(y));
                        OnPropertyChanged(nameof(CurrentPlayerScore));
                    }
                }

                if(coordinates.messageType == MessageType.gamePause || coordinates.messageType == MessageType.gamePauseUndo)
                {
                    SetGamePause();
                }

                if (MainPlayer.Score >= 100)
                {
                    MessageBox.Show(root.Display(1));
                    MainPlayer.SendGameOverMessage(_chatService, UniqueID);
                    MainPlayer.Score = 0;
                    //gameHasEnded = true;

                    //var BoardIter = new TilesCollection();
                    //for (int i = 0; i < 20; i++)
                    //{
                    //    for (int y = 0; y < 20; y++)
                    //    {
                    //        BoardIter[i, y] = TilesSet.GetTile(i, y);
                    //    }
                    //}
                    //AbstractIterator iter = BoardIter.CreateIterator();

                    //object item = iter.First();

                    //while (item != null)
                    //{
                    //    MessageBox.Show(item.ToString());
                    //    item = iter.Next();
                    //}
                }

                if(coordinates.messageType == MessageType.gameOver)
                {
                    gameHasEnded = true;

                }
            }

        }

        public void SetGamePause()
        {

            _gamePaused = !_gamePaused;
            OnPropertyChanged(nameof(GamePaused));

            if (_gamePaused)
                _gamePauseTitle = "Unpause";
            else
                _gamePauseTitle = "Pause";

            OnPropertyChanged(nameof(GamePauseTitle));
        }
    }
}
