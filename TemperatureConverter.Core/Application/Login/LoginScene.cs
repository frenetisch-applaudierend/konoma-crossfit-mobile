using System;
using System.Windows.Input;
using Konoma.CrossFit;
using TemperatureConverter.Core.Resources;

namespace TemperatureConverter.Core.Application.Login
{
    public class LoginScene : Scene
    {
        public string ScreenTitle { get; } = LoginStrings.ScreenTitle;

        public string UsernameLabel { get; } = LoginStrings.Username;

        public string PasswordLabel { get; } = LoginStrings.Password;

        public string LoginButtonTitle { get; } = LoginStrings.SignIn;

        public Property<string> Username =>
            GetProperty("")
                .OnChanged(() => _signInControl.NotifyCanExecuteChanged());

        public Property<string> Password =>
            GetProperty("")
                .OnChanged(() => _signInControl.NotifyCanExecuteChanged());

        private readonly Command.Control _signInControl = new Command.Control();

        public ICommand SignInCommand => new Command(_signInControl, SignIn) {CanExecuteCallback = CanSignIn};

        private void SignIn()
        {
            Console.WriteLine("Sign in");
        }

        private bool CanSignIn()
        {
            return !(string.IsNullOrWhiteSpace(this.Username.Value) || string.IsNullOrEmpty(this.Password.Value));
        }
    }
}
