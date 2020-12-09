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
    public class TilesCollection : Aggregate
    {


        private Tile[,] allTiles = new Tile[20, 20];
        //public void AddItems(Tile[,] items)
        //{
        //    allTiles = items;
        //}
        //public Tile GetItem(int x, int y)
        //{
        //    return allTiles[y, x];
        //}

        public int Count
        {
            get { return 20;/*allTiles.Length;*/ }
        }

        public Tile this[int x, int y]
        {
            get { return allTiles[y, x]; }
            set { allTiles[y, x] = value; }
        }

        //public override IEnumerator GetEnumerator()
        //{
        //   yield return new BoardIterator(this, false) ;
        //}
        public override AbstractIterator CreateIterator()
        {
            return new BoardIterator(this);
        }
    }
}
