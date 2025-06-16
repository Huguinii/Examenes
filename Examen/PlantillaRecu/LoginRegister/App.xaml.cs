using LoginRegister.ViewModel;
using LoginRegister.Service;
using LoginRegister.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using LoginRegister.Services;
using LoginRegister.Models;
using LoginRegister.View;


namespace LoginRegister
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Current.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //view principal
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ComentariosViewModel>();


            //view viewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistroViewModel>();
            services.AddTransient<ComentariosViewModel>();
            services.AddTransient<ViewModelBase>();



            //Services
            services.AddSingleton<LoginDTO>();
            services.AddSingleton<UsuarioDTO>();
            services.AddSingleton<IUsuarioServiceToApi, UsuarioServiceToApi>();
            services.AddSingleton<IStringUtils, StringUtils>();
            services.AddSingleton(typeof(IHttpJsonProvider<>), typeof(HttpJsonService<>));
            services.AddSingleton(typeof(IFileService<>), typeof(FileService<>));
            return services.BuildServiceProvider();
        }
    }
}


