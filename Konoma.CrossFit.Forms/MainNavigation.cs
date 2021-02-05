using Xamarin.Forms;

namespace Konoma.CrossFit.Forms
{
    public abstract class MainNavigation
    {
        protected MainNavigation(Application application)
        {
            _application = application;
        }

        private readonly Application _application;

        protected INavigation<TScene> Show<TScene>(CrossFitContentPage<TScene> page, bool wrap = false)
            where TScene : Scene
        {
            var rootPage = wrap ? (Page)new NavigationPage(page) : page;
            return CrossFit.Navigation.For<TScene>(() => _application.MainPage = rootPage);
        }
    }
}
