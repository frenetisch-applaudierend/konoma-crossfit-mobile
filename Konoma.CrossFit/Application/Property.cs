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

        public T Value => _value;

        public T Editable
        {
            get => _value;
            set => SetValue(value);
        }

        public void SetValue(T value, bool notify = true)
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

    public class PropertyBuilder<T>
    {
        public PropertyBuilder(string name, T initialValue, Action onChanging, Action onChanged)
        {
            Name = name;

            _initialValue = initialValue;
            _onChanging = onChanging;
            _onChanged = onChanged;
        }

        public string Name { get; }

        private readonly T _initialValue;
        private readonly Action _onChanging;
        private readonly Action _onChanged;

        private Action? _customOnChanging;
        private Action? _customOnChanged;

        private Property<T>? _property;

        public PropertyBuilder<T> OnChanging(Action onChanging)
        {
            _customOnChanging = onChanging;
            return this;
        }

        public PropertyBuilder<T> OnChanged(Action onChanged)
        {
            _customOnChanged = onChanged;
            return this;
        }

        public Property<T> CreateProperty()
        {
            if (_property is { } existing)
                return existing;

            var property = new Property<T>(
                _initialValue,
                () =>
                {
                    _onChanging();
                    _customOnChanging?.Invoke();
                },
                () =>
                {
                    _onChanged();
                    _customOnChanged?.Invoke();
                });

            _property = property;
            return property;
        }

        public static implicit operator Property<T>(PropertyBuilder<T> builder) => builder.CreateProperty();
    }
}
