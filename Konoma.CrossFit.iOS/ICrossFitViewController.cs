using UIKit;

namespace Konoma.CrossFit.iOS
{
    public interface ICrossFitViewController<TScene>
        where TScene : Scene
    {
        UIViewController AsViewController();
    }
}
