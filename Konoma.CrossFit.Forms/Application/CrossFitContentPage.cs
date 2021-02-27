using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public abstract class CrossFitContentPage<TScene> : ContentPage, ICrossFitPage<TScene>
        where TScene : Scene
    {
        protected CrossFitContentPage()
        {
            Scene = Konoma.CrossFit.Scene.Create<TScene>();
            BindingContext = Scene;
        }

        protected CrossFitContentPage(object args)
        {
            Scene = Konoma.CrossFit.Scene.Create<TScene>();
            Scene.SetArguments(args);
            BindingContext = Scene;
        }

        protected TScene Scene { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ConnectNavigationPoints();
        }

        protected abstract void ConnectNavigationPoints();

        Page ICrossFitPage<TScene>.AsFormsPage() => this;
    }
}
