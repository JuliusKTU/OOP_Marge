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
    class LightestNeutralTile : AbstractNeutralTile
    {
        public LightestNeutralTile()
        {
            this.color = "140 140 140";
            this.IsActive = true;
            this.IsColored = false;
            this.TileType = GameObjects.TileType.LightHole;
        }

        public override void Display(ConnectionProxy chatService)
        {
            Random randNum = new Random();
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);
            //MessageBox.Show(Randx + " " + Randy);
           // chatService.SendMessage("Light hole", 1, this.color, MessageType.lightHole, Randx, Randy);
        }
    }
}
