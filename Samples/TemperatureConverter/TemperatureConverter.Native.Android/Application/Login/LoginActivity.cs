using Android.App;
using Android.OS;
using Android.Widget;
using Konoma.CrossFit;
using TemperatureConverter.Core.Application.Login;
using Support = AndroidX.AppCompat.Widget;

namespace TemperatureConverter.Android.Application.Login
{
    [Activity]
    public class LoginActivity : CrossFitActivity<LoginScene>
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_login);
            SetSupportActionBar(FindViewById<Support.Toolbar>(Resource.Id.toolbar));

            Title = Scene.ScreenTitle;

            RequireViewById<TextView>(Resource.Id.username_label).Text = Scene.UsernameLabel;
            RequireViewById<TextView>(Resource.Id.password_label).Text = Scene.PasswordLabel;
            RequireViewById<Button>(Resource.Id.signin_button).Text = Scene.LoginButtonTitle;
        }

        protected override void ConnectNavigationPoints()
        {
            // TODO: Add me
        }

        protected override void ArrangeBindings(Binder<LoginScene> binder)
        {
            binder.Bind(scene => scene.Username.Editable).ToEditText(this, Resource.Id.username_input);
            binder.Bind(scene => scene.Password.Editable).ToEditText(this, Resource.Id.password_input);

            #error Fix bindings for EditText, because currently every input will reset the cursor to the beginning
            #error Possible fix: similar to commands, add an IBindingTarget interface that will include getters and
            #error setters, then override the setter for EditText to check for the current value before overriding.

            binder.Bind(Scene.SignInCommand).ToButton(this, Resource.Id.signin_button);
        }
    }
}
