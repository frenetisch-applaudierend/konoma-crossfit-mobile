using System;
using System.Linq.Expressions;

namespace Konoma.CrossFit
{
    public class PropertyBindingBuilder<T>
    {
        private readonly BindingRegistry _registry;
        private readonly IBindingEndpoint<T> _sourceEndpoint;

        public PropertyBindingBuilder(BindingRegistry registry, IBindingEndpoint<T> sourceEndpoint)
        {
            _registry = registry;
            _sourceEndpoint = sourceEndpoint;
        }

        public void To(IBindingEndpoint<T> targetEndpoint)
        {
            var binding = new PropertyBinding<T>(_sourceEndpoint, targetEndpoint);

            _registry.RegisterBinding(binding);
        }

        public void To<TTarget, TObserver>(
            TTarget target,
            Expression<Func<TTarget, T>> targetExpression,
            Func<TTarget, Action, TObserver> register,
            Action<TTarget, TObserver> unregister)
            where TTarget : class
        {
            var targetEndpoint = BindingEndpoint<T>.Create(target, targetExpression, register, unregister);
            To(targetEndpoint);
        }

        public void To<TTarget>(
            TTarget target,
            Expression<Func<TTarget, T>> targetExpression,
            Action<TTarget, EventHandler> register,
            Action<TTarget, EventHandler> unregister)
            where TTarget : class
        {
            To(target, targetExpression, RegisterObserver, unregister);

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
