using System.Threading.Tasks;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Core.Application
{
    public class TemperatureConverterCoordinator : Coordinator
    {
        protected override Task RegisterServicesAsync(IServiceRegistration services)
        {
            services.AddSingleton<LoginService>();
            services.AddSingleton<TemperatureConverterService>();

            return Task.CompletedTask;
        }

        public override async Task StartApplicationAsync()
        {
            var loginService = ServiceProvider.GetRequiredService<LoginService>();

            var isLoggedIn = await loginService.CheckLoggedInAsync();
            var iniitalNavigation = isLoggedIn
                ? (INavigationPoint)ShowHome
                : ShowLogin;

            await iniitalNavigation.NavigateAsync();
        }

        public NavigationPoint<LoginScene> ShowLogin { get; } = new NavigationPoint<LoginScene>();

        public NavigationPoint<ConverterScene> ShowHome { get; } = new NavigationPoint<ConverterScene>();
    }
}
