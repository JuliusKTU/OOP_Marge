using Marge.DesignPatterns.BridgePattern;
using Marge.DesignPatterns.ProxyPattern;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.Factory
{
    public abstract class Bonus
    {
        protected ConnectionProxy _chatService;

        public Dictionary<YellowColorShades, ColorSet> ColorOptions = new Dictionary<YellowColorShades, ColorSet>();

        public Bonus(ConnectionProxy chatService)
        {
            _chatService = chatService;
           
            ColorOptions.Add(YellowColorShades.Light, new LightYellowColor());
            ColorOptions.Add(YellowColorShades.Dark, new DarkYellowColor());
            ColorOptions.Add(YellowColorShades.Normal, new YellowColor());
        }


        public abstract void SendBonus(); 
        public abstract int ReturnAmount();

    }

    public enum YellowColorShades
    {
        Light,
        Dark,
        Normal
    }


}
