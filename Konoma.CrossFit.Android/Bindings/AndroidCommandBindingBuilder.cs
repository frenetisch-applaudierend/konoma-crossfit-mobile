using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class AndroidCommandBindingBuilder : CommandBindingBuilder
    {
        public AndroidCommandBindingBuilder(
            BindingRegistry bindingRegistry,
            MenuItemCommandRegistry menuRegistry,
            ICommand command)
            : base(bindingRegistry, command)
        {
            _menuRegistry = menuRegistry;
        }

        private readonly MenuItemCommandRegistry _menuRegistry;

        public override void To(ICommandTarget target)
        {
            if (target is OptionsMenuCommandTarget menuTarget)
                _menuRegistry.RegisterHandler(menuTarget);

            base.To(target);
        }
    }
}
