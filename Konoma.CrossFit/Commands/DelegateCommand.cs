using System;

namespace Konoma.CrossFit
{
    public class DelegateCommand<T> : CommandBase<T>
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(T parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(T parameter) => _execute(parameter);

        public void UpdateCanExecute() => NotifyCanExecuteChanged();
    }

    public class DelegateCommand : DelegateCommand<object?>
    {
        public DelegateCommand(Action execute, Func<bool>? canExecute = null) : base(
            _ => execute(),
            canExecute != null ? new Func<object?, bool>(_ => canExecute()) : null)
        {
        }
    }
}
