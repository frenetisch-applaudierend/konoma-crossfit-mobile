using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public abstract class CrossFitContentPage<TScene> : ContentPage
        where TScene : Scene
    {
        protected CrossFitContentPage()
        {
            Scene = Konoma.CrossFit.Scene.Create<TScene>();
            BindingContext = Scene;
        }

        protected TScene Scene { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ConnectNavigationPoints();
        }

        protected abstract void ConnectNavigationPoints();
    }
}
