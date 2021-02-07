using Konoma.CrossFit;
using Konoma.CrossFit.Forms;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Services;
using TemperatureConverter.Forms.Application.Converter;
using TemperatureConverter.Forms.Application.Login;
using TemperatureConverter.Forms.Services;
using Xamarin.Forms.Xaml;
using Navigation = Konoma.CrossFit.Forms.Navigation;

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
                new Navigation.MainPage<LoginScene>(this, () => new LoginPage()));

            coordinator.ShowHome.Connect(
                new Navigation.MainPage<ConverterScene>(this, () => new ConverterPage()));
        }
    }
}
