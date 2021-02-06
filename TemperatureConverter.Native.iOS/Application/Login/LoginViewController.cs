using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Native.iOS.Application.Common;
using UIKit;

namespace TemperatureConverter.Native.iOS.Application.Login
{
    public class LoginViewController : CrossFitViewController<LoginScene>
    {
        private LabeledTextInput _usernameInput = null!;
        private LabeledTextInput _passwordInput = null!;

        public override void LoadView()
        {
            View = new UIView {BackgroundColor = UIColor.SystemBackgroundColor};

            _usernameInput = new LabeledTextInput();
            _passwordInput = new LabeledTextInput();

            var stackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Spacing = 32.0f,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            stackView.AddArrangedSubview(_usernameInput);
            stackView.AddArrangedSubview(_passwordInput);

            View.AddSubview(stackView);

            var content = View.SafeAreaLayoutGuide;
            NSLayoutConstraint.ActivateConstraints(
                new[]
                {
                    stackView.LeadingAnchor.ConstraintEqualToSystemSpacingAfterAnchor(content.LeadingAnchor, 1.0f),
                    content.TrailingAnchor.ConstraintEqualToSystemSpacingAfterAnchor(stackView.TrailingAnchor, 1.0f),
                    stackView.TopAnchor.ConstraintEqualToSystemSpacingBelowAnchor(content.TopAnchor, 1.0f),
                    content.BottomAnchor.ConstraintGreaterThanOrEqualToSystemSpacingBelowAnchor(
                        stackView.BottomAnchor, 1.0f),
                });
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _usernameInput.Label = Scene.UsernameLabel;
            _passwordInput.Label = Scene.PasswordLabel;
        }
    }
}
