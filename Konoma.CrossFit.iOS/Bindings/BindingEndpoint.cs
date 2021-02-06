using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Konoma.CrossFit
{
    public sealed class BindingEndpoint<T> : IDisposable
    {
        public static BindingEndpoint<T> Create<TModel>(
            TModel model,
            Expression<Func<TModel, T>> propertyExpression,
            Action<Action> registerObserver,
            Action<Action> unregisterObserver)
            where TModel : class, INotifyPropertyChanged
        {

        }

        private BindingEndpoint(Func<T> getter, Action<T>? setter, Action? cleanup)
        {
            _getter = getter;
            _setter = setter;
            _cleanup = cleanup;
        }

        private Func<T> _getter;
        private Action<T>? _setter;
        private Action? _cleanup;

        public T GetValue()
        {
            _getter();
        }

        public void SetValue(T value)
        {
        }

        public Action? OnChanged { get; set; }

        private void HandleValueChanged()
        {
            OnChanged?.Invoke();
        }

        public void Dispose()
        {
            _cleanup?.Invoke();
            _cleanup = null;

            OnChanged = null;
        }
    }
}
