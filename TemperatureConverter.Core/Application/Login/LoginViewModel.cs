using Konoma.CrossFit;
// using Konoma.CrossFit.Generators.Bindable;

namespace TemperatureConverter.Core.Application.Login
{
    public partial class LoginViewModel : ViewModel
    {
        /*[Bindable]*/
        private string _username = "";

        /*[Bindable]*/
        private string _password = "";
    }

    public partial class LoginViewModel
    {
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
    }
}
