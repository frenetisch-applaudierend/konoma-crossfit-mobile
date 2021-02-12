using System;
using System.Linq.Expressions;
using System.Net.Mime;
using Android.App;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Konoma.CrossFit
{
    public static class ViewPropertyBindingBuilderExtensions
    {
        public static void To<T, TView, TEventHandler>(
            this PropertyBindingBuilder<T> builder,
            Activity activity,
            int targetId,
            Expression<Func<TView, T>> targetExpression,
            Func<TView, Action, TEventHandler> register,
            Action<TView, TEventHandler> unregister)
            where TView : View
        {
            var view = activity.FindViewById<TView>(targetId);
            if (view != null)
            {
                builder.To(view, targetExpression, register, unregister);
            }
        }

        public static void ToEditText(
            this PropertyBindingBuilder<string> builder,
            Activity activity,
            int targetId)
        {
            To<string, EditText, EventHandler<TextChangedEventArgs>>(
                builder,
                activity,
                targetId,
                v => v.Text!,
                (v, handler) =>
                {
                    EventHandler<TextChangedEventArgs> observer = delegate { handler(); };
                    v.TextChanged += observer;
                    return observer;
                },
                (v, observer) => v.TextChanged -= observer);
        }
    }
}
