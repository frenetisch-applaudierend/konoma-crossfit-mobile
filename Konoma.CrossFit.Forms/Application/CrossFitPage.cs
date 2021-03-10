using Xamarin.Forms;

namespace Konoma.CrossFit
{
    // ReSharper disable once UnusedTypeParameter
    public interface ICrossFitPage<TScene>
        where TScene : Scene
    {
        Page AsFormsPage();
    }
}
