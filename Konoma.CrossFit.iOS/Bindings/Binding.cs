using System;

namespace Konoma.CrossFit
{
    public sealed class Binding<T> : IDisposable
    {
        public Binding(BindingEndpoint<T> source, BindingEndpoint<T> target)
        {
            _source = source;
            _target = target;
        }

        private readonly BindingEndpoint<T> _source;
        private readonly BindingEndpoint<T> _target;

        //
        // public void HandleSourceUpdated()
        // {
        //     if (!(_target.Setter is { } setter))
        //         return;
        //
        //     var value = _source.Getter.Invoke();
        //     setter.Invoke(value);
        // }
        //
        // public void HandleTargetUpdated()
        // {
        //     if (!(_source.Setter is { } setter))
        //         return;
        //
        //     var value = _target.Getter.Invoke();
        //     setter.Invoke(value);
        // }

        public void Dispose()
        {
            _source.Dispose();
            _target.Dispose();
        }
    }
}
