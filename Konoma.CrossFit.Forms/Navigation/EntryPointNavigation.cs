using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IEntryPointNavigation<TScene, TNavigation> : INavigation<TScene>
        where TScene : Scene
        where TNavigation : IEntryPointNavigation<TScene, TNavigation>
    {
        TNavigation ContainedIn(Func<Page, Page> container);
    }

    public abstract class EntryPointNavigation<TScene, TNavigation> : IEntryPointNavigation<TScene, TNavigation>
        where TScene : Scene
        where TNavigation : EntryPointNavigation<TScene, TNavigation>
    {
        protected EntryPointNavigation(Func<ICrossFitPage<TScene>> targetPage)
        {
            _targetPage = targetPage;
        }

        private readonly Func<ICrossFitPage<TScene>> _targetPage;
        private Func<Page, Page>? _container;

        public TNavigation ContainedIn(Func<Page, Page> container)
        {
            _container = container;
            return (TNavigation)this;
        }

        protected Page InstantiatePage()
        {
            var target = _targetPage().AsFormsPage();
            var controller = _container is { } wrapper ? wrapper(target) : target;
            return controller;
        }

        public abstract Task NavigateAsync(bool animated);
    }

    public static class EntryPointNavigationExtensions
    {
        public static TNavigation InNavigationPage<TScene, TNavigation>(
            this IEntryPointNavigation<TScene, TNavigation> navigation)
            where TScene : Scene
            where TNavigation : IEntryPointNavigation<TScene, TNavigation>
        {
            return navigation.ContainedIn(target => new NavigationPage(target));
        }
    }
}
