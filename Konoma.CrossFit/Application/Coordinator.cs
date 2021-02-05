using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Konoma.CrossFit
{
    public abstract class Coordinator
    {
        public IServiceProvider ServiceProvider { get; private set; } = default!;

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

        protected virtual async Task RegisterScenesAsync(ISceneRegistration scenes)
        {
            await RegisterScenesFromAssemlyAsync(scenes, GetType().Assembly);
        }

        protected async Task RegisterScenesFromAssemlyAsync(ISceneRegistration scenes, Assembly assembly)
        {
            await Task.Run(
                () =>
                {
                    foreach (var type in assembly.DefinedTypes)
                    {
                        if (Scene.IsSceneType(type))
                            scenes.RegisterScene(type);
                    }
                });
        }

        protected abstract Task RegisterServicesAsync(IServiceRegistration services);

        public abstract Task StartApplicationAsync();
    }
}
