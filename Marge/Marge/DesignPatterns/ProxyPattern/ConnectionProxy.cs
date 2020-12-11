using Marge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.ProxyPattern
{
    public class ConnectionProxy : IConnection
    {
        private ServerClient client = new ServerClient();


        
        public void AddMessageReceiver(Action<BoardCoordinates> method)
        {
            client.AddMessageReceiver(method);
        }

       
        public void SendMessage(string mess, int uniqueid, string newColor, MessageType type, int xcoord, int ycoord)
        {
            client.SendMessage(mess, uniqueid, newColor, type, xcoord, ycoord);
        }
    }
}
