using Android.OS;
using Android.Views;
using AndroidX.Fragment.App;

namespace TemperatureConverter.Android.Application.Converter
{
    public class ConverterFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_converter, container, false);
        }
    }
}
