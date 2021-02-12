using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Konoma.CrossFit;
using TemperatureConverter.Android.Application.Converter;
using TemperatureConverter.Android.Application.Login;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;

namespace TemperatureConverter.Android.Application
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LauncherActivity : CrossFitLauncherActivity<TemperatureConverterCoordinator>
    {
        protected override void ConnectNavigationPoints(TemperatureConverterCoordinator coordinator)
        {
            coordinator.ShowLogin.Connect(new StartTaskNavigation<LoginScene, LoginActivity>(this));
            coordinator.ShowHome.Connect(new StartTaskNavigation<ConverterScene, ConverterActivity>(this));
        }
    }

    public class StartTaskNavigation<TScene, TActivity> : INavigation<TScene>
        where TScene : Scene
        where TActivity : Activity
    {
        public StartTaskNavigation(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public Task NavigateAsync(bool animated)
        {
            var intent = new Intent(_context, typeof(TActivity));
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            _context.StartActivity(intent);

            return Task.CompletedTask;
        }
    }
}
