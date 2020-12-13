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
            for (int i = 0; i < 10; i++)
            {
                AbstractNeutralTile tile = factory.GetNeutralTile("Darkest");
                tile.Display(connectionProxy);
            }
            for (int i = 0; i < 10; i++)
            {
                AbstractNeutralTile tile = factory.GetNeutralTile("Lightest");
                tile.Display(connectionProxy);
            }


        }

    }
}
