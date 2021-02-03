using System;
using System.Threading.Tasks;
using Konoma.CrossFit;
using Konoma.CrossFit.iOS;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Services;
using TemperatureConverter.Native.iOS.Services;
using UIKit;

namespace TemperatureConverter.Native.iOS.Application
{
    public class AppDelegate : CrossFitAppDelegate<TemperatureConverterCoordinator,
        TemperatureConverterCoordinator.Startup, TemperatureConverterCoordinator.IMainNavigation>
    {
        protected override TemperatureConverterCoordinator.IMainNavigation CreateMainNavigation() =>
            new MainNavigation(Window!);

        protected override void RegisterServices(IServiceRegistration services)
        {
            services.AddSingleton<IPreferencesService, PreferencesService>();
        }
    }

    public class MainNavigation : TemperatureConverterCoordinator.IMainNavigation
    {
        public MainNavigation(UIWindow window)
        {
            ShowLogin = new RootNavigation<LoginScene>(
                window,
                () =>
                {
                    var controller = new UIViewController();
                    controller.View!.BackgroundColor = UIColor.SystemIndigoColor;
                    return controller;
                });

            ShowHome = new RootNavigation<ConverterScene>(
                window,
                () =>
                {
                    var controller = new UIViewController();
                    controller.View!.BackgroundColor = UIColor.SystemTealColor;
                    return controller;
                });
        }

        public INavigation<LoginScene> ShowLogin { get; }
        public INavigation<ConverterScene> ShowHome { get; }

        class RootNavigation<TScene> : INavigation<TScene>
            where TScene : Scene
        {
            public RootNavigation(UIWindow window, Func<UIViewController> viewController)
            {
                _window = window;
                _viewController = viewController;
            }

            private readonly UIWindow _window;
            private readonly Func<UIViewController> _viewController;

            public Task NavigateAsync()
            {
                var controller = _viewController();
                _window.RootViewController = controller;

                return Task.CompletedTask;
            }
        }
    }
}
