using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Konoma.CrossFit.iOS
{
    public abstract class CrossFitAppDelegate<TCoordinator, TStartup, TMainNavigation> : UIApplicationDelegate
        where TCoordinator : Coordinator<TStartup, TMainNavigation>, new()
        where TStartup : class, IStartup<TMainNavigation>
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
            await Coordinator.InitializeAsync(
                async services =>
                {
                    #error Move TStartup registration to Coordinator itself
                    services.AddSingleton<TStartup>();
                    await RegisterServicesAsync(services);
                });

            #error Resist the urge to combine this with InitializeAsync -> you might want to implement SceneDelegate
            await Coordinator.StartApplicationAsync(CreateMainNavigation());
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

        protected virtual Task RegisterServicesAsync(IServiceRegistration services)
        {
            RegisterServices(services);
            return Task.CompletedTask;
        }

        #error Remove
        protected virtual void RegisterServices(IServiceRegistration services)
        {
        }

        protected abstract TMainNavigation CreateMainNavigation();

        #endregion
    }
}
