using Marge.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<BorderCoordinates> CoordinatesReceived;

        public SignalRChatService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<BorderCoordinates>("ReceivedCoordinatesMessage", (coordinates) => CoordinatesReceived?.Invoke(coordinates));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendCoordinatesMessage(BorderCoordinates coordinates)
        {
            await _connection.SendAsync("SendCoordinates", coordinates);
        }
    }
}
