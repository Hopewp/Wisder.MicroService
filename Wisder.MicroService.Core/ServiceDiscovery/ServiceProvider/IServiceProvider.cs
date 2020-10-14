using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wisder.MicroService.Core.ServiceDiscovery.ServiceProvider
{
    public interface IServiceProvider
    {
        Task<List<ServiceNode>> GetServiceAsync(string serviceName);
    }
}
