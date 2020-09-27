using Marge.ViewModels;
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

        public SignalRChatService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<BoardCoordinates>("ReceivedCoordinatesMessage", (coordinates) => CoordinatesReceived?.Invoke(coordinates));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendCoordinatesMessage(BoardCoordinates coordinates)
        {
            await _connection.SendAsync("SendCoordinates", coordinates);
        }

    }
}
