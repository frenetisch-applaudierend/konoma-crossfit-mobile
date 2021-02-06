using System;
using UIKit;

namespace Konoma.CrossFit
{
    public interface ICommandTarget : IDisposable
    {
        void SetCommandCanExecute(bool canExecute);

        Action? OnExecute { get; set; }
    }

    public class ButtonCommandTarget : ICommandTarget
    {
        private readonly UIButton _button;

        public ButtonCommandTarget(UIButton button)
        {
            _button = button;
            _button.PrimaryActionTriggered += HandlePrimaryActionTriggered;
        }

        public Action? OnExecute { get; set; }

        public void SetCommandCanExecute(bool canExecute)
        {
            _button.Enabled = canExecute;
        }

        private void HandlePrimaryActionTriggered(object sender, EventArgs e)
        {
            OnExecute?.Invoke();
        }

        public void Dispose()
        {
            _button.PrimaryActionTriggered -= HandlePrimaryActionTriggered;
        }
    }
}
