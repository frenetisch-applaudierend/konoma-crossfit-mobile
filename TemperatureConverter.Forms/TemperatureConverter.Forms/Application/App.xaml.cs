using Konoma.CrossFit;
using Konoma.CrossFit.Forms;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Application.Converter;
using TemperatureConverter.Core.Application.Login;
using TemperatureConverter.Core.Services;
using TemperatureConverter.Forms.Application.Converter;
using TemperatureConverter.Forms.Application.Login;
using TemperatureConverter.Forms.Services;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TemperatureConverter.Forms.Application
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            StartApplication();
        }

        protected override void RegisterServices(IServiceRegistration services)
        {
            services.AddSingleton<IPreferencesService, PreferencesService>();
        }

        protected override TemperatureConverterCoordinator.IMainNavigation CreateMainNavigation() =>
            new MainNavigation(this);
    }

    public class MainNavigation : Konoma.CrossFit.Forms.MainNavigation, TemperatureConverterCoordinator.IMainNavigation
    {
        public MainNavigation(Xamarin.Forms.Application app) : base(app) { }

        public INavigation<LoginScene> ShowLogin => Show(new LoginPage(), wrap: true);

        public INavigation<ConverterScene> ShowHome => Show(new ConverterPage(), wrap: true);
    }

    public abstract class FormsApp : CrossFitFormsApplication<
        TemperatureConverterCoordinator,
        TemperatureConverterCoordinator.Startup,
        TemperatureConverterCoordinator.IMainNavigation>
    {
    }
}
