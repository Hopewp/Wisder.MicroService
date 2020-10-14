using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Core.Exceptions;
using Wisder.MicroService.Core.ServiceDiscovery;

namespace Wisder.MicroService.Core.DynamicMiddleware.Urls
{
    public class DefaultDynamicMiddleUrl : IDynamicMiddleUrl
    {
        private readonly IServiceDiscovery serviceDiscovery;

        public DefaultDynamicMiddleUrl(IServiceDiscovery serviceDiscovery)
        {
            this.serviceDiscovery = serviceDiscovery;
        }

        public string GetMiddleUrl(string urlShcme, string serviceName)
        {
            ServiceNode serviceUrl = serviceDiscovery.GetServiceAsync(serviceName).Result;
            if (serviceUrl == null)
            {
                throw new FrameworkException($"{serviceName} 服务不存在");
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(urlShcme);
            stringBuilder.Append("://");
            stringBuilder.Append(serviceUrl.Url);
            return stringBuilder.ToString();
        }
    }
}
