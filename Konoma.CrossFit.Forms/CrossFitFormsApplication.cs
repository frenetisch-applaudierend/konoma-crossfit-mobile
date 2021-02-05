using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit.Forms
{
    public abstract class CrossFitFormsApplication<TCoordinator> : Xamarin.Forms.Application
        where TCoordinator : Coordinator, new()
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
                await coordinator.InitializeAsync(RegisterPlattformServicesAsync);
                RegisterNavigationPoints(coordinator);
                await coordinator.StartApplicationAsync();
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync($"Exception on application startup: {ex}");
                throw;
            }
        }

        protected virtual Task RegisterPlattformServicesAsync(IServiceRegistration services)
        {
            RegisterPlattformServices(services);
            return Task.CompletedTask;
        }

        protected virtual void RegisterPlattformServices(IServiceRegistration services)
        {
        }

        protected abstract void RegisterNavigationPoints(TCoordinator coordinator);

        protected virtual Xamarin.Forms.Page CreateMainPage() => new Xamarin.Forms.Page();
    }
}
