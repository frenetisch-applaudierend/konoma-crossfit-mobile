using TemperatureConverter.Native.iOS.Application;
using UIKit;

namespace TemperatureConverter.Native.iOS
{
    public static class Program
    {
        static void Main(string[] args) => UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
