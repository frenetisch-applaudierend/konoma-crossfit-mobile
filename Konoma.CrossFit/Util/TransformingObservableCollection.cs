using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Konoma.CrossFit
{
    public class TransformingObservableCollection<TElement, TSource> : IObservableReadonlyCollection<TElement>
    {
        private readonly IObservableReadonlyCollection<TSource> _sourceCollection;
        private readonly Func<TSource, TElement> _transformer;

        public TransformingObservableCollection(
            IObservableReadonlyCollection<TSource> sourceCollection,
            Func<TSource, TElement> transformer)
        {
            _sourceCollection = sourceCollection;
            _transformer = transformer;

            _sourceCollection.CollectionChanged += SourceCollectionOnCollectionChanged;
            _sourceCollection.PropertyChanged += SourceCollectionOnPropertyChanged;
        }

        ~TransformingObservableCollection()
        {
            Console.WriteLine("TransformingObservableCollection collected");
        }

        private void SourceCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(
                this,
                e.Action switch
                {
                    NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(
                        e.Action,
                        ConvertItems(e.NewItems),
                        e.NewStartingIndex),

                    NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(
                        e.Action,
                        ConvertItems(e.OldItems),
                        e.OldStartingIndex),

                    NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(
                        e.Action,
                        ConvertItems(e.NewItems),
                        ConvertItems(e.OldItems),
                        e.OldStartingIndex),

                    NotifyCollectionChangedAction.Move => new NotifyCollectionChangedEventArgs(
                        e.Action,
                        ConvertItems(e.OldItems),
                        e.NewStartingIndex,
                        e.OldStartingIndex),

                    NotifyCollectionChangedAction.Reset => new NotifyCollectionChangedEventArgs(e.Action),

                    _ => throw new ArgumentOutOfRangeException()
                });

            IList<TElement>? ConvertItems(IEnumerable? items)
            {
                if (items is null)
                    return null;

                return (from object item in items select _transformer((TSource)item)).ToList();
            }
        }

        private void SourceCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


        public IEnumerator<TElement> GetEnumerator() => _sourceCollection.Select(_transformer).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _sourceCollection.Count;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}