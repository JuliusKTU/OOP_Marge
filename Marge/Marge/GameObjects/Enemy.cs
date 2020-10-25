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

            Board.AddTile(PosX, PosY, new Tile(true, true, TileType.Neutral));

            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.enemy,
                message = "enemy",
                color = "255 0 0",
                x = 3,
                y = 3
            });
            PosX = 3;
            PosY = 3;
            Board.AddTile(3, 3, new Tile(true, true, TileType.Enemy));

        }

    }
}
