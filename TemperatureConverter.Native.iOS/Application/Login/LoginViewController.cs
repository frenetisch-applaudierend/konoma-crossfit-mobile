using System;
using System.Reflection;
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

        private PropertyBinding<string>? _usernameBinding;
        private PropertyBinding<string>? _passwordBinding;
        private CommandBinding? _signInBinding;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _usernameInput.Label = Scene.UsernameLabel;
            _passwordInput.Label = Scene.PasswordLabel;
            _signInButton.SetTitle(Scene.LoginButtonTitle, UIControlState.Normal);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var usernameSource = BindingEndpoint<string>.Create(Scene, scene => scene.Username.Editable);
            var usernameTarget = BindingEndpoint<string>.Create(
                _usernameInput,
                i => i.Text,
                (i, handler) =>
                {
                    // ReSharper disable once ConvertToLocalFunction
                    EventHandler observer = delegate { handler(); };
                    i.TextChanged += observer;
                    return observer;
                },
                (i, observer) => i.TextChanged -= observer);

            _usernameBinding = new PropertyBinding<string>(usernameSource, usernameTarget);
            _usernameBinding.HandleSourceUpdated();

            var passwordSource = BindingEndpoint<string>.Create(Scene, scene => scene.Password.Editable);
            var passwordTarget = BindingEndpoint<string>.Create(
                _passwordInput,
                i => i.Text,
                (i, handler) =>
                {
                    // ReSharper disable once ConvertToLocalFunction
                    EventHandler observer = delegate { handler(); };
                    i.TextChanged += observer;
                    return observer;
                },
                (i, observer) => i.TextChanged -= observer);

            _passwordBinding = new PropertyBinding<string>(passwordSource, passwordTarget);
            _passwordBinding.HandleSourceUpdated();

            _signInBinding = new CommandBinding(Scene.SignInCommand, new ButtonCommandTarget(_signInButton));
            _signInBinding.UpdateCanExecute();
        }
    }
}
