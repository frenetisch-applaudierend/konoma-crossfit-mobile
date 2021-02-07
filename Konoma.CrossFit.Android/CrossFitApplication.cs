using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;

namespace Konoma.CrossFit
{
    public class CrossFitApplication<TCoordinator> : Application
        where TCoordinator : Coordinator, new()
    {
        protected CrossFitApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public CrossFitApplication()
        {
        }

        internal static Task<TCoordinator> GetCoordinatorAsync() => CoordinatorRegistry.Task;

        private static readonly TaskCompletionSource<TCoordinator>
            CoordinatorRegistry = new TaskCompletionSource<TCoordinator>();

        public override void OnCreate()
        {
            base.OnCreate();

            SetupCoordinator();
        }

        private void SetupCoordinator()
        {
            var coordinator = new TCoordinator();
            SetupCoordinatorAsync(coordinator).FireAndForget();

            async Task SetupCoordinatorAsync(TCoordinator coordinator)
            {
                await coordinator.InitializeAsync(RegisterPlatformServicesAsyncInternal);
                CoordinatorRegistry.SetResult(coordinator);
            }
        }

        private async Task RegisterPlatformServicesAsyncInternal(IServiceRegistration services)
        {
            services.AddTransient<Context>(_ => Context.ApplicationContext!);

            await RegisterPlatformServicesAsync(services);
        }

        protected virtual Task RegisterPlatformServicesAsync(IServiceRegistration services)
        {
            RegisterPlatformServices(services);
            return Task.CompletedTask;
        }

        protected virtual void RegisterPlatformServices(IServiceRegistration services)
        {
        }
    }
}
