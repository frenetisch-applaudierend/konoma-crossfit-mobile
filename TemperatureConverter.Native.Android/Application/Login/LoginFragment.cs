using Android.OS;
using Android.Views;
using AndroidX.Fragment.App;

namespace TemperatureConverter.Android.Application.Login
{
    public class LoginFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_login, container, false);
        }
    }
}
