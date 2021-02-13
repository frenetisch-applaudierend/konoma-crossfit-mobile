using System;
using Android.App;
using Android.Text;
using Android.Widget;

namespace Konoma.CrossFit
{
    public class EditTextBindingEndpoint : IBindingEndpoint<string>
    {
        private readonly EditText _editText;

        public EditTextBindingEndpoint(EditText editText)
        {
            _editText = editText;

            editText.TextChanged += HandleTextChanged;
        }

        public void Dispose()
        {
            _editText.TextChanged -= HandleTextChanged;
        }

        public Action? OnChanged { get; set; }

        public string GetValue() => _editText.Text ?? "";

        public void SetValue(string value)
        {
            if (_editText.Text != value)
                _editText.Text = value;
        }

        public bool Writable => true;

        private void HandleTextChanged(object sender, TextChangedEventArgs e) => OnChanged?.Invoke();
    }

    public static class EditTextBindingExtensions
    {
        public static void ToEditText(
            this PropertyBindingBuilder<string> builder,
            Activity activity,
            int targetId)
        {
            var view = activity.RequireViewById<EditText>(targetId);
            builder.To(new EditTextBindingEndpoint(view));
        }
    }
}
