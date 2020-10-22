﻿using Marge.Domain;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    class FreezeOthers : Buff
    {
        private SignalRChatService _chatService;

        public FreezeOthers(SignalRChatService chatService)
        {
            _chatService = chatService;
        }

        public override async void SendFreeze()
        {

            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                messageType = MessageType.buff,
                message = "buff",
                color = "100 100 200",
                x = 11,
                y = 11
            });

        }
    }
}
