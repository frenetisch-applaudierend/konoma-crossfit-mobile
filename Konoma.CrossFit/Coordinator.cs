using System;
using System.Threading.Tasks;
using Konoma.CrossFit.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Konoma.CrossFit
{
    public abstract class Coordinator<TStartup, TMainNavigation>
        where TStartup : IStartup<TMainNavigation>
    {
        internal async Task InitializeAsync(Func<IServiceRegistration, Task> registerInternalServices)
        {
            var services = new ServiceCollection();
            var registration = new ServiceRegistration(services);

            await registerInternalServices(registration);
            await RegisterServicesAsync(registration);

            ServiceProvider = services.BuildServiceProvider();
        }

        internal async Task StartApplicationAsync(TMainNavigation mainNavigation)
        {
            var startup = ServiceProviderExtensions.GetRequiredService<TStartup>(ServiceProvider);
            await startup.StartApplicationAsync(mainNavigation);
        }

        public IServiceProvider ServiceProvider { get; private set; } = default!;

        protected virtual Task RegisterServicesAsync(IServiceRegistration services)
        {
            RegisterServices(services);
            return Task.CompletedTask;
        }

        protected virtual void RegisterServices(IServiceRegistration services) { }
    }
}
