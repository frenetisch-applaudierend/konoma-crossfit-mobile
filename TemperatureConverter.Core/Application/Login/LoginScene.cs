using Konoma.CrossFit;

namespace TemperatureConverter.Core.Application.Login
{
    public class LoginScene : Scene<LoginViewModel>
    {
        public LoginScene() : base(new LoginViewModel())
        {
            ViewModel.Username = "Username hier";
        }
    }
}
