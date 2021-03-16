using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public abstract class CrossFitFormsApplication<TCoordinator> : Xamarin.Forms.Application
        where TCoordinator : Coordinator, new()
    {
        // ReSharper disable once NotAccessedField.Local
        private TCoordinator _coordinator = null!;

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

                _coordinator = coordinator;
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
