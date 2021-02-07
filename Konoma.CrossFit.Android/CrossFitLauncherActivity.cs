using System.Threading.Tasks;
using Android.App;
using Android.OS;

namespace Konoma.CrossFit
{
    public abstract class CrossFitLauncherActivity<TCoordinator> : Activity
        where TCoordinator : Coordinator, new()
    {
        protected sealed override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartApplicationAsync().FireAndForget();
        }

        public sealed override void OnCreate(Bundle? savedInstanceState, PersistableBundle? persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

            StartApplicationAsync().FireAndForget();
        }

        private async Task StartApplicationAsync()
        {
            var coordinator = await CrossFitApplication<TCoordinator>.GetCoordinatorAsync();

            ConnectNavigationPoints(coordinator);
            await coordinator.StartApplicationAsync();
        }

        protected abstract void ConnectNavigationPoints(TCoordinator coordinator);
    }
}
