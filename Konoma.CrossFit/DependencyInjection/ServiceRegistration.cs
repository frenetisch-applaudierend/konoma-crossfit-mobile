using System;
using Microsoft.Extensions.DependencyInjection;

namespace Konoma.CrossFit
{
    public interface IServiceRegistration
    {
        void AddSingleton<TService>()
            where TService : class;

        void AddSingleton<TService, TServiceImpl>()
            where TService : class
            where TServiceImpl : class, TService;

        void AddSingleton<TService>(Func<IServiceProvider, TService> builder)
            where TService : class;

        void AddTransient<TService>()
            where TService : class;

        void AddTransient<TService>(Func<IServiceProvider, TService> builder)
            where TService : class;

        void AddTransient<TService, TServiceImpl>()
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

        public void AddSingleton<TService>()
            where TService : class
            => _services.AddSingleton<TService>();

        public void AddSingleton<TService, TServiceImpl>()
            where TService : class
            where TServiceImpl : class, TService
            => _services.AddSingleton<TService, TServiceImpl>();

        public void AddSingleton<TService>(Func<IServiceProvider, TService> builder)
            where TService : class
            => _services.AddSingleton(builder);

        public void AddTransient<TService>()
            where TService : class
            => _services.AddTransient<TService>();

        public void AddTransient<TService>(Func<IServiceProvider, TService> builder)
            where TService : class
            => _services.AddTransient(builder);

        public void AddTransient<TService, TServiceImpl>()
            where TService : class
            where TServiceImpl : class, TService
            => _services.AddTransient<TService, TServiceImpl>();
    }
}
