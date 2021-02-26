using Xamarin.Forms;

namespace Konoma.CrossFit
{
    public interface ICrossFitPage<TScene>
        where TScene : Scene
    {
        Page AsFormsPage();
    }
}
