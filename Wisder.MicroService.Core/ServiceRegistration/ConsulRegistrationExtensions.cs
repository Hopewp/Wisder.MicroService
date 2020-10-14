using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.ServiceRegistration
{
    public static class ConsulRegistrationExtensions
    {
        public static void AddConsul(this IServiceCollection services)
        {
            services.AddHealthChecks();
            var consulConfig = new ConfigurationBuilder().AddJsonFile("consulsettings.json").Build();
            services.Configure<ConsulServiceOptions>(consulConfig);
        }

        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app)
        {
            var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulServiceOptions>>().Value;
            var consulAddress = Environment.GetEnvironmentVariable("WisderRegistryAddress");
            var serviceAddress = Environment.GetEnvironmentVariable("WisderServiceAddress");
            //string consulAddress = serviceOptions.RegistryAddress;
            //string serviceAddress = serviceOptions.ServiceAddress;
            string serviceId = $"{serviceOptions.ServiceName}_{Guid.NewGuid():n}";

            using (Consul.ConsulClient consulClient = new Consul.ConsulClient(config =>
            {
                config.Address = new Uri(consulAddress);
            }))
            {
                //自动获取当前服务地址
                //var features = app.Properties["server.Features"] as FeatureCollection;
                //var address = features.Get<IServerAddressesFeature>().Addresses.First();
                //var serviceUri = new Uri(address);

                if (string.IsNullOrWhiteSpace(serviceOptions.HealthCheck))
                {
                    serviceOptions.HealthCheck = "/healthy";
                }
                app.UseHealthChecks(serviceOptions.HealthCheck);

                var registration = new AgentServiceRegistration()
                {
                    ID = serviceId,
                    Name = serviceOptions.ServiceName,
                    Address = serviceAddress,// serviceOptions.ServiceAddress,
                    Port = serviceOptions.ServicePort,
                    Tags = serviceOptions.ServiceTags,
                    Check = new AgentServiceCheck()
                    {
                        //注册超时
                        Timeout = TimeSpan.FromSeconds(5),
                        //服务停止多久后注销服务
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                        //健康检查接口地址
                        HTTP = $"{serviceOptions.ServiceScheme}://{serviceAddress}:{serviceOptions.ServicePort}{serviceOptions.HealthCheck}",
                        //健康检查
                        Interval = TimeSpan.FromSeconds(5)
                    }
                };
                consulClient.Agent.ServiceRegister(registration).Wait();
            }

            lifeTime.ApplicationStopping.Register(() =>
            {
                using (Consul.ConsulClient consulClientDeregister = new Consul.ConsulClient(config =>
                {
                    config.Address = new Uri(consulAddress);
                }))
                {
                    consulClientDeregister.Agent.ServiceDeregister(serviceId).Wait();
                }
            });
            return app;
        }
    }
}
