using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.Interpreter
{
    class ContextRoman
    {
        private string _input;
        private int _output;

        public ContextRoman(string input)
        {
            this._input = input;
        }

        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        public int Output
        {
            get { return _output; }
            set { _output = value; }
        }
    }
}
