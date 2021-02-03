using System;

namespace Konoma.CrossFit
{
    public class Property<T>
    {
        public Property(T initialValue, Action onChanging, Action onChanged)
        {
            _value = initialValue;
            _onChanging = onChanging;
            _onChanged = onChanged;
        }

        private readonly Action _onChanging;
        private readonly Action _onChanged;

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

            if (notify)
                _onChanging();

            _value = value;

            if (notify)
                _onChanged();
        }

        public static implicit operator T(Property<T> property) => property._value;
    }
}
