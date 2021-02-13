using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class Binder<TScene>
        where TScene : Scene
    {
        public Binder(BindingRegistry registry, TScene scene)
        {
            Registry = registry;
            Scene = scene;
        }

        protected BindingRegistry Registry { get; }

        protected TScene Scene { get; }

        public virtual PropertyBindingBuilder<T> Bind<T>(Expression<Func<TScene, T>> sceneProperty)
        {
            var source = BindingEndpoint<T>.Create(Scene, sceneProperty);
            return new PropertyBindingBuilder<T>(Registry, source);
        }

        public virtual CommandBindingBuilder Bind(ICommand command) =>
            new CommandBindingBuilder(Registry, command);
    }
}
