using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public interface INavigationPoint
    {
        Task NavigateAsync(bool animated = true);
    }

    public class NavigationPoint<TScene> : INavigationPoint
        where TScene : Scene
    {
        private INavigation<TScene>? _navigation;

        public void Navigate(bool animated = true)
        {
            EnsureNavigation();

            _navigation!.NavigateAsync(animated).FireAndForget();
        }

        public async Task NavigateAsync(bool animated = true)
        {
            EnsureNavigation();

            await _navigation!.NavigateAsync(animated);
        }

        public void Connect(INavigation<TScene> navigation)
        {
            _navigation = navigation;
        }

        private void EnsureNavigation()
        {
            if (_navigation is null)
                throw new InvalidOperationException("No navigation object was registered for this navigation point");
        }
    }
}
