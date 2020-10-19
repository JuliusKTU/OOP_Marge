﻿using Marge.Domain;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Marge.DesignPatterns.Factory
{
    class JackPot : Bonus
    {
        public JackPot(SignalRChatService chatService) : base(chatService)
        {
        }

        public override async void SendBonus()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                message = "buff",
                x = 8,
                y = 8
            });
        }

    }
}
