using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Marge.Domain
{
    public class BoardCoordinates
    {
        public int id { get; set; }
        public string message { get; set; }
        public MessageType messageType { get; set; }
        public string color { get; set; }
        public int random { get; set; }
        public int x { get; set; }
        public int y { get; set; }

    }

    public enum MessageType
    {
        playerMovement,
        buff,
        playerFreeze,
        enemy,
        gameOver
        
    }
}
