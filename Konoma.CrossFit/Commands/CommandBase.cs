using System;
using System.ComponentModel;

namespace Konoma.CrossFit
{
    public abstract class CommandBase<T> : ICommand<T>
    {
        public abstract bool CanExecute(T parameter);

        public abstract void Execute(T parameter);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool CanExecute(object? parameter)
        {
            if (!(parameter is null || parameter is T))
                throw new ArgumentException($"Parameter must be of type {typeof(T)}", nameof(parameter));

            return CanExecute((T)parameter!);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Execute(object? parameter)
        {
            if (!(parameter is null || parameter is T))
                throw new ArgumentException($"Parameter must be of type {typeof(T)}", nameof(parameter));

            Execute((T)parameter!);
        }

        public event EventHandler? CanExecuteChanged;

        protected void NotifyCanExecuteChanged(EventArgs? e = null)
        {
            CanExecuteChanged?.Invoke(this, e ?? EventArgs.Empty);
        }
    }
}
