using Marge.Domain;
using Marge.Services;
using Marge.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Marge.Views
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : UserControl
    {
        SignalRChatService chatService { get; }

        private int x = -1;
        private int y = -1;
        public GridView()
        {
            InitializeComponent();

            //AddTiles();

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/margechat")
                .Build();

            chatService = new Services.SignalRChatService(connection);

            chatService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    ;
                }
            });

            chatService.CoordinatesReceived += ChatService_CoordinatesMessageReceived;
        }
        

        public void AddTiles()
        {

            //< Rectangle Fill = "Black" IsHitTestVisible = "False"
            //           Grid.Column = "1" Grid.Row = "2" />
            for (int x=0; x<10; x++)
            {
                for (int y=0; y<10; y++)
                {
                    Rectangle ColorBlock = new Rectangle();
                    ColorBlock.Name = "Tile" + x + y;
                    ColorBlock.Fill = Brushes.Aqua;
                    Grid.SetColumn(ColorBlock, x);
                    Grid.SetRow(ColorBlock, y);
                    gridMain.Children.Add(ColorBlock);
                }
            }

            //chatService.CoordinatesReceived += ChatService_CoordinatesMessageReceived;

        }

        public void SetTile(int x, int y, Brush color)
        {
            Rectangle ColorBlock = new Rectangle();
            ColorBlock.Name = "Tile" + x + y;
            ColorBlock.Fill = color;
            Grid.SetColumn(ColorBlock, x);
            Grid.SetRow(ColorBlock, y);
            
            gridMain.Children.Add(ColorBlock);
        }

        private void ChatService_CoordinatesMessageReceived(BoardCoordinates coordinates)
        {
            string mess = coordinates.message;
            if(x != -1 && y != -1)
            {
                SetTile(coordinates.x, coordinates.y, Brushes.Black);
                SetTile(x, y, Brushes.Red);
            }
            x = coordinates.x;
            y = coordinates.y;
            

        }

    }
}
