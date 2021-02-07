using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Native.iOS.Application.Common;
using TemperatureConverter.Native.iOS.Application.Login;
using UIKit;

namespace TemperatureConverter.Native.iOS.Application.Converter
{
    public class ConverterViewController : CrossFitViewController<ConverterScene>
    {
        private LabeledTextInput _celsiusInput = null!;
        private LabeledTextInput _fahrenheitInput = null!;
        private UIButton _logoutButton = null!;

        public override void LoadView()
        {
            View = new UIView {BackgroundColor = UIColor.SystemBackgroundColor};

            _celsiusInput = new LabeledTextInput();
            _fahrenheitInput = new LabeledTextInput();

            _logoutButton = UIButton.GetSystemButton(null);

            var stackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Spacing = 32.0f,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            stackView.AddArrangedSubview(_celsiusInput);
            stackView.AddArrangedSubview(_fahrenheitInput);
            stackView.AddArrangedSubview(_logoutButton);

            View.AddSubview(stackView);

            var content = View.SafeAreaLayoutGuide;
            NSLayoutConstraint.ActivateConstraints(
                new[]
                {
                    stackView.LeadingAnchor.ConstraintEqualToSystemSpacingAfterAnchor(content.LeadingAnchor, 1.0f),
                    content.TrailingAnchor.ConstraintEqualToSystemSpacingAfterAnchor(
                        stackView.TrailingAnchor,
                        1.0f),
                    stackView.TopAnchor.ConstraintEqualToSystemSpacingBelowAnchor(content.TopAnchor, 1.0f),
                    content.BottomAnchor.ConstraintGreaterThanOrEqualToSystemSpacingBelowAnchor(
                        stackView.BottomAnchor,
                        1.0f),
                });
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = Scene.ScreenTitle;

            _celsiusInput.Label = Scene.CelsiusLabel;
            _fahrenheitInput.Label = Scene.FahrenheitLabel;
            _logoutButton.SetTitle(Scene.LogoutButton, UIControlState.Normal);
        }

        protected override void ConnectNavigationPoints()
        {
            Scene.ShowLogin.Connect(
                Navigation
                    .Root<LoginScene, LoginViewController>(this)
                    .InNavigationController());
        }

        protected override void ArrangeBindings(Binder<ConverterScene> binder)
        {
            binder.Bind(scene => scene.Celsius.Editable).To(_celsiusInput);
            binder.Bind(scene => scene.Fahrenheit.Editable).To(_fahrenheitInput);

            binder.Bind(Scene.SignOutCommand).To(_logoutButton);
        }
    }
}
