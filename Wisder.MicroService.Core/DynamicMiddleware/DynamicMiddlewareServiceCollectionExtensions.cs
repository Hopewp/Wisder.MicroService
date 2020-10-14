using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Core.DynamicMiddleware.Urls;
using Wisder.MicroService.Core.Middleware;
using Wisder.MicroService.Core.ServiceDiscovery;

namespace Wisder.MicroService.Core.DynamicMiddleware
{
    public static class DynamicMiddlewareServiceCollectionExtensions
    {
        /// <summary>
        /// 添加动态中台
        /// </summary>
        /// <typeparam name="IMiddleService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services, Action<DynamicMiddlewareOptions> options = null)
            where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            DynamicMiddlewareOptions dynamicMiddlewareOptions = new DynamicMiddlewareOptions();
            options?.Invoke(dynamicMiddlewareOptions);

            services.AddServiceDiscovery(dynamicMiddlewareOptions.serviceDiscoveryOptions);

            services.AddMiddleware(dynamicMiddlewareOptions.middlewareOptions);

            services.AddSingleton<IDynamicMiddleUrl, DefaultDynamicMiddleUrl>();

            services.AddSingleton<TMiddleService, TMiddleImplementation>();
            return services;
        }
    }
}
