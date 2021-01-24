namespace Konoma.CrossFit
{
    public interface IServiceRegistration
    {
        void RegisterSingleton<TService>();
        void RegisterSingleton<TService, TServiceImpl>();

        void RegisterTransient<TService>();
        void RegisterTransient<TService, TServiceImpl>();
    }

    internal class DummyServiceRegistration : IServiceRegistration
    {
        public void RegisterSingleton<TService>()
        {
        }

        public void RegisterSingleton<TService, TServiceImpl>()
        {
        }

        public void RegisterTransient<TService>()
        {
        }

        public void RegisterTransient<TService, TServiceImpl>()
        {
        }
    }
}
