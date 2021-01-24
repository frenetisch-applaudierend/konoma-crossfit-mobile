using System;
using System.Threading.Tasks;
using Konoma.CrossFit.Util;

namespace Konoma.CrossFit.Forms
{
    public abstract class
        CrossFitFormsApplication<TApp, TStartup, TMainNavigation> : Xamarin.Forms.Application
        where TApp : CrossFitApplication<TStartup, TMainNavigation>, new()
        where TStartup : class, IStartup<TMainNavigation>
    {
        protected void StartApplication()
        {
            StartApplicationAsync().FireAndForget();
        }

        protected async Task StartApplicationAsync()
        {
            try
            {
                MainPage = CreateMainPage();

                var app = new TApp();
                await app.InitializeAsync(
                    async services =>
                    {
                        services.RegisterSingleton<TStartup>();
                        await RegisterServicesAsync(services);
                    });

                await app.StartApplicationAsync(CreateMainNavigation());
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync($"Exception on application startup: {ex}");
                throw;
            }
        }

        protected virtual Task RegisterServicesAsync(IServiceRegistration services)
        {
            RegisterServices(services);
            return Task.CompletedTask;
        }

        protected virtual void RegisterServices(IServiceRegistration services)
        {
        }

        protected virtual Xamarin.Forms.Page CreateMainPage() => new Xamarin.Forms.Page();

        protected abstract TMainNavigation CreateMainNavigation();
    }
}
