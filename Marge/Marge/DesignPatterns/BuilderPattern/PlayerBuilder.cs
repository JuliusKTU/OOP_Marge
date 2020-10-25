using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.BuilderPattern
{
    class PlayerBuilder : Builder
    {
        private Player _player = new Player();

        public override void BuildPlayerName()
        {
            _player.Name = "Bebras";
        }

        public override void BuildPlayerColor()
        {
            Random randNum = new Random();
            int UniqueID = randNum.Next(100, 255);
            int UniqueID2 = randNum.Next(100, 255);
            int UniqueID3 = randNum.Next(100, 255);

            string playerColor = UniqueID.ToString() + " " + UniqueID2.ToString() + " " + UniqueID3.ToString();

            _player.Color = playerColor;
        }
        public override void BuildPlayerPos()
        {
            Random randNum = new Random();
            int x = randNum.Next(1, 20);
            int y = randNum.Next(1, 20);
            _player.PosX = x;
            _player.PosY = y;
        }

        public override Player GetPlayer()
        {
            return _player;
        }

        public override Enemy GetEnemy()
        {
            throw new NotImplementedException();
        }
    }
}
