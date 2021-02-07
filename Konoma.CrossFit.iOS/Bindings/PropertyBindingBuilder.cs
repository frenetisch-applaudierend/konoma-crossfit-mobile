using System;
using System.Linq.Expressions;

namespace Konoma.CrossFit
{
    public class PropertyBindingBuilder<T>
    {
        private readonly BindingRegistry _registry;
        private readonly BindingEndpoint<T> _sourceEndpoint;

        public PropertyBindingBuilder(BindingRegistry registry, BindingEndpoint<T> sourceEndpoint)
        {
            _registry = registry;
            _sourceEndpoint = sourceEndpoint;
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
