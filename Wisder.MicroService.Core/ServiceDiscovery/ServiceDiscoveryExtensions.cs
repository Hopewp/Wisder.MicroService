using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, Action<ServiceDiscoveryOptions> config = null)
        {
            ServiceDiscoveryOptions options = new ServiceDiscoveryOptions();
            config?.Invoke(options);
            return services.AddSingleton<IServiceDiscovery>(new ConsulServiceDiscovery(options));
        }
    }
}
