using System;

namespace Konoma.CrossFit
{
    public class Property<T>
    {
        public Property(T initialValue, Action<T, T> onChanging, Action<T, T> onChanged)
        {
            Value = initialValue;
            _onChanging = onChanging;
            _onChanged = onChanged;
        }

        private readonly Action<T, T> _onChanging;
        private readonly Action<T, T> _onChanged;

        public T Value { get; private set; }

        public T Editable
        {
            get => Value;
            set => Set(value);
        }

        public T Get() => Value;

        public void Set(T value, bool notify = true)
        {
            if (Equals(Value, value))
                return;

            var previousValue = Value;
            if (notify)
                _onChanging(previousValue, value);

            Value = value;

            if (notify)
                _onChanged(previousValue, value);
        }

        public static implicit operator T(Property<T> property) => property.Value;
    }
}
