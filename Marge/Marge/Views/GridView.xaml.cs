using Marge.Domain;
using Marge.GameObjects;
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
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    Board.AddTile(x, y, new Tile(false, true, TileType.Neutral));
                    MessageBox.Show("Hello I am created");
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

        public void SetTile(int x, int y, SolidColorBrush color)
        {
            Rectangle ColorBlock = new Rectangle();
            ColorBlock.Name = "Tile" + x + y;
            ColorBlock.Fill = color;
            Grid.SetColumn(ColorBlock, x);
            Grid.SetRow(ColorBlock, y);
            
            gridMain.Children.Add(ColorBlock);
        }

        public void SetEllipse(int x, int y, SolidColorBrush color)
        {
            Ellipse ColorBlock = new Ellipse();
            ColorBlock.Name = "Tile" + x + y;
            ColorBlock.Fill = color;
            ColorBlock.Stroke = Brushes.Black;
            ColorBlock.Stretch = Stretch.Fill;
            Grid.SetColumn(ColorBlock, x);
            Grid.SetRow(ColorBlock, y);

            gridMain.Children.Add(ColorBlock);
        }

        private void ChatService_CoordinatesMessageReceived(BoardCoordinates coordinates)
        {
            string mess = coordinates.message;
            if (coordinates.messageType == MessageType.buff)
            {

                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if(coordinates.messageType == MessageType.playerFreeze)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if(coordinates.messageType == MessageType.enemy)
            {
                string[] words = coordinates.color.Split(' ');

                SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else
            {
                string[] words = coordinates.color.Split(' ');

                SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                
            }

        }

    }
}
