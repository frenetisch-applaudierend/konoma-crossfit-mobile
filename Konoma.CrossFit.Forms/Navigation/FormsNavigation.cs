using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public static partial class FormsNavigation
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

            public async Task NavigateAsync(bool animated) => await _currentPage.Navigation.PushAsync(_targetPage());
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

            public async Task NavigateAsync(bool animated)
            {
                _currentPage.Navigation.InsertPageBefore(_targetPage(), _currentPage);
                await _currentPage.Navigation.PopAsync();
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
            navigationPoint.Connect(new FormsNavigation.Push<TScene>(currentPage, targetPage));
        }

        public static void RegisterReplace<TScene>(
            this NavigationPoint<TScene> navigationPoint,
            Page currentPage,
            Func<CrossFitContentPage<TScene>> targetPage)
            where TScene : Scene
        {
            navigationPoint.Connect(new FormsNavigation.Replace<TScene>(currentPage, targetPage));
        }
    }
}
