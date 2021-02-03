using Konoma.CrossFit.Forms;
using TemperatureConverter.Forms.Application.Login;

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
