using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Konoma.CrossFit
{
    public interface IReadonlyObservableCollection<out T> :
        IReadOnlyCollection<T>,
        INotifyCollectionChanged,
        INotifyPropertyChanged
    {
    }
}
