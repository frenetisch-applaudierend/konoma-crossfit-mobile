using Konoma.CrossFit;
using Konoma.CrossFit.iOS;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Services;
using TemperatureConverter.Native.iOS.Application.Converter;
using TemperatureConverter.Native.iOS.Application.Login;
using TemperatureConverter.Native.iOS.Services;

namespace TemperatureConverter.Native.iOS.Application
{
    public class AppDelegate : CrossFitAppDelegate<TemperatureConverterCoordinator>
    {
        protected override void RegisterPlatformServices(IServiceRegistration services)
        {
            services.AddSingleton<IPreferencesService, PreferencesService>();
        }

        protected override void RegisterNavigationPoints(TemperatureConverterCoordinator coordinator)
        {
            coordinator.ShowLogin.RegisterRoot(Window!, () => new LoginViewController());
            coordinator.ShowHome.RegisterRoot(Window!, () => new ConverterViewController());
        }
    }
}
