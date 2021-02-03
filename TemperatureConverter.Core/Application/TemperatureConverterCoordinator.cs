using System.Threading.Tasks;
using Konoma.CrossFit;
using Microsoft.Extensions.DependencyInjection;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Core.Application
{
    public class TemperatureConverterCoordinator : Coordinator<
        TemperatureConverterCoordinator.Startup,
        TemperatureConverterCoordinator.IMainNavigation>
    {
        public class Startup : IStartup<IMainNavigation>
        {
            public Startup(LoginService loginService)
            {
                _loginService = loginService;
            }

            private readonly LoginService _loginService;

            public async Task StartApplicationAsync(IMainNavigation navigation)
            {
                if (await _loginService.CheckLoggedInAsync())
                {
                    await navigation.ShowHome.NavigateAsync();
                }
                else
                {
                    await navigation.ShowLogin.NavigateAsync();
                }
            }
        }

        #error Remove IMainNavigation and replace with NavigationPoint properties in startup.

        public interface IMainNavigation
        {
            public INavigation<LoginScene> ShowLogin { get; }

            public INavigation<ConverterScene> ShowHome { get; }
        }

        protected override void RegisterServices(IServiceRegistration services)
        {
            services.AddSingleton<LoginService>();
            services.AddSingleton<TemperatureConverterService>();
        }
    }
}
