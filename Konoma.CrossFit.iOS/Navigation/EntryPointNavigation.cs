using System;
using System.Threading.Tasks;
using UIKit;

namespace Konoma.CrossFit
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IEntryPointNavigation<TScene, TNavigation> : INavigation<TScene>
        where TScene : Scene
        where TNavigation : IEntryPointNavigation<TScene, TNavigation>
    {
        TNavigation ContainedIn(Func<UIViewController, UIViewController> container);
    }

    public abstract class EntryPointNavigation<TScene, TNavigation> : IEntryPointNavigation<TScene, TNavigation>
        where TScene : Scene
        where TNavigation : EntryPointNavigation<TScene, TNavigation>
    {
        protected EntryPointNavigation(Func<ICrossFitViewController<TScene>> targetController)
        {
            _targetController = targetController;
        }

        private readonly Func<ICrossFitViewController<TScene>> _targetController;
        private Func<UIViewController, UIViewController>? _container;

        public TNavigation ContainedIn(Func<UIViewController, UIViewController> container)
        {
            _container = container;
            return (TNavigation)this;
        }

        protected UIViewController InstantiateController()
        {
            var target = _targetController().AsViewController();
            var controller = _container is { } wrapper ? wrapper(target) : target;
            return controller;
        }

        public abstract Task NavigateAsync(bool animated);
    }

    public static class EntryPointNavigationExtensions
    {
        public static TNavigation InNavigationController<TScene, TNavigation>(
            this IEntryPointNavigation<TScene, TNavigation> navigation)
            where TScene : Scene
            where TNavigation : IEntryPointNavigation<TScene, TNavigation>
        {
            return navigation.ContainedIn(target => new UINavigationController(target));
        }
    }
}
