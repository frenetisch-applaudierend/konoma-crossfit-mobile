using System;
using CoreMedia;
using UIKit;

namespace TemperatureConverter.Native.iOS.Application.Common
{
    public sealed class LabeledTextInput : UIStackView
    {
        public LabeledTextInput()
        {
            Axis = UILayoutConstraintAxis.Vertical;
            Spacing = 8.0f;

            AddArrangedSubview(_labelView);
            AddArrangedSubview(_inputView);

            _inputView.BorderStyle = UITextBorderStyle.RoundedRect;
        }

        private readonly UILabel _labelView = new UILabel();
        private readonly UITextField _inputView = new UITextField();

        public string Label
        {
            get => _labelView.Text ?? "";
            set => _labelView.Text = value;
        }

        public string Text
        {
            get => _inputView.Text ?? "";
            set => _inputView.Text = value;
        }

        public event EventHandler TextChanged
        {
            add => _inputView.EditingChanged += value;
            remove => _inputView.EditingChanged -= value;
        }
    }
}
