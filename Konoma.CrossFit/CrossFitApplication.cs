using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public abstract class CrossFitApplication<TStartup, TMainNavigation>
        where TStartup : IStartup<TMainNavigation>
    {
        internal async Task InitializeAsync(Action<IServiceRegistration> registerInternalServices)
        {
            var registration = new DummyServiceRegistration();

            registerInternalServices(registration);
            await RegisterServicesAsync(registration);
        }

        internal async Task StartApplicationAsync(TMainNavigation mainNavigation)
        {
            var startup = ServiceProvider.GetRequiredService<TStartup>();
            await startup.StartApplicationAsync(mainNavigation);
        }

        public IServiceProvider ServiceProvider { get; private set; } = default!;

        protected virtual Task RegisterServicesAsync(IServiceRegistration services) =>
            Task.CompletedTask;
    }
}
