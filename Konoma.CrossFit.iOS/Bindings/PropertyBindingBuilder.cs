using System;
using System.Linq.Expressions;

namespace Konoma.CrossFit
{
    public class PropertyBindingBuilder<T>
    {
        private readonly BindingRegistry _registry;
        private readonly BindingEndpoint<T> _sourceEndpoint;
        private bool _registered;

        public PropertyBindingBuilder(BindingRegistry registry, BindingEndpoint<T> sourceEndpoint)
        {
            _registry = registry;
            _sourceEndpoint = sourceEndpoint;
        }

        ~PropertyBindingBuilder()
        {
            if (!_registered)
                throw new InvalidOperationException("Binding must end with a To(...) call to register binding");
        }

        public void To<TTarget>(
            TTarget target,
            Expression<Func<TTarget, T>> targetExpression,
            Action<TTarget, EventHandler> register,
            Action<TTarget, EventHandler> unregister)
            where TTarget : class
        {
            var targetEndpoint = BindingEndpoint<T>.Create(target, targetExpression, RegisterObserver, unregister);
            var binding = new PropertyBinding<T>(_sourceEndpoint, targetEndpoint);

            _registry.RegisterBinding(binding);
            _registered = true;

            EventHandler RegisterObserver(TTarget t, Action handler)
            {
                // ReSharper disable once ConvertToLocalFunction
                EventHandler observer = delegate { handler(); };
                register(t, observer);
                return observer;
            }
        }
    }
}
