using System;
using System.Threading.Tasks;
using UIKit;

namespace Konoma.CrossFit
{
    public static class Navigation
    {
        public class Root<TScene> : INavigation<TScene>
            where TScene : Scene
        {
            public Root(UIWindow window, Func<ICrossFitViewController<TScene>> targetPage)
            {
                _window = window;
                _targetPage = targetPage;
            }

            private readonly UIWindow _window;
            private readonly Func<ICrossFitViewController<TScene>> _targetPage;

            public Task NavigateAsync()
            {
                _window.RootViewController = _targetPage().AsViewController();
                return Task.CompletedTask;
            }
        }
    }

    public static class NavigationPointRegistrationExtensions
    {
        public static void RegisterRoot<TScene>(
            this NavigationPoint<TScene> navigationPoint,
            UIWindow window,
            Func<ICrossFitViewController<TScene>> targetPage)
            where TScene : Scene
        {
            navigationPoint.RegisterNavigation(new Navigation.Root<TScene>(window, targetPage));
        }
    }
}
