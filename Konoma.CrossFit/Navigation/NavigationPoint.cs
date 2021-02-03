using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public class NavigationPoint<TScene>
        where TScene : Scene
    {
        private INavigation<TScene>? _navigation;

        public void Navigate()
        {
            EnsureNavigation();

            _navigation!.NavigateAsync().FireAndForget();
        }

        public async Task NavigateAsync()
        {
            EnsureNavigation();

            await _navigation!.NavigateAsync();
        }

        public void RegisterNavigation(INavigation<TScene> navigation)
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
