using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Native.iOS.Application.Common;
using TemperatureConverter.Native.iOS.Application.Converter;
using UIKit;

namespace TemperatureConverter.Native.iOS.Application.Login
{
    public class LoginViewController : CrossFitViewController<LoginScene>
    {
        private LabeledTextInput _usernameInput = null!;
        private LabeledTextInput _passwordInput = null!;
        private UIButton _signInButton = null!;

        public override void LoadView()
        {
            View = new UIView {BackgroundColor = UIColor.SystemBackgroundColor};

            _usernameInput = new LabeledTextInput();
            _passwordInput = new LabeledTextInput();

            _signInButton = UIButton.GetSystemButton(null);

            var stackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Spacing = 32.0f,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            stackView.AddArrangedSubview(_usernameInput);
            stackView.AddArrangedSubview(_passwordInput);
            stackView.AddArrangedSubview(_signInButton);

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

            _usernameInput.Label = Scene.UsernameLabel;
            _passwordInput.Label = Scene.PasswordLabel;
            _signInButton.SetTitle(Scene.LoginButtonTitle, UIControlState.Normal);
        }

        protected override void ConnectNavigationPoints()
        {
            Scene.ShowConverter.Connect(
                Navigation
                    .Root<ConverterScene, ConverterViewController>(this)
                    .InNavigationController());
        }

        protected override void ArrangeBindings(Binder<LoginScene> binder)
        {
            binder.Bind(scene => scene.Username.Editable).To(_usernameInput);
            binder.Bind(scene => scene.Password.Editable).To(_passwordInput);

            binder.Bind(Scene.SignInCommand).To(_signInButton);
        }
    }
}
