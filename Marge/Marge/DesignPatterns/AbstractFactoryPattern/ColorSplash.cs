using Marge.DesignPatterns.ProxyPattern;
using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.AbstractFactoryPattern
{
    public class ColorSplash : Buff
    {
        private ConnectionProxy _chatService;
        public ColorSplash(ConnectionProxy chatService)
        {

            _chatService = chatService;
        }

        public override void SendBuff()
        {
            _chatService.SendMessage("SplashBomb", 1, "255 51 51", MessageType.buffSplashBomb, 5, 5);

            if (TilesSet.GetTile(5, 5).IsColored)
            {
                TilesSet.AddTile(5, 5, new Tile(true, true, TileType.BuffColorSplash, 5, 5));
            }
            else
            {
                TilesSet.AddTile(5, 5, new Tile(false, true, TileType.BuffColorSplash, 5, 5));
            }
        }
    }
}
