using System;

namespace Konoma.CrossFit
{
    public sealed class PropertyBinding<T> : IDisposable
    {
        public PropertyBinding(BindingEndpoint<T> source, BindingEndpoint<T> target)
        {
            _source = source;
            _target = target;

            source.OnChanged = HandleSourceUpdated;
            target.OnChanged = HandleTargetUpdated;
        }

        private readonly BindingEndpoint<T> _source;
        private readonly BindingEndpoint<T> _target;

        public void HandleSourceUpdated()
        {
            if (!_target.Writable)
                return;

            var value = _source.GetValue();
            _target.SetValue(value);
        }

        public void HandleTargetUpdated()
        {
            if (!_source.Writable)
                return;

            var value = _target.GetValue();
            _source.SetValue(value);
        }

        public void Dispose()
        {
            _source.Dispose();
            _target.Dispose();
        }
    }
}
