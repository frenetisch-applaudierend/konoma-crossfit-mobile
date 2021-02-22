using Konoma.CrossFit;
using TemperatureConverter.Forms.Application.Login;

namespace TemperatureConverter.Forms.Application.Converter
{
    public partial class ConverterPage
    {
        public ConverterPage()
        {
            InitializeComponent();
        }

        protected override void ConnectNavigationPoints()
        {
            Scene.ShowLogin.Connect(
                FormsNavigation.MainPage(Xamarin.Forms.Application.Current, () => new LoginPage()).InNavigationPage());
        }
    }
}
