using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Konoma.CrossFit
{
    public interface IBindingEndpoint<T> : IDisposable
    {
        Action? OnChanged { get; set; }

        T GetValue();

        void SetValue(T value);

        bool Writable { get; }
    }

    public sealed class BindingEndpoint<T> : IBindingEndpoint<T>
    {
        public static BindingEndpoint<T> Create<TModel>(
            TModel model,
            Expression<Func<TModel, T>> expression)
            where TModel : class, INotifyPropertyChanged
        {
            var property = PropertyExpressionParser.ParseExpression<T>(expression.Body, model, mustBeObservable: true);
            var endpoint = new BindingEndpoint<T>(property.Getter, property.Setter);

            model.PropertyChanged += Handler;
            endpoint._cleanup = () => model.PropertyChanged -= Handler;

            return endpoint;

            void Handler(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName != property.ObservablePropertyName) return;

                endpoint.HandleValueChanged();
            }
        }

        public static BindingEndpoint<T> Create<TModel, TObserver>(
            TModel model,
            Expression<Func<TModel, T>> expression,
            Func<TModel, Action, TObserver> registerObserver,
            Action<TModel, TObserver> unregisterObserver)
            where TModel : class
        {
            var property = PropertyExpressionParser.ParseExpression<T>(expression.Body, model, mustBeObservable: false);
            var endpoint = new BindingEndpoint<T>(property.Getter, property.Setter);

            var observer = registerObserver(model, endpoint.HandleValueChanged);
            endpoint._cleanup = () => unregisterObserver(model, observer);

            return endpoint;
        }

        private BindingEndpoint(Func<T> getter, Action<T>? setter)
        {
            _getter = getter;
            _setter = setter;
        }

        private Func<T> _getter;
        private Action<T>? _setter;
        private Action? _cleanup;

        public Action? OnChanged { get; set; }

        public T GetValue()
        {
            CheckDisposed();
            return _getter();
        }

        public void SetValue(T value)
        {
            CheckDisposed();
            _setter?.Invoke(value);
        }

        public bool Writable => !(_setter is null);

        private void HandleValueChanged()
        {
            CheckDisposed();
            OnChanged?.Invoke();
        }

        private bool _disposed;

        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(BindingEndpoint<T>));
        }

        public void Dispose()
        {
            _disposed = true;

            _cleanup?.Invoke();
            _cleanup = null;

            _getter = null!;
            _setter = null;

            OnChanged = null;
        }
    }
}
