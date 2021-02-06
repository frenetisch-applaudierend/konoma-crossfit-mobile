using System;
using Foundation;
using UIKit;

namespace Konoma.CrossFit
{
    public abstract class CrossFitViewController<TScene> : UIViewController, ICrossFitViewController<TScene>
        where TScene : Scene
    {
        #region Initialization

        protected CrossFitViewController()
        {
            Scene = Konoma.CrossFit.Scene.Create<TScene>();
            Binder = new Binder();
        }

        protected CrossFitViewController(string? nibName, NSBundle? bundle) : base(nibName, bundle)
        {
            Scene = Konoma.CrossFit.Scene.Create<TScene>();
            Binder = new Binder();
        }

#pragma warning disable 8618
        protected CrossFitViewController(IntPtr handle) : base(handle) { }
#pragma warning restore 8618

        #endregion

        #region Cross Fit Support

        protected TScene Scene { get; }

        protected Binder Binder { get; }

        UIViewController ICrossFitViewController<TScene>.AsViewController() => this;

        #endregion
    }
}
