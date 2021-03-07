using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Caidi.App.Models;
using Caidi.App.ViewModels;
using Caidi.App.Views;
using Microsoft.Extensions.Configuration;

namespace Caidi.App
{
    public class App : Application
    {
        private readonly string ConfigFile = Environment.GetEnvironmentVariable("Environment") == "Development"
            ? "appsettings.Development.json"
            : "appsettings.json";
        
        private IConfiguration Configuration;
        public override void Initialize()
        {
            LoadConfiguration();
            Configure();
            AvaloniaXamlLoader.Load(this);
        }

        private void Configure()
        {
            
        }

        private void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(ConfigFile, false);

            Configuration = builder.Build();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}