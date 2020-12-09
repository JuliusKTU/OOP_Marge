using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Marge.GameObjects
{
    public class Tile
    {
        public bool IsColored { get; set; }

        public bool IsActive{ get; set; }

        public TileType TileType { get; set; }

        public int x { get; set; }
        public int y { get; set; }

        public Tile(bool Colored, bool Active, TileType Type, int xcoord, int ycoord)
        {
            IsColored = Colored;
            IsActive = Active;
            TileType = Type;
            x = xcoord;
            y = ycoord;
        }
    }

    public enum TileType
    {
        BuffFreezeOthers,
        BuffColorSplash,
        DebuffFreezeYourself,
        DebuffBlackSplash,
        BonusJackPot,
        BonusNormal,
        BonusJoke,
        Neutral,
        Enemy,
        EnemyWithAbilities,
        Magician,
        MasterThief

    }
}
