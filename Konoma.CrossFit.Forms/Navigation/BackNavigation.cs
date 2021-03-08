using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public class BackNavigation : INavigation
    {
        public BackNavigation(Page currentPage)
        {
            _currentPage = currentPage;
        }

        private readonly Page _currentPage;

        public async Task NavigateAsync(bool animated)
        {
            await _currentPage.Navigation.PopAsync(animated);
        }
    }

    public partial class FormsNavigation
    {
        public static BackNavigation Back(Page currentPage)
            => new BackNavigation(currentPage);
    }
}
