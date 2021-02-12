using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class CommandBindingBuilder
    {
        private readonly BindingRegistry _registry;
        private readonly ICommand _command;

        public CommandBindingBuilder(BindingRegistry registry, ICommand command)
        {
            _registry = registry;
            _command = command;
        }

        public void To(ICommandTarget target)
        {
            _registry.RegisterBinding(new CommandBinding(_command, target));
        }
    }
}
