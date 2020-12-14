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
        private TilesCollection _aggregate;

        private int _currentx = 0;
        private int _currenty = 0;

        private bool _even = false;

        public BoardIterator(TilesCollection aggregate, bool even = false)
        {
            _aggregate = aggregate;
            _even = even;

            if (even)
            {
                _currentx = 0;
                _currenty = 0;
            }
        }

        public override object CurrentItem()
        {
            return _aggregate[_currentx, _currenty];
        }

        public override object First()
        {
            return _aggregate[0, 0];
        }

        public override bool IsDone()
        {
            return _currentx >= 19 && _currenty >= 19;
        }

        public override object Next()
        {

            if (_currenty + 2 < _aggregate.Count)
            {
                _currenty += 2;
                return _aggregate[_currentx, _currenty];
                
            }
            else
            {
                if (_currentx + 1 < _aggregate.Count)
                {
                    _currentx += 1;
                    _currenty = 0;
                    return _aggregate[_currentx, _currenty];
   
                }
                else
                {
                    return null;
                }

            }

        }

    }
}
