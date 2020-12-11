using Marge.Domain;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.ProxyPattern
{
    class ServerClient : IConnection
    {
        Services.SignalRChatService SignalRConnection;

        public ServerClient()
        {
            Connect();
        }

        public void AddMessageReceiver(Action<Marge.Domain.BoardCoordinates> method)
        {
            SignalRConnection.CoordinatesReceived += method;

        }

        private void Connect()
        {
            MessageBox.Show("Connected");
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/margechat")
                .Build();

            SignalRConnection = new Services.SignalRChatService(connection);
            SignalRConnection.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    ;
                }
            });
        }

        public async void SendMessage(string mess, int uniqueid, string newColor, MessageType type, int xcoord, int ycoord)
        {
            await SignalRConnection.SendCoordinatesMessage(new BoardCoordinates()
            {
                message = mess,
                id = uniqueid,
                color = newColor,
                messageType = type,
                x = xcoord,
                y = ycoord
            });
        }
    }
}
