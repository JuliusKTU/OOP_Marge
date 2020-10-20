using System;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marge.SignalR;
using Marge.ViewModels;
using Marge.Domain;

namespace Marge.SignalR.Hubs
{
    public class MargeChatHub : Hub
    {
        public async Task SendCoordinates(BoardCoordinates coords)
        {
            await Clients.All.SendAsync("ReceivedCoordinatesMessage", coords);
        }

    }
}
