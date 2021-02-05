using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Konoma.CrossFit.iOS
{
    public abstract class CrossFitAppDelegate<TCoordinator> : UIApplicationDelegate
        where TCoordinator : Coordinator, new()
    {
        public override UIWindow? Window { get; set; }

        protected TCoordinator Coordinator { get; } = new TCoordinator();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds) {RootViewController = CreateLaunchController()};
            Window.MakeKeyAndVisible();

            StartApplicationAsync().FireAndForget();

            return true;
        }

        private async Task StartApplicationAsync()
        {
            await Coordinator.InitializeAsync(RegisterPlatformServicesAsync);
            RegisterNavigationPoints(Coordinator);
            await Coordinator.StartApplicationAsync();
        }

        #region Extension Points

        protected virtual UIViewController CreateLaunchController()
        {
            UIViewController? launchController =
                UIStoryboard.FromName("LaunchScreen", null).InstantiateInitialViewController();
            return launchController ?? DefaultController();

            static UIViewController DefaultController()
            {
                var controller = new UIViewController();
                controller.View!.BackgroundColor = UIColor.SystemBackgroundColor;
                return controller;
            }
        }

        protected virtual Task RegisterPlatformServicesAsync(IServiceRegistration services)
        {
            RegisterPlatformServices(services);
            return Task.CompletedTask;
        }

        protected virtual void RegisterPlatformServices(IServiceRegistration services)
        {
        }

        protected abstract void RegisterNavigationPoints(TCoordinator coordinator);

        #endregion
    }
}
