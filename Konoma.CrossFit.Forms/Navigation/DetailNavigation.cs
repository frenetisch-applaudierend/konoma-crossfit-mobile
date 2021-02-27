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

    public class DetailNavigation<TScene, TArgs> : INavigation<TScene, TArgs>
        where TScene : Scene<TArgs>
        where TArgs : TransferData
    {
        public DetailNavigation(
            Page currentPage,
            Func<TArgs, ICrossFitPage<TScene>> targetPage)
        {
            _currentPage = currentPage;
            _targetPage = targetPage;
        }

        private readonly Page _currentPage;
        private readonly Func<TArgs, ICrossFitPage<TScene>> _targetPage;

        public async Task NavigateAsync(TArgs args, bool animated) =>
            await _currentPage.Navigation.PushAsync(_targetPage(args).AsFormsPage());
    }

    public partial class FormsNavigation
    {
        public static DetailNavigation<TScene> PushDetail<TScene>(
            Page currentPage,
            Func<ICrossFitPage<TScene>> targetPage) where TScene : Scene
            => new DetailNavigation<TScene>(currentPage, targetPage);

        public static DetailNavigation<TScene, TArgs> PushDetail<TScene, TArgs>(
            Page currentPage,
            Func<TArgs, ICrossFitPage<TScene>> targetPage)
            where TScene : Scene<TArgs>
            where TArgs : TransferData
            => new DetailNavigation<TScene, TArgs>(currentPage, targetPage);
    }
}
