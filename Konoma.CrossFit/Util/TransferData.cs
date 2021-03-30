using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Konoma.CrossFit
{
    public class TransferData
    {
        private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

        protected T? GetObjectValue<T>([CallerMemberName] string key = default!) where T : class =>
            _data.TryGetValue(key, out var value) ? value as T : null;

        protected T? GetStructValue<T>([CallerMemberName] string key = default!) where T : struct
        {
            if (!_data.TryGetValue(key, out var value))
                return null;

            if (value is T typedValue)
                return typedValue;

            return null;
        }

        protected T GetRequiredObjectValue<T>([CallerMemberName] string key = default!) where T : class =>
            GetObjectValue<T>(key)
            ?? throw new InvalidOperationException($"Property {key} was required, but not found");

        protected T GetRequiredStructValue<T>([CallerMemberName] string key = default!) where T : struct =>
            GetStructValue<T>(key)
            ?? throw new InvalidOperationException($"Property {key} was required, but not found");

        protected void SetValue(object? value, [CallerMemberName] string key = default!)
        {
            if (value is null)
                _data.Remove(key);
            else
                _data[key] = value;
        }
    }
}
