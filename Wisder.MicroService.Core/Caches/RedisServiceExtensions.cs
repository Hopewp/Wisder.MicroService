using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.Caches
{
    public static class RedisServiceExtensions
    {
        /// <summary>
        /// 注册Redis访问实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services, string connectionString)
        {
            var csredis = new CSRedisClient(connectionString);
            services.AddSingleton(csredis);
            RedisHelper.Initialization(csredis);
            return services;
        }
        /// <summary>
        /// 注册Redis集群，哨兵模式，非官方集群
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services, string[] connectionString)
        {
            var csredis = new CSRedisClient(null, connectionString);
            services.AddSingleton(csredis);
            RedisHelper.Initialization(csredis);
            return services;
        }
    }
}
