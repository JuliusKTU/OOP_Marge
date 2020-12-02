using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marge.DesignPatterns.IteratorPattern;
using System.Windows;

namespace Marge.DesignPatterns.Iterator
{
    public class BoardIterator : AbstractIterator
    {
        private TilesCollection _collection;

        private int xposition = -1;
        private int yposition = -1;

        private bool _even = false;

        public BoardIterator(TilesCollection tileset, bool even = false)
        {
            _collection = tileset;
            _even = even;

            if (even)
            {
                xposition = 0;
                yposition = 0;
            }
        }

        public override object Current()
        {
            return _collection.GetItem(xposition, yposition);
        }

        public override int[] Key()
        {
            int[] array = new int[2];
            array[0] = xposition;
            array[1] = yposition;
            return array;
        }

        public override bool MoveNext()
        {
            MessageBox.Show("Inside");
            if (yposition + 2 < _collection.HowManyTiles())
            {
                yposition += 2;
                MessageBox.Show("Inside IF");
            }
            else
            {
                MessageBox.Show("Inside ELSE");
                if (xposition + 1 <= _collection.HowManyTiles())
                {
                    xposition += 1;
                    yposition = 0;
                    return true;
                }
                else
                {
                    MessageBox.Show("Inside ELSE ELSE ");
                    return false;
                }
                
            }

            return true;

        }

        public override void Reset()
        {
            xposition = 0;
            yposition = 0;

        }
    }
}
