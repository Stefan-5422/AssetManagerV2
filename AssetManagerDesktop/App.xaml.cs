using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using AssetManagerDesktop.Services;
using AssetManagerDesktop.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagerDesktop
{
    /// <summary>
	/// Interaction logic for App.xaml
	/// </summary
	public partial class App : Application
	{
        private readonly ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<UserConfigProvider>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginPage>();
            services.AddSingleton<MainPage>();
            services.AddSingleton<IRemoteFileService,RemoteFileService>();

            serviceProvider = services.BuildServiceProvider();
        }

        protected void OnStartup(object sender, StartupEventArgs startupEventArgs)
        {
            MainWindow window = serviceProvider.GetService<MainWindow>()!;
            window.Show();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            serviceProvider.GetService<UserConfigProvider>()?.Deconstruct();
        }
    }
}
