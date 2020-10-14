using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Wisder.MicroService.Common.Entity;
using Wisder.MicroService.Core.Caches;
using Wisder.MicroService.Core.Filters;
using Wisder.MicroService.Core.ServiceRegistration;
using Wisder.MicroService.UserService.Repositories;
using Wisder.MicroService.UserService.Services;

namespace Wisder.MicroService.UserService
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
            using (Consul.ConsulClient consulClient = new Consul.ConsulClient(config =>
            {
                config.Address = new Uri(consulAddress);
            }))
            {
                connectString = Encoding.UTF8.GetString(consulClient.KV.Get("UserServiceDB").Result.Response.Value);
                redisConnectString = Encoding.UTF8.GetString(consulClient.KV.Get("WisderMicroServiceRedis").Result.Response.Value);
            }
            #endregion

            var dataCenterId = Configuration.GetValue<long>("DataCenterId");
            var serverId = Configuration.GetValue<long>("ServerId");
            services.AddSingleton<IdBuilder>(p => new IdBuilder(dataCenterId, serverId));

            services.AddSingleton<IFreeSql>((provider) =>
            {
                var fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.MySql, connectString).Build();
                return fsql;
            });
            services.AddDistributedRedisCache(redisConnectString);

            services.AddScoped<UnitOfWorkManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserServiceImpl>();

            services.AddControllers(option =>
            {
                option.Filters.Add<BizExceptionFilter>();
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
