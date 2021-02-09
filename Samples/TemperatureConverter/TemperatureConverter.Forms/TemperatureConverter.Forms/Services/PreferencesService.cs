using TemperatureConverter.Core.Services;
using Xamarin.Essentials;

namespace TemperatureConverter.Forms.Services
{
    public class PreferencesService : IPreferencesService
    {
        public bool GetBool(string key, bool defaultValue) => Preferences.Get(key, defaultValue ? 1 : 0) > 0;

        public void SetBool(string key, bool newValue) => Preferences.Set(key, newValue ? 1 : 0);
    }
}
