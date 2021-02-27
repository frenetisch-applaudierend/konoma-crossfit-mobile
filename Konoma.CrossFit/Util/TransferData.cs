using System.Collections.Generic;

namespace Konoma.CrossFit
{
    public class TransferData
    {
        #region Data

        private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

        #endregion

        #region Reading and Writing Data

        public string? GetString(string key) => GetObjectValue<string>(key);

        public void SetString(string key, string value) => SetValue(key, value);

        public bool? GetBool(string key) => GetStructValue<bool>(key);

        public void SetBool(string key, bool value) => SetValue(key, value);

        public void SetValue(string key, object? value)
        {
            if (value is null)
                _data.Remove(key);
            else
                _data[key] = value;
        }

        private T? GetObjectValue<T>(string key) where T : class =>
            _data.TryGetValue(key, out var value) ? value as T : null;

        private T? GetStructValue<T>(string key) where T : struct
        {
            if (!_data.TryGetValue(key, out var value))
                return null;

            if (value is T typedValue)
                return typedValue;

            return null;
        }

        #endregion
    }
}
