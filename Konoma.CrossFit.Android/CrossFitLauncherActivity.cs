using System.Threading.Tasks;
using Android.OS;
using AndroidX.AppCompat.App;

namespace Konoma.CrossFit
{
    public abstract class CrossFitLauncherActivity<TCoordinator> : AppCompatActivity
        where TCoordinator : Coordinator, new()
    {
        protected sealed override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LoadContentView();
            StartApplicationAsync().FireAndForget();
        }

        public sealed override void OnCreate(Bundle? savedInstanceState, PersistableBundle? persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

            LoadContentView();
            StartApplicationAsync().FireAndForget();
        }

        protected virtual void LoadContentView() { }

        private async Task StartApplicationAsync()
        {
            var coordinator = await CrossFitApplication<TCoordinator>.GetCoordinatorAsync();

            ConnectNavigationPoints(coordinator);
            await coordinator.StartApplicationAsync();
        }

        protected abstract void ConnectNavigationPoints(TCoordinator coordinator);
    }
}
