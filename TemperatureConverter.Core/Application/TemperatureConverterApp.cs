using System.Threading.Tasks;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Core.Application
{
    public class TemperatureConverterApp : CrossFitApplication<
        TemperatureConverterApp.Startup,
        TemperatureConverterApp.IMainNavigation>
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
                    await navigation.ShowHome.GoAsync();
                }
                else
                {
                    await navigation.ShowLogin.GoAsync();
                }
            }
        }

        public interface IMainNavigation
        {
            public INavigation<LoginScene> ShowLogin { get; }

            public INavigation<ConverterScene> ShowHome { get; }
        }

        protected override void RegisterServices(IServiceRegistration services)
        {
            services.RegisterSingleton<LoginService>();
        }
    }
}
