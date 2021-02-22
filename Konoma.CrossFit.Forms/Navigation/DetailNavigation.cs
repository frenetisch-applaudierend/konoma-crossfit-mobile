using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public class DetailNavigation<TScene> : INavigation<TScene> where TScene : Scene
    {
        public DetailNavigation(
            Page currentPage,
            Func<CrossFitContentPage<TScene>> targetPage)
        {
            _currentPage = currentPage;
            _targetPage = targetPage;
        }

        private readonly Page _currentPage;
        private readonly Func<CrossFitContentPage<TScene>> _targetPage;

        public async Task NavigateAsync(bool animated) => await _currentPage.Navigation.PushAsync(_targetPage());
    }

    public partial class FormsNavigation
    {
        public static DetailNavigation<TScene> PushDetail<TScene>(
            Page currentPage,
            Func<CrossFitContentPage<TScene>> targetPage) where TScene : Scene
            => new DetailNavigation<TScene>(currentPage, targetPage);
    }
}
