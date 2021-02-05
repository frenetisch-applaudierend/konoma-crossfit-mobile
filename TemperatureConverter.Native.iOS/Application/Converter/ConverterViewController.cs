using Konoma.CrossFit.iOS;
using TemperatureConverter.Core.Application.Converter;
using UIKit;

namespace TemperatureConverter.Native.iOS.Application.Converter
{
    public class ConverterViewController : UIViewController, ICrossFitViewController<ConverterScene>
    {
        public override void LoadView()
        {
            View = new UIView {BackgroundColor = UIColor.SystemTealColor};
        }

        UIViewController ICrossFitViewController<ConverterScene>.AsViewController() => this;
    }
}
