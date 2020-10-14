using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.ServiceDiscovery.LoadBalancer
{
    public interface ILoadBalancer
    {
        ServiceNode Resolve(string serviceName, IList<ServiceNode> serviceList);
    }

    public enum TypeLoadBalancer
    {
        Random,
        RoundRobin
    }
}
