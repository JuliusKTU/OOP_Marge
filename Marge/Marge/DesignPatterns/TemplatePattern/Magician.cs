using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marge.DesignPatterns.TemplatePattern
{
    public class Magician : Thief
    {

        protected override string Hide()
        {
            string[] bytes = CurrentPlayer.color.Split(' ');
            if(Convert.ToInt32(bytes.Max()) < 220)
            {
                bytes[0] = Convert.ToInt32(bytes[0] + 30).ToString();
                bytes[1] = Convert.ToInt32(bytes[1] + 30).ToString();
                bytes[2] = Convert.ToInt32(bytes[2] + 30).ToString();
            }

            return bytes[0] + " "+ bytes[1] + " "+ bytes[2];
        }

        protected override MessageType GetType()
        {
            return MessageType.magician;
        }
    }
}
