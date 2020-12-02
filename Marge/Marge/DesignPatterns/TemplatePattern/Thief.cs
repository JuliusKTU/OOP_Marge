using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.TemplatePattern
{
    public abstract class Thief
    {
        protected SignalRChatService _chatService;
        public abstract void Steal();
        public abstract void Hide();
        public abstract void Create();

        public void Run(SignalRChatService chatService)
        {
            _chatService = chatService;
            Create();
        }

    }
}
