using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public class NavigationPoint
    {
        private INavigation? _navigation;

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

        public void Connect(INavigation navigation)
        {
            _navigation = navigation;
        }

        private void EnsureNavigation()
        {
            if (_navigation is null)
                throw new InvalidOperationException("No navigation object was registered for this navigation point");
        }
    }

    public class NavigationPoint<TScene>
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

    public class NavigationPoint<TScene, TArgs>
        where TScene : Scene<TArgs>
        where TArgs : TransferData
    {
        private INavigation<TScene, TArgs>? _navigation;

        public void Navigate(TArgs args, bool animated = true)
        {
            EnsureNavigation().NavigateAsync(args, animated).FireAndForget();
        }

        public async Task NavigateAsync(TArgs args, bool animated = true)
        {
            await EnsureNavigation().NavigateAsync(args, animated);
        }

        public void Connect(INavigation<TScene, TArgs> navigation)
        {
            _navigation = navigation;
        }

        private INavigation<TScene, TArgs> EnsureNavigation()
        {
            if (_navigation is null)
                throw new InvalidOperationException("No navigation object was registered for this navigation point");

            return _navigation;
        }
    }
}
