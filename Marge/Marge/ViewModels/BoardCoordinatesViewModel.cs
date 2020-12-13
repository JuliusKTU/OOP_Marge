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
using System.Runtime.Remoting.Contexts;
using Marge.DesignPatterns.Interpreter;
using Marge.DesignPatterns.VisitorPattern;
using Marge.DesignPatterns.MementoPattern;
using Marge.DesignPatterns.ProxyPattern;

namespace Marge.ViewModels
{
    public class BoardCoordinatesViewModel : ViewModelBase
    {
        SignalRChatService _chatService;
        ConnectionProxy _connectionProxy;
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
        private int BonusCount = 0;
        private int FreezeYourselfStepCount = 0;
        private int FreezeOthersStepCount = 0;
        private int EnemyCount = 0;
        private int BlackSplashCount = 0;
        private int ColorSplashCount = 0;
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

        List<ExpressionRoman> tree;
        GameStructure game;

        public MementoCareTaker memento { get; private set; }

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
        public ICommand RestartGame { get; }

        public BoardCoordinatesViewModel(SignalRChatService chatService, ConnectionProxy connectionProxy, Player mainPlayer, Enemy mainEnemy)
        {

            _connectionProxy = connectionProxy;
            MainPlayer = mainPlayer;

            memento = new MementoCareTaker();
            memento.Memento = MainPlayer.CreateMemento();

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
            MoveDownChatMessageCommand = new MoveDownChatMessageCommand(this, _connectionProxy, MainPlayer);
            MoveLeftChatMessageCommand = new MoveLeftChatMessageCommand(this, _connectionProxy, MainPlayer);
            MoveRightChatMessageCommand = new MoveRightChatMessageCommand(this, _connectionProxy, MainPlayer);
            MoveUpChatMessageCommand = new MoveUpChatMessageCommand(this, _connectionProxy, MainPlayer);
            Pause = new Pause(this, _connectionProxy);
            RestartGame = new RestartGame(this, _connectionProxy);

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

            _connectionProxy.AddMessageReceiver(ChatService_CoordinatesMessageReceived);

            //chatService.CoordinatesReceived += ChatService_CoordinatesMessageReceived;
            _chatService = chatService;
            facade = new Facade(_connectionProxy);

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

            /* INTERPRETER DESIGN PATTERN IMPLEMENTATION */
            tree = new List<ExpressionRoman>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());
            /* INTERPRETER DESIGN PATTERN IMPLEMENTATION */


            /* VISITOR DESIGN PATTERN IMPLEMENTATION */
            game = new GameStructure();
            game.Attach(new Hard());

            BonusSpawnRate bonusSpawn = new BonusSpawnRate();
            BuffSpawnRate buffSpawn = new BuffSpawnRate();
            DebuffSpawnRate debuffSpawn = new DebuffSpawnRate();

            game.Accept(bonusSpawn);
            game.Accept(buffSpawn);
            game.Accept(debuffSpawn);
            /* VISITOR DESIGN PATTERN IMPLEMENTATION */

        }

        public static BoardCoordinatesViewModel CreateConnectedViewModel(SignalRChatService chatService, ConnectionProxy connectionProxy, Player mainPlayer, Enemy mainEnemy, Board currboard)
        {
            board = currboard;
            BoardCoordinatesViewModel viewModel = new BoardCoordinatesViewModel(chatService, connectionProxy, mainPlayer, mainEnemy);
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
                    if (StepsCount == 0)
                    {
                        board.State = new Darken();
                    }
                    else if (StepsCount == SpawnRates.StepsCount / 2)
                    {
                        board.State = new Lighten();
                    }

                    board.Request();
                    if (StepsCount >= SpawnRates.StepsCount)
                    {
                        StepsCount = -1;
                    }

                    //currByte -= 1;
                    //BackgroundColor = Color.FromRgb(255, currByte, currByte).ToString();
                    //OnPropertyChanged(nameof(BackgroundColor));

                    if (coordinates.id == UniqueID)
                    {
                        MainPlayer.Score++;
                    }

                    if (BonusCount > SpawnRates.BonusCount)
                    {
                        //var a = new BonusFactory();
                        Random randNum = new Random();
                        int BonusNumber = randNum.Next(1, 4);
                        //a.CreateBonus(BonusNumber, _chatService).SendBonus();
                        facade.CreateBonus(BonusNumber);
                        BonusCount = 0;
                        //MainPlayer.SetMemento(memento.Memento);

                    }

                    if (FreezeYourselfStepCount >= SpawnRates.FreezeYourselfStepCount)
                    {
                        facade.CreateDeBuff(TileType.DebuffFreezeYourself);
                        FreezeYourselfStepCount = 0;
                    }

                    if (FreezeOthersStepCount >= SpawnRates.FreezeOthersStepCount)
                    {
                        facade.CreateBuff(TileType.BuffFreezeOthers);
                        FreezeOthersStepCount = 0;
                    }

                    if (BlackSplashCount >= SpawnRates.BlackSplashCount)
                    {
                        facade.CreateDeBuff(TileType.DebuffBlackSplash);
                        BlackSplashCount = 0;
                    }
                    if (ColorSplashCount >= SpawnRates.ColorSplashCount)
                    {
                        facade.CreateBuff(TileType.BuffColorSplash);
                        ColorSplashCount = 0;
                    }

                    if (EnemyCount >= SpawnRates.EnemyCount)
                    {
                        MainEnemy.ChangePossition();
                        dazeEnemy.Operation(MainEnemy.PosX, MainEnemy.PosY, _connectionProxy);
                        EnemyCount = 0;
                    }

                    if (MagicianCount > SpawnRates.MagicianCount)
                    {
                        Thief newThief = new Magician();
                        newThief.Run(_connectionProxy);
                        MagicianCount = 0;
                    }

                    if (MasterThiefCount > SpawnRates.MasterThiefCount)
                    {
                        Thief newThief2 = new MasterThief();
                        newThief2.Run(_connectionProxy);
                        MasterThiefCount = 0;
                    }

                    StepsCount++;
                    BonusCount++;
                    FreezeYourselfStepCount++;
                    FreezeOthersStepCount++;
                    EnemyCount++;
                    BlackSplashCount++;
                    ColorSplashCount++;
                    MasterThiefCount++;
                    MagicianCount++;

                    //jei turi str count bet nedaro

                    if (coordinates.id == UniqueID)
                    {
                        _message = coordinates.message;
                        _x = coordinates.x;
                        _y = coordinates.y;


                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusJackPot)
                        {
                            root.AddPoint(ComponentType.JackPot);
                            MainPlayer.PlayerCalculateScore(Score.AddPoints(new BonusFactory().CreateBonus(1, _connectionProxy)));
                            // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " +25 Points");
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusNormal)
                        {
                            root.AddPoint(ComponentType.Normal);
                            MainPlayer.PlayerCalculateScore(Score.AddPoints(new BonusFactory().CreateBonus(3, _connectionProxy)));
                            // MessageBox.Show(Board.GetTile(_x, _y).TileType.ToString() + " +10 Points");
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString() + MainPlayer.Score);
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.BonusJoke)
                        {
                            root.AddPoint(ComponentType.Joke);
                            MainPlayer.PlayerCalculateScore(Score.ReducePoints(new BonusFactory().CreateBonus(2, _connectionProxy)));
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
                            MainPlayer.SendSteppedOnColorSplash(_connectionProxy, _x, _y);
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString());
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.DebuffBlackSplash)
                        {
                            root.AddPoint(ComponentType.BlackSplash);
                            MainPlayer.SendSteppedOnBlackSplash(_connectionProxy, _x, _y);
                            MessageBox.Show(TilesSet.GetTile(_x, _y).TileType.ToString());
                        }
                        if (TilesSet.GetTile(_x, _y).TileType == TileType.Magician)
                        {

                            Random randNum = new Random();
                            int number = randNum.Next(1, 1000);
                            string roman = ToRoman(number);

                            ContextRoman context = new ContextRoman(roman);
                            foreach (ExpressionRoman exp in tree)
                            {
                                exp.Interpret(context);
                            }

                            string message = "Does " + roman + " = " + number + " ?";
                            string title = "Answer the question";

                            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.No)
                            {
                                IceDamageDealer.ProcessRequest(DamageDealerType.MagitianDamage, MainPlayer);
                            }

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
                        MainPlayer.PosX = x;
                        MainPlayer.PosY = y;
                        OnPropertyChanged(nameof(y));
                        OnPropertyChanged(nameof(CurrentPlayerScore));
                    }
                }

                if (coordinates.messageType == MessageType.gamePause || coordinates.messageType == MessageType.gamePauseUndo)
                {
                    SetGamePause();
                }

                if (MainPlayer.Score >= 100)
                {
                    MessageBox.Show(root.Display(1));
                    MainPlayer.SendGameOverMessage(_connectionProxy, UniqueID);
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

                if (coordinates.messageType == MessageType.gameOver)
                {
                    gameHasEnded = true;

                }

                if (coordinates.messageType == MessageType.reset)
                {
                    MainPlayer.SetMemento(memento.Memento);
                    x = MainPlayer.PosX;
                    y = MainPlayer.PosY;
                    OnPropertyChanged(nameof(x));
                    OnPropertyChanged(nameof(y));
                    OnPropertyChanged(nameof(CurrentPlayerScore));
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

        public string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
    }
}
