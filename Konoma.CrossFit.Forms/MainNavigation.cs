namespace Konoma.CrossFit.Forms
{
    public abstract class MainNavigation
    {
        protected MainNavigation(Xamarin.Forms.Application application)
        {
            _application = application;
        }

        private readonly Xamarin.Forms.Application _application;

        protected INavigation<TScene> Show<TScene>(CrossFitContentPage<TScene> page) where TScene : Scene =>
            Navigation.For<TScene>(() => _application.MainPage = page);
    }
}
