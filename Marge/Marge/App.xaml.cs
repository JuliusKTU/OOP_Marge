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

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/margechat")
                .Build();

            var SignalRConnection = new Services.SignalRChatService(connection);

            var player = new PlayerBuilder();
            player.BuildPlayerName();
            player.BuildPlayerColor();
            player.BuildPlayerPos();

            var enemy = new EnemyBuilder();
            enemy.BuildPlayerName();
            enemy.BuildPlayerColor();
            enemy.BuildPlayerPos();
            enemy.passConnection(SignalRConnection);

            BoardCoordinatesViewModel chatViewModel = BoardCoordinatesViewModel.CreateConnectedViewModel(SignalRConnection, player.GetPlayer(), enemy.GetEnemy());

            MainWindow window = new MainWindow
            {
                DataContext = new MainViewModel(chatViewModel)
            };

            window.Show();


        }

    }
}
