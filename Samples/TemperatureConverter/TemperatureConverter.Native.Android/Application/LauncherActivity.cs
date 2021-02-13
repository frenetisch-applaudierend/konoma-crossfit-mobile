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
}
