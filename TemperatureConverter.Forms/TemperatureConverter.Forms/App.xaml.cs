using Konoma.CrossFit.Forms;
using TemperatureConverter.Core.Application;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TemperatureConverter.Forms
{
    public partial class App : FormsApp
    {
        public App()
        {
            InitializeComponent();
            StartApplication();
        }
    }

    public class FormsApp : CrossFitFormsApplication<TemperatureConverterApp>
    {

    }
}
