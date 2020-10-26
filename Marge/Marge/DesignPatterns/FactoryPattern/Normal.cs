﻿using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.Factory
{
    class Normal : Bonus
    {
        public Normal(SignalRChatService chatService) : base(chatService)
        {
        }

        public override async void SendBonus()
        {
            Random randNum = new Random();
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buff,
                message = "buff",
                color = "255 255 102",
                x = Randx,
                y = Randy
            });

            
            if (Board.GetTile(Randx, 15).IsColored)
            {
                Board.AddTile(Randx, Randy, new Tile(true, true, TileType.BonusNormal));
            }
            else
            {
                Board.AddTile(Randx, Randy, new Tile(false, true, TileType.BonusNormal));

            }
        }

    }
}
