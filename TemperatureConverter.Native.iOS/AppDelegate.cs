﻿using Foundation;
using UIKit;

namespace TemperatureConverter.Native.iOS
{
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.RootViewController = new UIViewController();

            Window.MakeKeyAndVisible();

            return true;
        }
    }
}
