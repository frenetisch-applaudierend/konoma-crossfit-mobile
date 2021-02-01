using Konoma.CrossFit;

namespace TemperatureConverter.Core.Application.Login
{
    public class LoginScene : Scene
    {
        public LoginScene()
        {
            Username.SetValue("Username hier", notify: false);
        }

        public Property<string> Username => GetProperty("");

        public Property<string> Password => GetProperty("");
    }
}
