using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using AndroidX.AppCompat.Widget;
using AndroidX.Navigation;
using AndroidX.Navigation.UI;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;

namespace TemperatureConverter.Android.Application
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LauncherActivity : CrossFitLauncherActivity<TemperatureConverterCoordinator>
    {
        protected override void LoadContentView()
        {
            SetContentView(Resource.Layout.layout_launcher);
        }

        protected override void ConnectNavigationPoints(TemperatureConverterCoordinator coordinator)
        {
            var navController = Navigation.FindNavController(this, Resource.Id.nav_host_fragment);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            var appBarConfiguration = new AppBarConfiguration.Builder(navController.Graph).Build();

            NavigationUI.SetupWithNavController(toolbar, navController, appBarConfiguration);

            coordinator.ShowLogin.Connect(
                new DelegateNavigation<LoginScene>(() => navController.Navigate(Resource.Id.start_login_action)));
            coordinator.ShowHome.Connect(
                new DelegateNavigation<ConverterScene>(
                    () => Navigation.FindNavController(this, Resource.Id.nav_host_fragment)
                        .Navigate(Resource.Id.start_converter_action)));
        }
    }

    public class DelegateNavigation<TScene> : INavigation<TScene>
        where TScene : Scene
    {
        private readonly Func<Task> _navigateAction;

        public DelegateNavigation(Func<Task> navigateAction)
        {
            _navigateAction = navigateAction;
        }

        public DelegateNavigation(Action navigateAction)
        {
            _navigateAction = delegate
            {
                navigateAction();
                return Task.CompletedTask;
            };
        }

        public Task NavigateAsync(bool animated) => _navigateAction();
    }
}
