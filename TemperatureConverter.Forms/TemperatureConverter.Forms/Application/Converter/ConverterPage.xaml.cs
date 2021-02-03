using System;
using System.Globalization;
using Konoma.CrossFit.Forms;
using TemperatureConverter.Forms.Application.Login;
using Xamarin.Forms;

namespace TemperatureConverter.Forms.Application.Converter
{
    public partial class ConverterPage
    {
        public ConverterPage()
        {
            InitializeComponent();

            Scene.ShowLogin.RegisterReplace(this, () => new LoginPage());
        }
    }
}
