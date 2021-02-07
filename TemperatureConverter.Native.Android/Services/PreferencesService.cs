using Android.Content;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Android.Services
{
    public class PreferencesService : IPreferencesService
    {
        private readonly ISharedPreferences _sharedPreferences;

        public PreferencesService(Context context)
        {
            _sharedPreferences = context.GetSharedPreferences("TemperatureConverter", FileCreationMode.Private);
        }

        public bool GetBool(string key, bool defaultValue) =>
            _sharedPreferences.GetBoolean(key, defaultValue);

        public void SetBool(string key, bool newValue) =>
            _sharedPreferences.Edit()!.PutBoolean(key, newValue)!.Commit();
    }
}
