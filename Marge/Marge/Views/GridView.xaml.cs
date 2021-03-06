﻿using Marge.DesignPatterns.IteratorPattern;
using Marge.DesignPatterns.ProxyPattern;
using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using Marge.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            ConnectionProxy chatService = new ConnectionProxy();
            chatService.AddMessageReceiver(ChatService_CoordinatesMessageReceived);

        }
        

        public void AddTiles()
        {

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    TilesSet.AddTile(x, y, new Tile(false, true, TileType.Neutral, x, y));
                    MessageBox.Show("Hello I am created");
                    Rectangle ColorBlock = new Rectangle();
                    ColorBlock.Name = "Tile" + x + y;
                    ColorBlock.Fill = Brushes.Aqua;
                    Grid.SetColumn(ColorBlock, x);
                    Grid.SetRow(ColorBlock, y);
                    gridMain.Children.Add(ColorBlock);
                }
            }
        }

        public void ClearBoard()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    TilesSet.AddTile(x, y, new Tile(false, true, TileType.Neutral, x, y));
                    Rectangle ColorBlock = new Rectangle();
                    ColorBlock.Fill = Brushes.White;
                    Grid.SetColumn(ColorBlock, x);
                    Grid.SetRow(ColorBlock, y);
                    gridMain.Children.Add(ColorBlock);
                }
            }
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

        public void GenerateGameOverWindow2()
        {
            var BoardIter = new TilesCollection();
            for (int i = 0; i < 20; i++)
            {
                for (int y = 0; y < 20; y++)
                {
                    BoardIter[i, y] = TilesSet.GetTile(i, y);
                }
            }

            AbstractIterator iter = BoardIter.CreateIterator();

            object item = iter.First();

            while (item != null)
            {
                Rectangle ColorBlock = new Rectangle();
                ColorBlock.Name = "";

                ColorBlock.Fill = Brushes.Black;
                Tile tile = (Tile)item;
                Grid.SetColumn(ColorBlock, tile.x);
                Grid.SetRow(ColorBlock, tile.y);
                gridMain.Children.Add(ColorBlock);
                item = iter.Next();
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
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.Enemy, coordinates.x, coordinates.y));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.EnemyWithAbilities, coordinates.x, coordinates.y));
                }
                
            }
            else if (coordinates.messageType == MessageType.bonusJackPot)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                if (TilesSet.GetTile(coordinates.x, coordinates.y).IsColored)
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.BonusJackPot, coordinates.x, coordinates.y));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.BonusJackPot, coordinates.x, coordinates.y));

                }

            }
            else if (coordinates.messageType == MessageType.bonusNormal)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                if (TilesSet.GetTile(coordinates.x, coordinates.y).IsColored)
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.BonusNormal, coordinates.x, coordinates.y));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.BonusNormal, coordinates.x, coordinates.y));

                }

            }
            else if (coordinates.messageType == MessageType.bonusJoke)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                if (TilesSet.GetTile(coordinates.x, coordinates.y).IsColored)
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.BonusJoke, coordinates.x, coordinates.y));
                }
                else
                {
                    TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.BonusJoke, coordinates.x, coordinates.y));

                }

            }
            else if (coordinates.messageType == MessageType.gameOver)
            {
                
                GenerateGameOverWindow2();

            }
            else if (coordinates.messageType == MessageType.magician)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.Magician, coordinates.x, coordinates.y));

            }
            else if (coordinates.messageType == MessageType.masterThief)
            {
                string[] words = coordinates.color.Split(' ');

                SetEllipse(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));

                TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(false, true, TileType.MasterThief, coordinates.x, coordinates.y));

            }
            else if(coordinates.messageType == MessageType.gamePause || coordinates.messageType == MessageType.gamePauseUndo)
            {
                ;
            }
            else if (coordinates.messageType == MessageType.reset)
            {
                ClearBoard();
                string[] words = coordinates.color.Split(' ');
                //SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
            }
            else if (coordinates.messageType == MessageType.darkHole)
            {
                string[] words = coordinates.color.Split(' ');
                SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.DarkHole, coordinates.x, coordinates.y));
            }
            else if (coordinates.messageType == MessageType.lightHole)
            {
                string[] words = coordinates.color.Split(' ');
                SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                TilesSet.AddTile(coordinates.x, coordinates.y, new Tile(true, true, TileType.LightHole, coordinates.x, coordinates.y));
            }
            else
            {
                if(TilesSet.GetTile(coordinates.x, coordinates.y).TileType == TileType.DarkHole || TilesSet.GetTile(coordinates.x, coordinates.y).TileType == TileType.LightHole)
                {
                    
                }
                else
                {
                    string[] words = coordinates.color.Split(' ');

                    SetTile(coordinates.x, coordinates.y, new SolidColorBrush(Color.FromRgb(Byte.Parse(words[0]), Byte.Parse(words[1]), Byte.Parse(words[2]))));
                }

            }

        }

    }
}
