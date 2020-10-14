using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wisder.MicroService.Core.ServiceDiscovery
{
    public interface IServiceDiscovery
    {
        Task<ServiceNode> GetServiceAsync(string serviceName);
    }
}
