using System;
using Android.App;
using Android.Widget;

namespace Konoma.CrossFit
{
    public class ButtonCommandTarget : ICommandTarget
    {
        private readonly Button _button;

        public ButtonCommandTarget(Button button)
        {
            _button = button;
            _button.Click += HandleClick;
        }

        public Action? OnExecute { get; set; }

        public void SetCommandCanExecute(bool canExecute)
        {
            _button.Enabled = canExecute;
        }

        private void HandleClick(object sender, EventArgs e)
        {
            OnExecute?.Invoke();
        }

        public void Dispose()
        {
            _button.Click -= HandleClick;
        }
    }

    public static class CommandingBindingBuilderButtonCommandTargetExtensions
    {
        public static void To(this CommandBindingBuilder builder, Button button) =>
            builder.To(new ButtonCommandTarget(button));

        public static void ToButton(this CommandBindingBuilder builder, Activity activity, int buttonId)
        {
            if (activity.FindViewById<Button>(buttonId) is { } button)
                To(builder, button);
        }
    }
}
