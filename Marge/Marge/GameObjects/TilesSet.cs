using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Marge.GameObjects
{
    public static class TilesSet
    {
        static Tile[,] TilesMatrix = new Tile[20,20];

        public static void AddTile(int x, int y, Tile tile)
        {
            TilesMatrix[y,x] = tile;
        }

        public static Tile GetTile(int x, int y)
        {
            return TilesMatrix[y,x];
        }

    }
}
