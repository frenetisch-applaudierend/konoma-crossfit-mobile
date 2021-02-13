using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;

namespace Konoma.CrossFit
{
    public abstract class CrossFitActivity<TScene> : AppCompatActivity
        where TScene : Scene
    {
#pragma warning disable 8618
        protected CrossFitActivity(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
#pragma warning restore 8618

        public CrossFitActivity()
        {
            Scene = CrossFit.Scene.Create<TScene>();
        }

        public CrossFitActivity(int contentLayoutId) : base(contentLayoutId)
        {
            Scene = CrossFit.Scene.Create<TScene>();
        }

        #region Cross Fit Support

        protected TScene Scene { get; }

        protected BindingRegistry BindingRegistry { get; } = new BindingRegistry();
        protected MenuItemCommandRegistry MenuItemCommandRegistry { get; } = new MenuItemCommandRegistry();

        #endregion

        #region Lifecycle

        protected override void OnPostCreate(Bundle? savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            ConnectNavigationPoints();
        }

        protected override void OnStart()
        {
            base.OnStart();

            ArrangeBindings(new AndroidBinder<TScene>(BindingRegistry, MenuItemCommandRegistry, Scene));
        }

        protected override void OnStop()
        {
            base.OnStop();

            BindingRegistry.ClearAndDisposeBindings();
            MenuItemCommandRegistry.ClearHandlers();
        }

        public override bool OnPrepareOptionsMenu(IMenu? menu)
        {
            MenuItemCommandRegistry.PrepareOptionsMenu(menu!);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item) =>
            MenuItemCommandRegistry.HandleItemSelected(item)
            || base.OnOptionsItemSelected(item);

        protected abstract void ConnectNavigationPoints();

        protected abstract void ArrangeBindings(Binder<TScene> binder);

        #endregion

        #region Convenience

        protected TView RequireViewById<TView>(int resourceId) where TView : View =>
            ActivityExtensions.RequireViewById<TView>(this, resourceId);

        #endregion
    }
}
