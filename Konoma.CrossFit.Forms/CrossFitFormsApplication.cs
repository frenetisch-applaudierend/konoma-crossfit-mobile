using System.Threading.Tasks;
using Konoma.CrossFit.Util;

namespace Konoma.CrossFit.Forms
{
    public abstract class
        CrossFitFormsApplication<TApp, TStartup, TMainNavigation> : Xamarin.Forms.Application
        where TApp : CrossFitApplication<TStartup, TMainNavigation>, new()
        where TStartup : IStartup<TMainNavigation>
    {
        protected void StartApplication()
        {
            StartApplicationAsync().FireAndForget();
        }

        protected async Task StartApplicationAsync()
        {
            MainPage = CreateMainPage();

            var app = new TApp();
            await app.InitializeAsync(services => services.RegisterSingleton<TStartup>());
            await app.StartApplicationAsync(CreateMainNavigation());
        }

        protected virtual Xamarin.Forms.Page CreateMainPage() => new Xamarin.Forms.Page();

        protected abstract TMainNavigation CreateMainNavigation();
    }
}
