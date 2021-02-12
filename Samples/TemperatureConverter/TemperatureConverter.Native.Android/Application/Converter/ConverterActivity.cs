using Android.App;
using Android.OS;
using AndroidX.AppCompat.Widget;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Converter;

namespace TemperatureConverter.Android.Application.Converter
{
    [Activity]
    public class ConverterActivity : CrossFitActivity<ConverterScene>
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_converter);
            SetSupportActionBar(this.FindViewById<Toolbar>(Resource.Id.toolbar));
        }

        protected override void ConnectNavigationPoints()
        {
            // TODO: Add me
        }

        protected override void ArrangeBindings(Binder<ConverterScene> binder)
        {
            // TODO: Add me
        }
    }
}
