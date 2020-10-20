﻿using Marge.Domain;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.Factory
{
    class Joke : Bonus
    {
        public Joke(SignalRChatService chatService) : base(chatService)
        {
        }

        public override async void SendBonus()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buff,
                message = "buff",
                x = 8,
                y = 8
            });
        }
    }
}
