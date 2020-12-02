using Marge.Domain;
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

        public override async void Create()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                color = "123 0 129",
                messageType = MessageType.magician,
                x = 2,
                y = 2
            });
        }

        public override void Hide()
        {
            MessageBox.Show("Hidden");
        }

        public override void Steal()
        {
            MessageBox.Show("Stolen");
        }
    }
}
