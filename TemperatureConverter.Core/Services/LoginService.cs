using System;
using System.Threading;
using System.Threading.Tasks;

namespace TemperatureConverter.Core.Services
{
    public class LoginService
    {
        public LoginService(IPreferencesService preferences)
        {
            _preferences = preferences;
        }

        private readonly IPreferencesService _preferences;

        private const string LoggedInKey = "LoggedIn";

        public async Task<bool> CheckLoggedInAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            return _preferences.GetBool(LoggedInKey, defaultValue: false);
        }

        public void LogIn()
        {
            _preferences.SetBool(LoggedInKey, true);
        }

        public void LogOut()
        {
            _preferences.SetBool(LoggedInKey, false);
        }
    }
}
