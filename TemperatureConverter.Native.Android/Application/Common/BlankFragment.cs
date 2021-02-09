using Android.OS;
using Android.Views;
using AndroidX.Fragment.App;

namespace TemperatureConverter.Android.Application.Common
{
    public class BlankFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
            inflater.Inflate(Resource.Layout.fragment_blank, container, false);
    }
}
