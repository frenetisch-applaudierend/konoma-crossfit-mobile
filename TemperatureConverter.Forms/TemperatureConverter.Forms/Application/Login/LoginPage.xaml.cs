using Konoma.CrossFit.Forms;
using TemperatureConverter.Forms.Application.Converter;

namespace TemperatureConverter.Forms.Application.Login
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();

            Scene.ShowConverter.RegisterReplace(this, () => new ConverterPage());
        }
    }
}
