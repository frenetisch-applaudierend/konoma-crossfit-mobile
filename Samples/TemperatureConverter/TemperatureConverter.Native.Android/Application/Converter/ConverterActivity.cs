using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Konoma.CrossFit;
using TemperatureConverter.Android.Application.Login;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using SupportToolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace TemperatureConverter.Android.Application.Converter
{
    [Activity]
    public class ConverterActivity : CrossFitActivity<ConverterScene>
    {
        private const int LogoutMenuItem = 1;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_converter);
            SetSupportActionBar(this.FindViewById<SupportToolbar>(Resource.Id.toolbar));

            Title = Scene.ScreenTitle;

            RequireViewById<TextView>(Resource.Id.celsius_label).Text = Scene.CelsiusLabel;
            RequireViewById<TextView>(Resource.Id.fahrenheit_label).Text = Scene.FahrenheitLabel;
        }

        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            menu!.Add(0, LogoutMenuItem, 0, "Logout");
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void ConnectNavigationPoints()
        {
            Scene.ShowLogin.Connect(AndroidNavigation.StartTask<LoginScene, LoginActivity>(this));
        }

        protected override void ArrangeBindings(Binder<ConverterScene> binder)
        {
            binder.Bind(s => s.Celsius.Editable).ToEditText(this, Resource.Id.celsius_input);
            binder.Bind(s => s.Fahrenheit.Editable).ToEditText(this, Resource.Id.fahrenheit_input);

            binder.Bind(Scene.SignOutCommand).ToOptionsMenu(this, LogoutMenuItem);
        }
    }
}
