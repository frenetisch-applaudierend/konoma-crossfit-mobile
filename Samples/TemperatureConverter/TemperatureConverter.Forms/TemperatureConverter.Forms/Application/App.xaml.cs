using Konoma.CrossFit;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Services;
using TemperatureConverter.Forms.Application.Converter;
using TemperatureConverter.Forms.Application.Login;
using TemperatureConverter.Forms.Services;
using Xamarin.Forms.Xaml;

namespace TemperatureConverter.Forms.Application
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            StartApplication();
        }

        protected override void RegisterPlattformServices(IServiceRegistration services)
        {
            services.AddSingleton<IPreferencesService, PreferencesService>();
        }

        protected override void RegisterNavigationPoints(TemperatureConverterCoordinator coordinator)
        {
            coordinator.ShowLogin.Connect(
                FormsNavigation.MainPage(this, () => new LoginPage()).InNavigationPage());

            coordinator.ShowHome.Connect(
                FormsNavigation.MainPage(this, () => new ConverterPage()).InNavigationPage());
        }
    }
}
