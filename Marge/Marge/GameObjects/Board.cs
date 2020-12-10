using Marge.DesignPatterns.StatePattern;
using Marge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Marge.GameObjects
{
    public class Board : ViewModelBase
    {
        private State _state;
        public int Lenght { get; set; }
        public int Width { get; set; }
        private string backgroundColor { get; set; }
        public byte colorCode = 255;
        public string BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public State State
        {
            get { return _state; }
            set
            {
                _state = value;
               // MessageBox.Show("State: " + _state.GetType().Name);
            }
        }

        public void Draw()
        {

        }

        public Board(int lenght, int width, State state)
        {
            Lenght = lenght;
            Width = width;
            State = state;
            
        }
        public Board(State state)
        {
            State = state;
            BackgroundColor = Color.FromRgb(255, 255, 255).ToString();
            OnPropertyChanged(nameof(BackgroundColor));
        }

        public void Request()
        {
            _state.Change(this);
        }

        public void UpdateColorVisual()
        {
            OnPropertyChanged(nameof(BackgroundColor));
        }

        //deep copies all the object
        //return new board parameters - current variables
    }
}
