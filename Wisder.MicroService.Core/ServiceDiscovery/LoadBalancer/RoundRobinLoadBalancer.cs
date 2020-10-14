using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.ServiceDiscovery.LoadBalancer
{
    public class RoundRobinLoadBalancer : ILoadBalancer
    {
        private static readonly Dictionary<string, int> _dicIndex = new Dictionary<string, int>();
        private static readonly object _locker = new object();
        public ServiceNode Resolve(string serviceName, IList<ServiceNode> serviceList)
        {
            lock (_locker)
            {
                if (!_dicIndex.ContainsKey(serviceName))
                {
                    _dicIndex.Add(serviceName, 0);
                }
                var index = _dicIndex[serviceName];
                if (index >= serviceList.Count) index = 0;
                _dicIndex[serviceName] = index + 1;
                return serviceList[index];
            }
        }
    }
}
