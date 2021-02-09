using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public class MainPageNavigation<TScene> : EntryPointNavigation<TScene, MainPageNavigation<TScene>>
        where TScene : Scene
    {
        public MainPageNavigation(Application application, Func<CrossFitContentPage<TScene>> targetPage) : base(
            targetPage)
        {
            _application = application;
        }

        private readonly Application _application;

        public override Task NavigateAsync(bool animated)
        {
            _application.MainPage = InstantiatePage();
            return Task.CompletedTask;
        }
    }

    public partial class Navigation
    {
        public static MainPageNavigation<TScene> MainPage<TScene>(
            Application application,
            Func<CrossFitContentPage<TScene>> targetPage) where TScene : Scene
            => new MainPageNavigation<TScene>(application, targetPage);
    }
}
