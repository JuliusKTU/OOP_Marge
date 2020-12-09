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

            //HubConnection connection = new HubConnectionBuilder()
            //    .WithUrl("https://margesignalr20201107074704.azurewebsites.net/margechat")
            //    .Build();

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
                    TilesSet.AddTile(x, y, new Tile(false, true, TileType.Neutral));
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

        public void GenerateGameOverWindow()
        {
            bool even = false;
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    Rectangle ColorBlock = new Rectangle();
                    ColorBlock.Name = "Tile" + x + y;
                    if (even)
                    {
                        ColorBlock.Fill = Brushes.Black;
                        even = false;
                    }
                    else
                    {
                        ColorBlock.Fill = Brushes.White;
                        even = true;
                    }
                    
                    Grid.SetColumn(ColorBlock, x);
                    Grid.SetRow(ColorBlock, y);
                    gridMain.Children.Add(ColorBlock);
                }

                if (even) even = false;
                else even = true;
            }
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
            if (coordinates.messageType == MessageType.buffFreezeOthers)
            {

                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if(coordinates.messageType == MessageType.debuffFreezeYourself)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if (coordinates.messageType == MessageType.buffSplashBomb)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if (coordinates.messageType == MessageType.debuffBlackSplash)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if (coordinates.messageType == MessageType.stepedOnColorSplash)
            {
                string[] words = coordinates.color.Split(' ');
                SetTile(coordinates.x + 1, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 1, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y + 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y - 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x + 2, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 2, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y + 2, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y - 2, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x + 1, coordinates.y + 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 1, coordinates.y + 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x + 1, coordinates.y - 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 1, coordinates.y - 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if (coordinates.messageType == MessageType.stepedOnBlackSplash)
            {
                string[] words = coordinates.color.Split(' ');
                SetTile(coordinates.x + 1, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 1, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y + 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y - 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x + 2, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 2, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y + 2, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x, coordinates.y - 2, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x + 1, coordinates.y + 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 1, coordinates.y + 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x + 1, coordinates.y - 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                SetTile(coordinates.x - 1, coordinates.y - 1, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if(coordinates.messageType == MessageType.enemy || coordinates.messageType == MessageType.dazePlayerEnemy || coordinates.messageType == MessageType.stealPointEnemy || coordinates.messageType == MessageType.teleportPlayerEnemy)
            {
                if(coordinates.messageType == MessageType.enemy)
                {
                    string[] words = coordinates.color.Split(' ');

                    SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.Enemy));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.EnemyWithAbilities));
                }
                
            }
            else if (coordinates.messageType == MessageType.bonusJackPot)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                if (TilesSet.GetTile(coordinates.x, coordinates.y).IsColored)
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.BonusJackPot));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.BonusJackPot));

                }

            }
            else if (coordinates.messageType == MessageType.bonusNormal)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                if (TilesSet.GetTile(coordinates.x, coordinates.y).IsColored)
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.BonusNormal));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.BonusNormal));

                }

            }
            else if (coordinates.messageType == MessageType.bonusJoke)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                if (TilesSet.GetTile(coordinates.x, coordinates.y).IsColored)
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.BonusJoke));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.BonusJoke));

                }

            }
            else if (coordinates.messageType == MessageType.gameOver)
            {
                
                GenerateGameOverWindow();

            }
            else if (coordinates.messageType == MessageType.magician)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.Magician));

            }
            else if (coordinates.messageType == MessageType.masterThief)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.MasterThief));

            }
            else if(coordinates.messageType == MessageType.gamePause || coordinates.messageType == MessageType.gamePauseUndo)
            {
                ;
            }
            else
            {
                string[] words = coordinates.color.Split(' ');

                SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                
            }

        }

    }
}
