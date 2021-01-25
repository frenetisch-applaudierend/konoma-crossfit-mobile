using System;
using Microsoft.Extensions.DependencyInjection;

namespace Konoma.CrossFit
{
    public interface IServiceRegistration
    {
        void RegisterSingleton<TService>()
            where TService : class;

        void RegisterSingleton<TService, TServiceImpl>()
            where TService : class
            where TServiceImpl : class, TService;

        void RegisterTransient<TService>()
            where TService : class;

        void RegisterTransient<TService, TServiceImpl>()
            where TService : class
            where TServiceImpl : class, TService;
    }

    internal class ServiceRegistration : IServiceRegistration
    {
        public ServiceRegistration(IServiceCollection services)
        {
            _services = services;
        }

        private readonly IServiceCollection _services;

        public void RegisterSingleton<TService>()
            where TService : class
            => _services.AddSingleton<TService>();

        public void RegisterSingleton<TService, TServiceImpl>()
            where TService : class
            where TServiceImpl : class, TService
            => _services.AddSingleton<TService, TServiceImpl>();

        public void RegisterTransient<TService>()
            where TService : class
            => _services.AddTransient<TService>();

        public void RegisterTransient<TService, TServiceImpl>()
            where TService : class
            where TServiceImpl : class, TService
            => _services.AddTransient<TService, TServiceImpl>();
    }
}
