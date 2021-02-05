using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit.Forms
{
    public static class Navigation
    {
        public class Push<TScene> : INavigation<TScene> where TScene : Scene
        {
            public Push(Page currentPage, Func<CrossFitContentPage<TScene>> targetPage)
            {
                _currentPage = currentPage;
                _targetPage = targetPage;
            }

            private readonly Page _currentPage;
            private readonly Func<CrossFitContentPage<TScene>> _targetPage;

            public async Task NavigateAsync() => await _currentPage.Navigation.PushAsync(_targetPage());
        }

        public class Replace<TScene> : INavigation<TScene> where TScene : Scene
        {
            public Replace(Page currentPage, Func<CrossFitContentPage<TScene>> targetPage)
            {
                _currentPage = currentPage;
                _targetPage = targetPage;
            }

            private readonly Page _currentPage;
            private readonly Func<CrossFitContentPage<TScene>> _targetPage;

            public async Task NavigateAsync()
            {
                _currentPage.Navigation.InsertPageBefore(_targetPage(), _currentPage);
                await _currentPage.Navigation.PopAsync();
            }
        }

        public class MainPage<TScene> : INavigation<TScene> where TScene : Scene
        {
            public MainPage(Application application, Func<CrossFitContentPage<TScene>> targetPage)
            {
                _application = application;
                _targetPage = targetPage;
            }

            private readonly Application _application;
            private readonly Func<CrossFitContentPage<TScene>> _targetPage;

            public Task NavigateAsync()
            {
                _application.MainPage = _targetPage();
                return Task.CompletedTask;
            }
        }
    }

    public static class NavigationPointRegistrationExtensions
    {
        public static void RegisterPush<TScene>(
            this NavigationPoint<TScene> navigationPoint,
            Page currentPage,
            Func<CrossFitContentPage<TScene>> targetPage)
            where TScene : Scene
        {
            navigationPoint.RegisterNavigation(new Navigation.Push<TScene>(currentPage, targetPage));
        }

        public static void RegisterReplace<TScene>(
            this NavigationPoint<TScene> navigationPoint,
            Page currentPage,
            Func<CrossFitContentPage<TScene>> targetPage)
            where TScene : Scene
        {
            navigationPoint.RegisterNavigation(new Navigation.Replace<TScene>(currentPage, targetPage));
        }

        public static void RegisterMainPage<TScene>(
            this NavigationPoint<TScene> navigationPoint,
            Application application,
            Func<CrossFitContentPage<TScene>> targetPage)
            where TScene : Scene
        {
            navigationPoint.RegisterNavigation(new Navigation.MainPage<TScene>(application, targetPage));
        }
    }
}
