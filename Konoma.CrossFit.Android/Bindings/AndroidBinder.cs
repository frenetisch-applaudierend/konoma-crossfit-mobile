using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class AndroidBinder<TScene> : Binder<TScene>
        where TScene : Scene
    {
        private readonly MenuItemCommandRegistry _menuRegistry;

        public AndroidBinder(BindingRegistry registry, MenuItemCommandRegistry menuRegistry, TScene scene)
            : base(registry, scene)
        {
            _menuRegistry = menuRegistry;
        }

        public override CommandBindingBuilder Bind(ICommand command) =>
            new AndroidCommandBindingBuilder(Registry, _menuRegistry, command);
    }
}
