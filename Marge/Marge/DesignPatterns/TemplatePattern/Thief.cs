using Marge.DesignPatterns.ProxyPattern;
using Marge.Domain;
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
        ConnectionProxy _chatService;
        protected abstract string Hide();

        protected abstract MessageType GetType();

        public void Run(ConnectionProxy chatService)
        {
            _chatService = chatService;
            string hiddenColor = Hide();
            MessageType type = GetType();
            Create(hiddenColor, type);
        }

        public async void Create(string hiddenColor, MessageType type)
        {
            Random randNum = new Random();
            int Randx = randNum.Next(0, 20);
            int Randy = randNum.Next(0, 20);

            _chatService.SendMessage("Thief", 1, hiddenColor, type, Randx, Randy);

        }

    }
}
