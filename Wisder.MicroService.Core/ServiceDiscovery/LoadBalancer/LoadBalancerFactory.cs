using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.ServiceDiscovery.LoadBalancer
{
    public class LoadBalancerFactory
    {
        public static ILoadBalancer CreateInstance(TypeLoadBalancer typeLoadBalancer)
        {
            switch (typeLoadBalancer)
            {
                case TypeLoadBalancer.Random:
                    return new RandomLoadBalancer();
                default:
                    return new RoundRobinLoadBalancer();
            }
        }
    }
}
