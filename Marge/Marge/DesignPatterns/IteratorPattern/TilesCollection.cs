using Marge.DesignPatterns.Iterator;
using Marge.GameObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.IteratorPattern
{
    public class TilesCollection : IteratorAggregate
    {
        private Tile[,] allTiles;
        public void AddItems(Tile[,] items)
        {
            allTiles = items;
        }
        public Tile GetItem(int x, int y)
        {
            return allTiles[y, x];
        }

        public int HowManyTiles()
        {
            return allTiles.Length;
        }

        public override IEnumerator GetEnumerator()
        {
           yield return new BoardIterator(this, false) ;
        }
    }
}
