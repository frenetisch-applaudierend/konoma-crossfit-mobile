using Android.App;
using Android.Runtime;
using Android.Views;

namespace Konoma.CrossFit
{
    public static class ActivityExtensions
    {
        public static TView RequireViewById<TView>(this Activity activity, int resourceId)
            where TView : View =>
            activity.RequireViewById(resourceId).JavaCast<TView>();
    }
}
