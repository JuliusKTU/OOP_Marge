using Marge.DesignPatterns.AdapterPattern;
using Marge.DesignPatterns.Memento;
using Marge.DesignPatterns.ProxyPattern;
using Marge.DesignPatterns.StrategyPattern;
using Marge.Domain;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Marge.GameObjects
{
    public class Player
    {
        private string _name { get; set; }
        private string _color { get; set; }
        private int _posX { get; set; }
        private int _posY { get; set; }
        private PlayerMemento privatePlayerMemento;

        private int _score = 0;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public int PosX
        {
            get
            {
                return _posX;
            }
            set
            {
                _posX = value;
            }
        }

        public int PosY
        {
            get
            {
                return _posY;
            }
            set
            {
                _posY = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        public StrategyType Strategy { get; set; }
        public int AffectedCount { get; set; }

        //atributai
        Dictionary<StrategyType, IMovingStatusStrategy> strategyContext = new Dictionary<StrategyType, IMovingStatusStrategy>();

        public Player()
        {
            Score = 0;
            strategyContext.Add(StrategyType.Confused, new Confusion());
            strategyContext.Add(StrategyType.Frozen, new Freeze());
            strategyContext.Add(StrategyType.Reversed, new Reverse());
            strategyContext.Add(StrategyType.Move, new Move());
            Strategy = strategyContext[StrategyType.Move].MovementChange();
            AffectedCount = 0;
        }

        public void GetStrategy(IMovingStatusStrategy strategy)
        {

        }
        public void RequestStrategy(StrategyType strategy)
        {
            Strategy = strategyContext[strategy].MovementChange();
            AffectedCount = 6;
        }

        public void PlayerCalculateScore(int score)
        {
            Score += score;
        }

        public void SendGameOverMessage(ConnectionProxy _chatService, int currenPlayerId)
        {
            _chatService.SendMessage("Game over", currenPlayerId, "255 0 0", MessageType.gameOver, 0, 0);
        }

        public void SendSteppedOnColorSplash(ConnectionProxy _chatService, int xP, int yP)
        {
            _chatService.SendMessage("buff effect", 1, Color, MessageType.stepedOnColorSplash, xP, yP);
        }

        public void SendSteppedOnBlackSplash(ConnectionProxy _chatService, int xP, int yP)
        {
            _chatService.SendMessage("debuff effect", 1, "0 0 0", MessageType.stepedOnBlackSplash, xP, yP);

        }

        public PlayerMemento CreateMemento()
        {
            return (new PlayerMemento(_name, _color, _posX, _posY, _score));
        }

        public void CreatePrivateMemento()
        {
            privatePlayerMemento = new PlayerMemento(_name, _color, _posX, _posY, _score);
        }

        public void SetMemento(PlayerMemento memento)
        {
            Name = memento.Name;
            Color = memento.Color;
            PosX = memento.PosX;
            PosY = memento.PosY;
            Score = memento.Score;
        }

        public void SetPrivateMemento()
        {
            Name = privatePlayerMemento.Name;
            Color = privatePlayerMemento.Color;
            PosX = privatePlayerMemento.PosX;
            PosY = privatePlayerMemento.PosY;
            Score = privatePlayerMemento.Score;
        }
    }
}
