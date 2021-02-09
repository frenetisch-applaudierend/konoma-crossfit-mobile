namespace TemperatureConverter.Core.Services
{
    public interface IPreferencesService
    {
        bool GetBool(string key, bool defaultValue);

        void SetBool(string key, bool newValue);
    }
}
