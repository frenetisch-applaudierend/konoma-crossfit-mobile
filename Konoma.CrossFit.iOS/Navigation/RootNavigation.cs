using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;

namespace Konoma.CrossFit
{
    public class RootNavigation<TScene> : EntryPointNavigation<TScene, RootNavigation<TScene>>
        where TScene : Scene
    {
        public RootNavigation(Func<UIWindow> window, Func<ICrossFitViewController<TScene>> targetPage)
            : base(targetPage)
        {
            _window = window;
        }

        private readonly Func<UIWindow> _window;

        public override Task NavigateAsync(bool animated)
        {
            _window().RootViewController = InstantiateController();
            return Task.CompletedTask;
        }
    }

    public static partial class Navigation
    {
        public static RootNavigation<TScene> Root<TScene>(
            UIWindow window,
            Func<ICrossFitViewController<TScene>> targetPage)
            where TScene : Scene
            => new RootNavigation<TScene>(() => window, targetPage);

        public static RootNavigation<TScene> Root<TScene, TTargetController>(UIViewController current)
            where TScene : Scene
            where TTargetController : UIViewController, ICrossFitViewController<TScene>, new()
            => new RootNavigation<TScene>(() => current.View!.Window, () => new TTargetController());
    }
}
