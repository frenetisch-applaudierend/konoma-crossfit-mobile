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
        }

        protected CrossFitViewController(string? nibName, NSBundle? bundle) : base(nibName, bundle)
        {
            Scene = Konoma.CrossFit.Scene.Create<TScene>();
        }

#pragma warning disable 8618
        protected CrossFitViewController(IntPtr handle) : base(handle) { }
#pragma warning restore 8618

        #endregion

        #region Cross Fit Support

        protected TScene Scene { get; }

        protected BindingRegistry BindingRegistry { get; } = new BindingRegistry();

        UIViewController ICrossFitViewController<TScene>.AsViewController() => this;

        #endregion

        #region Lifecycle

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var binder = new Binder<TScene>(BindingRegistry, Scene);
            ArrangeBindings(binder);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            BindingRegistry.ClearAndDisposeBindings();
        }

        protected abstract void ArrangeBindings(Binder<TScene> binder);

        #endregion
    }
}
