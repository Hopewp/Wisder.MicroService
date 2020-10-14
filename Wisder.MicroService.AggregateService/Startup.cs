using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wisder.MicroService.Core.ServiceRegistration;
using Wisder.MicroService.Core.ServiceDiscovery;
using Wisder.MicroService.Core.MicroClients;
using Wisder.MicroService.Core.Filters;
using Newtonsoft.Json.Serialization;

namespace Wisder.MicroService.AggregateService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConsul();

            string connectString = "", redisConnectString = "";
            //var connectString = Configuration.GetConnectionString("IdentityServerDB");
            //var redisConnectString = Configuration.GetConnectionString("WisderMicroServiceRedis");
            #region 从配置中心获取配置参数
            var consulAddress = Environment.GetEnvironmentVariable("WisderRegistryAddress");
            //using (Consul.ConsulClient consulClient = new Consul.ConsulClient(config =>
            //{
            //    config.Address = new Uri(consulAddress);
            //}))
            //{
            //    connectString = Encoding.UTF8.GetString(consulClient.KV.Get("UserServiceDB").Result.Response.Value);
            //    redisConnectString = Encoding.UTF8.GetString(consulClient.KV.Get("WisderMicroServiceRedis").Result.Response.Value);
            //}
            #endregion

            services.AddMicroClient((options) =>
            {
                options.AssmelyName = "Wisder.MicroService.AggregateService";
                options.dynamicMiddlewareOptions = (dynamicMiddwareOptions) =>
                {
                    dynamicMiddwareOptions.middlewareOptions = (middwareOptions) =>
                    {
                        //middwareOptions.HttpClientName = "AggregateService";
                    };
                    dynamicMiddwareOptions.serviceDiscoveryOptions = (discoveryOptions) =>
                    {
                        discoveryOptions.DiscoveryServerAddress = new Uri(consulAddress);
                        discoveryOptions.typeLoadBalancer = Core.ServiceDiscovery.LoadBalancer.TypeLoadBalancer.Random;
                    };
                };
            });
            services.AddControllers(option =>
            {
                option.Filters.Add<BizExceptionFilter>();
                option.Filters.Add<FrontResultFilter>();
            }).AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.RegisterConsul();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
