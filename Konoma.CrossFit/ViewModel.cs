using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Konoma.CrossFit
{
    public abstract class ViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler? PropertyChanging;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = default!)
        {
            if (Equals(field, value))
                return;

            NotifyPropertyChanging(propertyName);
            field = value;
            NotifyPropertyChanged(propertyName);
        }

        protected void NotifyPropertyChanging(string propertyName) =>
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        protected void NotifyPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
