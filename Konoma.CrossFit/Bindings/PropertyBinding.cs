using System;

namespace Konoma.CrossFit
{
    public sealed class PropertyBinding<T> : IBinding
    {
        public PropertyBinding(IBindingEndpoint<T> source, IBindingEndpoint<T> target)
        {
            _source = source;
            _target = target;

            source.OnChanged = HandleSourceUpdated;
            target.OnChanged = HandleTargetUpdated;
        }

        private readonly IBindingEndpoint<T> _source;
        private readonly IBindingEndpoint<T> _target;

        private void HandleSourceUpdated()
        {
            if (!_target.Writable)
                return;

            var value = _source.GetValue();
            _target.SetValue(value);
        }

        private void HandleTargetUpdated()
        {
            if (!_source.Writable)
                return;

            var value = _target.GetValue();
            _source.SetValue(value);
        }

        public Action<IBinding>? OnDisposed { get; set; }

        public void SetupAfterRegistration()
        {
            HandleSourceUpdated();
        }

        public void Dispose()
        {
            _source.Dispose();
            _target.Dispose();

            OnDisposed?.Invoke(this);
            OnDisposed = null;
        }
    }
}
