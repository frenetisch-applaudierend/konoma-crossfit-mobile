using UIKit;

namespace Konoma.CrossFit
{
    public interface ICrossFitViewController<TScene>
        where TScene : Scene
    {
        UIViewController AsViewController();
    }
}
