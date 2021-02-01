using Konoma.CrossFit;

namespace TemperatureConverter.Core.Application.Login
{
    public class LoginScene : Scene
    {
        public string UsernameLabel { get; } = "Username";

        public string PasswordLabel { get; } = "Password";

        public Property<string> Username => GetProperty("");

        public Property<string> Password => GetProperty("");
    }
}
