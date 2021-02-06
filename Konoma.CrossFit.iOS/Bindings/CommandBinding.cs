using System;
using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class CommandBinding : IDisposable
    {
        public CommandBinding(ICommand command, ICommandTarget target)
        {
            command.CanExecuteChanged += HandleCanExecuteChanged;
            target.OnExecute = HandleOnExecute;

            _command = command;
            _target = target;
        }

        private readonly ICommand _command;
        private readonly ICommandTarget _target;

        public void UpdateCanExecute() =>
            _target.SetCommandCanExecute(_command.CanExecute(null));

        private void HandleCanExecuteChanged(object sender, EventArgs e) =>
            UpdateCanExecute();

        private void HandleOnExecute() =>
            _command.Execute(null);

        public void Dispose()
        {
            _command.CanExecuteChanged -= HandleCanExecuteChanged;

            _target.OnExecute = null;
            _target.Dispose();
        }
    }
}
