using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Core.Pollys;

namespace Wisder.MicroService.Core.Middleware
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddMiddleware(this IServiceCollection services, Action<MiddlewareOptions> options = null)
        {
            MiddlewareOptions middlewareOptions = new MiddlewareOptions();
            options?.Invoke(middlewareOptions);

            // 1、注册到IOC
            services.Configure<MiddlewareOptions>(options);

            // 2、添加HttpClient
            // services.AddHttpClient(middlewareOptions.HttpClientName);
            services.AddPollyHttpClient(middlewareOptions.HttpClientName, middlewareOptions.pollyHttpClientOptions);

            // 3、注册中台
            services.AddSingleton<IMiddleService, HttpMiddleService>();

            return services;
        }
    }
}
