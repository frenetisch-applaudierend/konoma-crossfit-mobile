using System;
using System.Windows.Input;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Resources;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Core.Application.Login
{
    public class LoginScene : Scene
    {
        #region Initialization

        public LoginScene(LoginService loginService)
        {
            _loginService = loginService;
        }

        #endregion

        #region Dependencies

        private readonly LoginService _loginService;

        #endregion

        #region View Properties

        public string ScreenTitle { get; } = LoginStrings.ScreenTitle;

        public string UsernameLabel { get; } = LoginStrings.Username;

        public string PasswordLabel { get; } = LoginStrings.Password;

        public string LoginButtonTitle { get; } = LoginStrings.SignIn;

        public Property<string> Username => GetProperty("", _signInControl.NotifyCanExecuteChanged);

        public Property<string> Password => GetProperty("", _signInControl.NotifyCanExecuteChanged);

        #endregion

        #region Commands

        private readonly Command.Control _signInControl = new Command.Control();

        public ICommand SignInCommand => new Command(_signInControl, SignIn) {CanExecuteCallback = CanSignIn};

        private void SignIn()
        {
            _loginService.LogIn();
            ShowConverter.Navigate();
        }

        private bool CanSignIn()
        {
            return !(string.IsNullOrWhiteSpace(Username) || string.IsNullOrEmpty(Password));
        }

        #endregion

        #region Navigation

        public NavigationPoint<ConverterScene> ShowConverter { get; } = new NavigationPoint<ConverterScene>();

        #endregion
    }
}
