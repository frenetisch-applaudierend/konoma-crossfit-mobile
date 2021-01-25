using System;

namespace Konoma.CrossFit.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static TService GetRequiredService<TService>(this IServiceProvider services)
        {
            var service = services.GetService(typeof(TService));
            if (service is null)
                throw new InvalidOperationException($"Required service {typeof(TService)} not found");

            return (TService)service;
        }
    }
}
