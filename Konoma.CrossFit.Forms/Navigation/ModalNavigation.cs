using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public class ModalNavigation<TScene> : EntryPointNavigation<TScene, ModalNavigation<TScene>>
        where TScene : Scene
    {
        public ModalNavigation(Page currentPage, Func<CrossFitContentPage<TScene>> targetPage) : base(targetPage)
        {
            _currentPage = currentPage;
        }

        private readonly Page _currentPage;

        public override async Task NavigateAsync(bool animated)
        {
            await _currentPage.Navigation.PushModalAsync(InstantiatePage());
        }
    }

    public partial class FormsNavigation
    {
        public static ModalNavigation<TScene> PushModal<TScene>(
            Page currentPage,
            Func<CrossFitContentPage<TScene>> targetPage) where TScene : Scene
            => new ModalNavigation<TScene>(currentPage, targetPage);
    }
}
