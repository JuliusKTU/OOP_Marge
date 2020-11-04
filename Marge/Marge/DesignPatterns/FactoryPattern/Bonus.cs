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
        protected SignalRChatService _chatService;
        public Bonus(SignalRChatService chatService)
        {
            _chatService = chatService;
        }
        public abstract void SendBonus(); 
        public abstract int ReturnAmount();
    }
}
