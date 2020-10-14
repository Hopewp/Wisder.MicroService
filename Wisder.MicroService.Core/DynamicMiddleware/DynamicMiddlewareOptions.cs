using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Core.Middleware;
using Wisder.MicroService.Core.ServiceDiscovery;

namespace Wisder.MicroService.Core.DynamicMiddleware
{
    public class DynamicMiddlewareOptions
    {
        /// <summary>
        /// 服务发现选项
        /// </summary>
        public Action<ServiceDiscoveryOptions> serviceDiscoveryOptions { set; get; }

        // 中台选项
        public Action<MiddlewareOptions> middlewareOptions { set; get; }
    }
}
