using System;
using System.Linq.Expressions;

namespace Konoma.CrossFit
{
    public class Binder<TScene>
        where TScene : Scene
    {
        private readonly BindingRegistry _registry;
        private readonly TScene _scene;

        public Binder(BindingRegistry registry, TScene scene)
        {
            _registry = registry;
            _scene = scene;
        }

        public PropertyBindingBuilder<T> Bind<T>(Expression<Func<TScene, T>> sceneProperty)
        {
            var source = BindingEndpoint<T>.Create(_scene, sceneProperty);
            return new PropertyBindingBuilder<T>(_registry, source);
        }
    }
}
