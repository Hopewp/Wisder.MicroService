using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.ServiceDiscovery.LoadBalancer
{
    public class RandomLoadBalancer : ILoadBalancer
    {
        private static readonly Dictionary<string, Random> _dicRandom = new Dictionary<string, Random>();
        public ServiceNode Resolve(string serviceName, IList<ServiceNode> serviceList)
        {
            Random random;
            if (_dicRandom.ContainsKey(serviceName))
            {
                random = _dicRandom[serviceName];
            }
            else
            {
                random = new Random();
                _dicRandom.Add(serviceName, random);
            }
            var index = random.Next(serviceList.Count);
            return serviceList[index];
        }
    }
}
