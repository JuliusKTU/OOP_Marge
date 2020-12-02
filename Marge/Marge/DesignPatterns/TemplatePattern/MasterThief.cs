using Marge.Domain;
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
        public override async void Create()
        {
            await _chatService.SendCoordinatesMessage(new BoardCoordinates()
            {
                color = "201 0 129",
                messageType = MessageType.masterThief,
                x = 7,
                y = 7
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
