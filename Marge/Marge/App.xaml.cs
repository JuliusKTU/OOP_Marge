using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Marge.ViewModels;

namespace Marge
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/margechat")
                .Build();

            BoardCoordinatesViewModel chatViewModel = BoardCoordinatesViewModel.CreateConnectedViewModel(new Services.SignalRChatService(connection));

            MainWindow window = new MainWindow
            {
                DataContext = new MainViewModel(chatViewModel)
            };

            window.Show();

        }

    }
}
