using Android.App;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application;

namespace TemperatureConverter.Android.Application
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LauncherActivity : CrossFitLauncherActivity<TemperatureConverterCoordinator>
    {
        protected override void ConnectNavigationPoints(TemperatureConverterCoordinator coordinator)
        {
            // coordinator.ShowLogin.Connect();
            // coordinator.ShowHome.Connect();
        }
    }
}
