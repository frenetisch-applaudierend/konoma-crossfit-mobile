namespace TemperatureConverter.Core.Services
{
    public class TemperatureConverterService
    {
        public double ConvertCelsiusToFahrenheit(double celsius) => celsius * 1.8 + 32;

        public double ConvertFahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32) / 1.8;
    }
}
