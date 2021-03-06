﻿using Marge.DesignPatterns.DecoratorPattern;
using Marge.DesignPatterns.ProxyPattern;
using Marge.Domain;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.GameObjects
{
    public class Enemy : AbstractEnemy
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public ConnectionProxy _chatService;

        public Enemy()
        {

        }
        public void ChangePossition()
        {

            _chatService.SendMessage("enemy", 1, "255 255 255", MessageType.enemy, PosX, PosY);
            //await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            //{
            //    messageType = MessageType.enemy,
            //    message = "enemy",
            //    color = "255 255 255",
            //    x = PosX,
            //    y = PosY
            //});

            TilesSet.AddTile(PosX, PosY, new Tile(true, true, TileType.Neutral, PosX, PosY));
            Random randNum = new Random();
            PosX = randNum.Next(0, 20);
            PosY = randNum.Next(0, 20);
            _chatService.SendMessage("enemy", 1, "255 0 0", MessageType.enemy, PosX, PosY);
            //await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            //{
            //    messageType = MessageType.enemy,
            //    message = "enemy",
            //    color = "255 0 0",
            //    x = PosX,
            //    y = PosY
            //});
            TilesSet.AddTile(PosX, PosY, new Tile(true, true, TileType.Enemy, PosX, PosY));

        }

        public override async void Operation(int xa, int ya,ConnectionProxy _chatService)
        {
            //Random randNum = new Random();
            //PosX = randNum.Next(0, 20);
            //PosY = randNum.Next(0, 20);

            //await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            //{
            //    messageType = MessageType.enemy,
            //    message = "enemy",
            //    color = "255 0 0",
            //    x = PosX,
            //    y = PosY
            //});
        }
    }
    public enum EnemyEffectOnPlayer
    {
        DazeCurrentPlayer,
        StealPoints,
        Teleport,
        Normal
    }
}
