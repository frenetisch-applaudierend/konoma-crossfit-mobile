using System;
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

        public async Task<bool> CheckLoggedInAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            return _preferences.GetBool("LoggedIn", defaultValue: false);
        }
    }
}
