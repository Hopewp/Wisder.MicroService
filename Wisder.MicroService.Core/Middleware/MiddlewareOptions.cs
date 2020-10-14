using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Core.Pollys;

namespace Wisder.MicroService.Core.Middleware
{
    public class MiddlewareOptions
    {
        public string HttpClientName { set; get; } = "wisderpolly";
        /// <summary>
        /// polly熔断降级选项
        /// </summary>
        public Action<PollyHttpClientOptions> pollyHttpClientOptions { get; }

    }
}
