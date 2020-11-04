using Marge.DesignPatterns.AdapterPattern;
using Marge.DesignPatterns.StrategyPattern;
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
        public string Name { get; set; }
        public string Color { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public StrategyType Strategy { get; set; }
        public int AffectedCount { get; set; }

        public int Score = 0;

        //atributai
        Dictionary<StrategyType, IMovingStatusStrategy> strategyContext = new Dictionary<StrategyType, IMovingStatusStrategy>();

        public Player()
        {
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
    }
}
