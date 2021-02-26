using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public abstract class CrossFitTabbedPage<TScene> : TabbedPage, ICrossFitPage<TScene>
        where TScene : Scene
    {
        protected CrossFitTabbedPage()
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

        Page ICrossFitPage<TScene>.AsFormsPage() => this;
    }
}
