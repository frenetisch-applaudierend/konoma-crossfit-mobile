using System.Windows.Input;

namespace Konoma.CrossFit
{
    public interface ICommand<in T> : ICommand
    {
        bool CanExecute(T parameter);

        void Execute(T parameter);
    }
}
