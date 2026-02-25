using ClassroomManagement.Infrastructure.DependencyInjection;
using ClassroomManagementApp1.Services;
using ClassroomManagementApp1.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows;

namespace ClassroomManagementApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;

        public static IServiceProvider Services =>
            ((App)Current)._host?.Services ?? throw new InvalidOperationException("Application host is not initialized.");

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host = CreateHostBuilder().Build();
            _host.Start();

            var signInView = _host.Services.GetRequiredService<SignInView>();
            signInView.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
            base.OnExit(e);
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(AppContext.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddInfrastructure(context.Configuration);
                    services.AddSingleton<ICurrentStudentContext, CurrentStudentContext>();

                    services.AddTransient<SignInView>();
                });
        }
    }

}
