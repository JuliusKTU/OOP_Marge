using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.FlyweightPattern
{
    class NeutralTileFactory
    {
        private Dictionary<string, AbstractNeutralTile> _tiles =new Dictionary<string, AbstractNeutralTile>();

        public AbstractNeutralTile GetNeutralTile(string key)
        {
            AbstractNeutralTile tile = null;
            if (_tiles.ContainsKey(key))
            {
                tile = _tiles[key];
            }
            else
            {
                switch (key)
                {
                    case "Darkest": tile = new DarkestNeutralTile(); break;
                    case "Lightest": tile = new LightestNeutralTile(); break;
                }
                _tiles.Add(key, tile);
            }
            return tile;
        }
    }
}
