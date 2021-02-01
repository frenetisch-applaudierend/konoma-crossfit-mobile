using Xamarin.Forms;

namespace Konoma.CrossFit.Forms
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
    }
}
