using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Core.ServiceDiscovery.LoadBalancer;

namespace Wisder.MicroService.Core.ServiceDiscovery
{
    public class ServiceDiscoveryOptions
    {
        public Uri DiscoveryServerAddress { get; set; }
        public TypeLoadBalancer typeLoadBalancer { get; set; } = TypeLoadBalancer.RoundRobin;
    }
}
