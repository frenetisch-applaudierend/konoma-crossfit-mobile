using Foundation;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Native.iOS.Services
{
    public class PreferencesService : IPreferencesService
    {
        private readonly NSUserDefaults _userDefaults = NSUserDefaults.StandardUserDefaults;

        public bool GetBool(string key, bool defaultValue)
        {
            var numberValue = _userDefaults.ValueForKey(new NSString(key)) as NSNumber;
            return numberValue?.BoolValue ?? defaultValue;
        }

        public void SetBool(string key, bool newValue)
        {
            _userDefaults.SetBool(newValue, key);
        }
    }
}
