using System.Linq;
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
            if (_currentPage.Navigation.NavigationStack.Any())
                await _currentPage.Navigation.PopAsync(animated);
            else if (_currentPage.Navigation.ModalStack.Any())
                await _currentPage.Navigation.PopModalAsync(animated);
        }
    }

    public partial class FormsNavigation
    {
        public static BackNavigation Back(Page currentPage)
            => new BackNavigation(currentPage);
    }
}
