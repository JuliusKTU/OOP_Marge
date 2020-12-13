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
    class FreezeYourself : Debuff
    {
        private ConnectionProxy _chatService;
        public FreezeYourself(ConnectionProxy chatService)
        {
            _chatService = chatService;
        }
      

        public override void SendDebuff()
        {
            _chatService.SendMessage("playerFreeze", 1, "100 149 237", MessageType.debuffFreezeYourself, 10, 10);

            if (TilesSet.GetTile(10, 10).IsColored)
            {
                TilesSet.AddTile(10, 10, new Tile(true, true, TileType.DebuffFreezeYourself, 10, 10));
            }
            else
            {
                TilesSet.AddTile(10, 10, new Tile(false, true, TileType.DebuffFreezeYourself, 10, 10));
            }

        }
    }
}
