using Marge.Domain;
using Marge.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.TemplatePattern
{
    public class MasterThief : Thief
    {
        protected override string Hide()
        {
            //219, 217, 255
            //183, 158, 254
            //143, 68, 254
            Random randNum = new Random();
            int Randx = randNum.Next(0, 3);
            if( Randx == 0)
            {
                return "219 217 255";
            }
            else if(Randx == 1)
            {
                return "183 158 254";
            }
            else
            {
                return "143 68 254";
            }

        }

        protected override MessageType GetType()
        {
            return MessageType.masterThief;
        }
    }
}
