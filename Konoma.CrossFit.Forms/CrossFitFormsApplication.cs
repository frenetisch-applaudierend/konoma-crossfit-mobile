using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit.Forms
{
    public abstract class
        CrossFitFormsApplication<TCoordinator, TStartup, TMainNavigation> : Xamarin.Forms.Application
        where TCoordinator : Coordinator<TStartup, TMainNavigation>, new()
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

                var coordinator = new TCoordinator();
                await coordinator.InitializeAsync(
                    async services =>
                    {
                        services.AddSingleton<TStartup>();
                        await RegisterServicesAsync(services);
                    });

                await coordinator.StartApplicationAsync(CreateMainNavigation());
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
