using Konoma.CrossFit.iOS;
using TemperatureConverter.Core.Application.Login;
using UIKit;

namespace TemperatureConverter.Native.iOS.Application.Login
{
    public class LoginViewController : UIViewController, ICrossFitViewController<LoginScene>
    {
        public override void LoadView()
        {
            View = new UIView {BackgroundColor = UIColor.SystemIndigoColor};
        }

        UIViewController ICrossFitViewController<LoginScene>.AsViewController() => this;
    }
}
