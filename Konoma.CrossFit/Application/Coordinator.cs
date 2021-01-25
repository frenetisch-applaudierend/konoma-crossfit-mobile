using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Konoma.CrossFit
{
    public abstract class Coordinator<TStartup, TMainNavigation>
        where TStartup : IStartup<TMainNavigation>
    {
        internal async Task InitializeAsync(Func<IServiceRegistration, Task> registerInternalServices)
        {
            var services = new ServiceCollection();
            var serviceRegistration = new ServiceRegistration(services);
            var sceneRegistration = new SceneRegistration(services);

            await registerInternalServices(serviceRegistration);
            await RegisterServicesAsync(serviceRegistration);

            await RegisterScenesAsync(sceneRegistration);

            ServiceProvider = services.BuildServiceProvider();
            Scene.ServiceProvider = ServiceProvider;
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

        protected virtual Task RegisterScenesAsync(ISceneRegistration scenes)
        {
            RegisterScenes(scenes);
            return Task.CompletedTask;
        }

        protected virtual void RegisterScenes(ISceneRegistration scenes)
        {
            RegisterScenesFromAssemly(scenes, GetType().Assembly);
        }

        protected void RegisterScenesFromAssemly(ISceneRegistration scenes, Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (Scene.IsSceneType(type))
                    scenes.RegisterScene(type);
            }
        }
    }
}
