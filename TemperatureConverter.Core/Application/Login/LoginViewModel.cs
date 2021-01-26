using Konoma.CrossFit;

namespace TemperatureConverter.Core.Application.Login
{
    public class LoginViewModel : ViewModel
    {
        private string _username = "";

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        private string _password = "";

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
    }
}
