using Xamarin.Forms;

namespace Konoma.CrossFit.Forms
{
    public abstract class CrossFitContentPage<TScene> : ContentPage
        where TScene : Scene
    {
        protected CrossFitContentPage()
        {
            Scene = Konoma.CrossFit.Scene.Create<TScene>();
        }

        protected TScene Scene { get; }
    }

    public abstract class CrossFitContentPage<TScene, TViewModel> : CrossFitContentPage<TScene>
        where TScene : Scene<TViewModel>
    {
        protected CrossFitContentPage()
        {
            BindingContext = ViewModel;
        }

        protected TViewModel ViewModel => Scene.ViewModel;
    }
}
