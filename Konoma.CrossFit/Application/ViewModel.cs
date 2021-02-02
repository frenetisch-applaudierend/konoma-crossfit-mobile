using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Konoma.CrossFit
{
    public abstract class ViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Bindable Property Support

        private readonly Dictionary<string, object> _propertyStorage = new Dictionary<string, object>();

        protected PropertyBuilder<T> GetProperty<T>(T initialValue, [CallerMemberName] string propertyName = default!)
        {
            if (_propertyStorage.TryGetValue(propertyName, out var existing))
                return (PropertyBuilder<T>)existing;

            var builder = new PropertyBuilder<T>(
                name: propertyName,
                initialValue: initialValue,
                onChanging: () => NotifyPropertyChanging(propertyName),
                onChanged: () => NotifyPropertyChanged(propertyName));

            _propertyStorage[propertyName] = builder;
            return builder;
        }

        #endregion

        #region Custom Properties Support

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = default!)
        {
            if (Equals(field, value))
                return;

            NotifyPropertyChanging(propertyName);
            field = value;
            NotifyPropertyChanged(propertyName);
        }

        #endregion

        #region INotifyPropertyChanging and INotifyPropertyChanged

        public event PropertyChangingEventHandler? PropertyChanging;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanging(string propertyName) =>
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        protected void NotifyPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
