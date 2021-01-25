using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit.Forms
{
    public abstract class FormsNavigation
    {
        public class Push<TScene> : INavigation<TScene> where TScene : Scene
        {
            public Push(Page currentPage, CrossFitContentPage<TScene> targetPage)
            {
                _currentPage = currentPage;
                _targetPage = targetPage;
            }

            private readonly Page _currentPage;
            private readonly CrossFitContentPage<TScene> _targetPage;

            public async Task NavigateAsync() => await _currentPage.Navigation.PushAsync(_targetPage);
        }
    }
}
