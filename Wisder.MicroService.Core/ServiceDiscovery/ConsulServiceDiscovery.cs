using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wisder.MicroService.Core.ServiceDiscovery.LoadBalancer;
using Wisder.MicroService.Core.ServiceDiscovery.ServiceProvider;

namespace Wisder.MicroService.Core.ServiceDiscovery
{
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private ConsulServiceProvider serviceProvider;
        private ServiceDiscoveryOptions serviceDiscoveryOptions;
        public ConsulServiceDiscovery(ServiceDiscoveryOptions options)
        {
            serviceDiscoveryOptions = options;
            serviceProvider = new ConsulServiceProvider(options.DiscoveryServerAddress); ;
        }
        public async Task<ServiceNode> GetServiceAsync(string serviceName)
        {
            var serviceList = await serviceProvider.GetServiceAsync(serviceName);
            if (serviceList == null || serviceList.Count <= 0)
                return null;
            ServiceNode serviceNode;
            if (serviceList.Count == 1)
            {
                serviceNode = serviceList[0];
            }
            else
            {
                serviceNode = LoadBalancerFactory.CreateInstance(serviceDiscoveryOptions.typeLoadBalancer).Resolve(serviceName, serviceList);
            }
            return serviceNode;
        }
    }
}
