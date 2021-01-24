using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Forms.Services
{
    public class DummyPreferencesService : IPreferencesService
    {
        public bool GetBool(string key, bool defaultValue)
        {
            return defaultValue;
        }
    }
}
