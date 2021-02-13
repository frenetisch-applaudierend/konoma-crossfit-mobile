using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class CommandBindingBuilder
    {
        public CommandBindingBuilder(BindingRegistry registry, ICommand command)
        {
            Registry = registry;
            Command = command;
        }

        protected BindingRegistry Registry { get; }
        protected ICommand Command { get; }

        public virtual void To(ICommandTarget target)
        {
            Registry.RegisterBinding(new CommandBinding(Command, target));
        }
    }
}
