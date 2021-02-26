using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public class DetailNavigation<TScene> : INavigation<TScene> where TScene : Scene
    {
        public DetailNavigation(
            Page currentPage,
            Func<ICrossFitPage<TScene>> targetPage)
        {
            _currentPage = currentPage;
            _targetPage = targetPage;
        }

        private readonly Page _currentPage;
        private readonly Func<ICrossFitPage<TScene>> _targetPage;

        public async Task NavigateAsync(bool animated) =>
            await _currentPage.Navigation.PushAsync(_targetPage().AsFormsPage());
    }

    public partial class FormsNavigation
    {
        public static DetailNavigation<TScene> PushDetail<TScene>(
            Page currentPage,
            Func<ICrossFitPage<TScene>> targetPage) where TScene : Scene
            => new DetailNavigation<TScene>(currentPage, targetPage);
    }
}
