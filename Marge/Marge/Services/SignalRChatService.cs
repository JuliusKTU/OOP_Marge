﻿using Marge.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marge.Domain;

namespace Marge.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<BoardCoordinates> CoordinatesReceived;
        public event Action<BoardCoordinates> BonusReceived;

        public SignalRChatService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<BoardCoordinates>("ReceivedCoordinatesMessage", (coordinates) => CoordinatesReceived?.Invoke(coordinates));
            _connection.On<BoardCoordinates>("ReceivedBonusMessage", (coordinates) => BonusReceived?.Invoke(coordinates));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendCoordinatesMessage(BoardCoordinates coordinates)
        {
            await _connection.SendAsync("SendCoordinates", coordinates);
        }

        public async Task SendBonus(BoardCoordinates coordinates)
        {
            await _connection.SendAsync("BonusMessage", coordinates);
        }

    }
}
