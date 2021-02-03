using System;
using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class Command : ICommand
    {
        public class Control
        {
            internal Command? Command { get; set; }

            public void NotifyCanExecuteChanged() => this.Command?.NotifyCanExecuteChanged();
        }

        public Command(Action action) : this(null!, action) { }

        public Command(Control control, Action action)
        {
            _action = action;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (!(control is null))
                control.Command = this;
        }

        private readonly Action _action;

        private Func<bool>? _canExecuteCallback;

        public Func<bool>? CanExecuteCallback
        {
            get => _canExecuteCallback;
            set
            {
                _canExecuteCallback = value;
                NotifyCanExecuteChanged();
            }
        }


        public bool CanExecute(object parameter)
        {
            if (_canExecuteCallback is { } callback)
                return callback();

            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler? CanExecuteChanged;

        private void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
