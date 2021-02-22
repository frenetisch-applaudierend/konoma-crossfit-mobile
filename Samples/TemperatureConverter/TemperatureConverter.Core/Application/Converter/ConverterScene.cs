using System;
using System.Windows.Input;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Resources;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Core.Application.Converter
{
    public class ConverterScene : Scene
    {
        #region Initialization

        public ConverterScene(TemperatureConverterService converter, LoginService loginService)
        {
            _converter = converter;
            _loginService = loginService;
        }

        #endregion

        #region Dependencies

        private readonly TemperatureConverterService _converter;
        private readonly LoginService _loginService;

        #endregion

        #region View Properties

        public string ScreenTitle { get; } = ConverterStrings.ScreenTitle;

        public string CelsiusLabel { get; } = ConverterStrings.CelsiusLabel;

        public string FahrenheitLabel { get; } = ConverterStrings.FahrenheitLabel;

        public string LogoutButton { get; } = ConverterStrings.Logout;

        public Property<string> Celsius => GetProperty("0", CelsiusChanged);

        public Property<string> Fahrenheit => GetProperty("0", FahrenheitChanged);

        #endregion

        #region Commands

        public ICommand SignOutCommand => new DelegateCommand(SignOut);

        #endregion

        #region Actions

        private void CelsiusChanged()
        {
            var celsiusValue = ParseNumber(Celsius);
            var fahrenheitValue = _converter.ConvertCelsiusToFahrenheit(celsiusValue);

            Fahrenheit.Set(Format(fahrenheitValue), notify: false);
            NotifyPropertyChanged(nameof(Fahrenheit));
        }

        private void FahrenheitChanged()
        {
            var fahrenheitValue = ParseNumber(Fahrenheit);
            var celsiusValue = _converter.ConvertFahrenheitToCelsius(fahrenheitValue);

            Celsius.Set(Format(celsiusValue), notify: false);
            NotifyPropertyChanged(nameof(Celsius));
        }

        private static double ParseNumber(string numberValue) =>
            double.TryParse(numberValue, out var result) ? result : 0.0;

        private static string Format(double number) => number.ToString("F2");

        private void SignOut()
        {
            _loginService.LogOut();
            ShowLogin.Navigate();
        }

        #endregion

        #region Navigation

        public NavigationPoint<LoginScene> ShowLogin { get; } = new NavigationPoint<LoginScene>();

        #endregion
    }
}
