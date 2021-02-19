using System;

namespace Konoma.CrossFit
{
    public class Property<T>
    {
        public Property(T initialValue, Action<T, T> onChanging, Action<T, T> onChanged)
        {
            _value = initialValue;
            _onChanging = onChanging;
            _onChanged = onChanged;
        }

        private readonly Action<T, T> _onChanging;
        private readonly Action<T, T> _onChanged;

        private T _value;

        public T Editable
        {
            get => _value;
            set => Set(value);
        }

        public T Get() => _value;

        public void Set(T value, bool notify = true)
        {
            if (Equals(_value, value))
                return;

            var previousValue = _value;
            if (notify)
                _onChanging(previousValue, value);

            _value = value;

            if (notify)
                _onChanged(previousValue, value);
        }

        public static implicit operator T(Property<T> property) => property._value;
    }
}
