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
    public class BlackSplash : Debuff
    {

        private ConnectionProxy _chatService;
        public BlackSplash(ConnectionProxy chatService)
        {
            _chatService = chatService;
        }

        public override void SendDebuff()
        {
            _chatService.SendMessage("BlackSplash", 1, "0 0 0", MessageType.debuffBlackSplash, 4, 4);

            if (TilesSet.GetTile(4, 4).IsColored)
            {
                TilesSet.AddTile(4, 4, new Tile(true, true, TileType.DebuffBlackSplash, 4, 4));
            }
            else
            {
                TilesSet.AddTile(4, 4, new Tile(false, true, TileType.DebuffBlackSplash, 4, 4));
            }
        }
    }
}
