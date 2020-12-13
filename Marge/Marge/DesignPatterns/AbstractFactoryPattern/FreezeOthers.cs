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
    class FreezeOthers : Buff
    {
        private ConnectionProxy _chatService;

        public FreezeOthers(ConnectionProxy chatService)
        {
            _chatService = chatService;
        }

        public override void SendBuff()
        {
            _chatService.SendMessage("buff", 1, "100 100 200", MessageType.buffFreezeOthers, 11, 11);

            if (TilesSet.GetTile(11,11).IsColored)
            {
                TilesSet.AddTile(11, 11, new Tile(true, true, TileType.BuffFreezeOthers, 11, 11));
            } 
            else
            {
                TilesSet.AddTile(11, 11, new Tile(false, true, TileType.BuffFreezeOthers, 11, 11));

            }
            

        }
    }
}
