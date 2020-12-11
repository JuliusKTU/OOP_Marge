using Marge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.ProxyPattern
{
    interface IConnection
    {
         void SendMessage(string mess, int uniqueid, string newColor, MessageType type, int xcoord, int ycoord);
         void AddMessageReceiver(Action<Marge.Domain.BoardCoordinates> method);

    }
}
