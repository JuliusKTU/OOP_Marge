using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.Memento
{
    public class PlayerMemento
    {
        private string _name { get; set; }
        private string _color { get; set; }
        private int _posX { get; set; }
        private int _posY { get; set; }

        private int _score = 0;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public int PosX
        {
            get
            {
                return _posX;
            }
            set
            {
                _posX = value;
            }
        }

        public int PosY
        {
            get
            {
                return _posY;
            }
            set
            {
                _posY = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        public PlayerMemento(string name, string color, int posx, int posy, int score)
        {
            this._name = name;
            this._color = color;
            this._posX = posx;
            this._posY = posy;
            this._score = score;
        }
    }
}
