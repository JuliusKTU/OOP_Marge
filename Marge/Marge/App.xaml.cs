using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Marge.ViewModels;
using Marge.GameObjects;
using Marge.DesignPatterns.BuilderPattern;
using Marge.DesignPatterns.StatePattern;
using Marge.DesignPatterns.ProxyPattern;
using Marge.DesignPatterns.FlyweightPattern;

namespace Marge
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //HubConnection connection = new HubConnectionBuilder()
            //    .WithUrl("https://margesignalr20201107074704.azurewebsites.net/margechat")
            //    .Build();

            ConnectionProxy connectionProxy = new ConnectionProxy();

            var player = new PlayerBuilder();
            player.BuildPlayerName();
            player.BuildPlayerColor();
            player.BuildPlayerPos();

            var enemy = new EnemyBuilder();
            enemy.BuildPlayerName();
            enemy.BuildPlayerColor();
            enemy.BuildPlayerPos();
            enemy.passConnection(connectionProxy);

            var darkenBoard = new Darken();
            Board board = new Board(darkenBoard);
            BoardCoordinatesViewModel chatViewModel = BoardCoordinatesViewModel.CreateConnectedViewModel(connectionProxy, player.GetPlayer(), enemy.GetEnemy(), board);
            
            MainWindow window = new MainWindow
            {
                DataContext = new MainViewModel(chatViewModel, board)
            };

            window.Show();

            NeutralTileFactory factory = new NeutralTileFactory();
            for (int i = 0; i < 5; i++)
            {
                AbstractNeutralTile tile = factory.GetNeutralTile("Darkest");
                tile.Display(connectionProxy);
            }

            for (int i = 0; i < 5; i++)
            {
                AbstractNeutralTile tile = factory.GetNeutralTile("Lightest");
                tile.Display(connectionProxy);
            }

        }

        private void SpeedTest(ConnectionProxy connectionProxy)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            NeutralTileFactory factory = new NeutralTileFactory();
            for (int i = 0; i < 10000000; i++)
            {
                AbstractNeutralTile tile = factory.GetNeutralTile("Darkest");
                tile.Display(connectionProxy);
            }

            stopwatch.Stop();

            stopwatch2.Start();
            for (int i = 0; i < 10000000; i++)
            {
                LightestNeutralTile a = new LightestNeutralTile();
                a.Display(connectionProxy);
            }

            stopwatch2.Stop();

            MessageBox.Show("Time taken for 10000000 - using flyweight: " + stopwatch.Elapsed + ", regular: " + stopwatch2.Elapsed);

            stopwatch.Reset();
            stopwatch2.Reset();


            stopwatch.Start();
            for (int i = 0; i < 100000000; i++)
            {
                AbstractNeutralTile tile = factory.GetNeutralTile("Darkest");
                tile.Display(connectionProxy);
            }

            stopwatch.Stop();

            stopwatch2.Start();
            for (int i = 0; i < 100000000; i++)
            {
                LightestNeutralTile a = new LightestNeutralTile();
                a.Display(connectionProxy);
            }

            stopwatch2.Stop();

            MessageBox.Show("Time taken for 100000000 - using flyweight: " + stopwatch.Elapsed + ", regular: " + stopwatch2.Elapsed);

            //for (int i = 0; i < 10; i++)
            //{
            //    AbstractNeutralTile tile = factory.GetNeutralTile("Lightest");
            //    tile.Display(connectionProxy);
            //}
        }

    }
}
