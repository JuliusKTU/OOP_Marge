using Marge.DesignPatterns.ProxyPattern;
using Marge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.FlyweightPattern
{
    class DarkestNeutralTile : AbstractNeutralTile
    {
        public DarkestNeutralTile()
        {
            this.color = "90 90 90";
            this.IsActive = true;
            this.IsColored = false;
            this.TileType = GameObjects.TileType.DarkHole;
        }

        public override void Display(ConnectionProxy chatService)
        {
            
            Random randNum = new Random();
            
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);
            //MessageBox.Show(Randx + " " + Randy);
            chatService.SendMessage("Dark hole", 1, this.color, MessageType.darkHole, Randx, Randy);
        }
    }
}
