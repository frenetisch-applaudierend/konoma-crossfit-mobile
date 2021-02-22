using Konoma.CrossFit;
using TemperatureConverter.Forms.Application.Converter;

namespace TemperatureConverter.Forms.Application.Login
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void ConnectNavigationPoints()
        {
            Scene.ShowConverter.Connect(
                FormsNavigation.MainPage(Xamarin.Forms.Application.Current, () => new ConverterPage())
                    .InNavigationPage());
        }
    }
}
