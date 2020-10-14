using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Core.DynamicMiddleware;

namespace Wisder.MicroService.Core.MicroClients
{
    public static class MicroClientServiceCollectionExtensions
    {
        /// <summary>
        /// 添加中台
        /// </summary>
        /// <typeparam name="IMiddleService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroClient(this IServiceCollection services, string assmelyName)
        {
            // 1、注册动态中台
            services.AddDynamicMiddleware<IDynamicMiddleService, DefaultDynamicMiddleService>();

            // 2、注册客户端工厂
            services.AddSingleton<MicroClientProxyFactory>();

            // 3、注册客户端
            services.AddSingleton<MicroClientList>();

            // 4、注册MicroClient代理对象
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            MicroClientList microClients = serviceProvider.GetRequiredService<MicroClientList>();

            IDictionary<Type, object> dics = microClients.GetClients(assmelyName);
            foreach (var key in dics.Keys)
            {
                services.AddSingleton(key, dics[key]);
            }
            return services;
        }


        /// <summary>
        /// 添加中台
        /// </summary>
        /// <typeparam name="IMiddleService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroClient(this IServiceCollection services, Action<MicroClientOptions> options)
        {
            MicroClientOptions microClientOptions = new MicroClientOptions();
            options(microClientOptions);

            // 1、注册AddMiddleware
            services.AddDynamicMiddleware<IDynamicMiddleService, DefaultDynamicMiddleService>(microClientOptions.dynamicMiddlewareOptions);

            // 2、注册客户端工厂
            services.AddSingleton<MicroClientProxyFactory>();

            // 3、注册客户端集合
            services.AddSingleton<MicroClientList>();

            // 4、注册代理对象
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            MicroClientList microClients = serviceProvider.GetRequiredService<MicroClientList>();

            IDictionary<Type, object> dics = microClients.GetClients(microClientOptions.AssmelyName);
            foreach (var key in dics.Keys)
            {
                services.AddSingleton(key, dics[key]);
            }
            return services;
        }
    }
}
