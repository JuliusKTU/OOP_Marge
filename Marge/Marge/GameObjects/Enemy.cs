using Marge.Domain;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.GameObjects
{
    public class Enemy
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public SignalRChatService _chatService;

        public Enemy()
        {

        }
        public async void ChangePossition()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.enemy,
                message = "enemy",
                color = "255 255 255",
                x = PosX,
                y = PosY
            });

            TilesSet.AddTile(PosX, PosY, new Tile(true, true, TileType.Neutral));
            Random randNum = new Random();
            PosX = randNum.Next(0, 20);
            PosY = randNum.Next(0, 20);
            
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.enemy,
                message = "enemy",
                color = "255 0 0",
                x = PosX,
                y = PosY
            });
            TilesSet.AddTile(PosX, PosY, new Tile(true, true, TileType.Enemy));

        }

    }
}
