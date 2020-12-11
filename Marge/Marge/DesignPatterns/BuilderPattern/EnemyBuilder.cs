using Marge.DesignPatterns.ProxyPattern;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.BuilderPattern
{
    class EnemyBuilder : Builder 
    {
        private Enemy _enemy = new Enemy();

        public override void BuildPlayerName()
        {
            _enemy.Name = "Zudikas";
        }

        public override void BuildPlayerColor()
        {
            string playerColor = 255 + " " + 255 + " " + 255;
            _enemy.Color = playerColor;
        }
        public override void BuildPlayerPos()
        {
            Random randNum = new Random();
            int x = randNum.Next(1, 20);
            int y = randNum.Next(1, 20);
            _enemy.PosX = x;
            _enemy.PosY = y;
        }
        public void passConnection(ConnectionProxy chatService)
        {
            _enemy._chatService = chatService;
        }
        public override Enemy GetEnemy()
        {
            return _enemy;
        }

        public override Player GetPlayer()
        {
            throw new NotImplementedException();
        }
    }
}
