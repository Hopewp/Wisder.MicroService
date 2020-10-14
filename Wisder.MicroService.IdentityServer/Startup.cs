using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Wisder.MicroService.IdentityServer.Validators;
using Wisder.MicroService.Core.Caches;
using Wisder.MicroService.Core.ServiceRegistration;
using System.Text;

namespace Wisder.MicroService.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
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
                connectString = Encoding.UTF8.GetString(consulClient.KV.Get("IdentityServerDB").Result.Response.Value);
                redisConnectString = Encoding.UTF8.GetString(consulClient.KV.Get("WisderMicroServiceRedis").Result.Response.Value);
            }
            #endregion

            services.AddSingleton<IFreeSql>((provider) =>
            {
                var fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.MySql, connectString).Build();
                return fsql;
            });

            services.AddDistributedRedisCache(redisConnectString);

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            services.AddIdentityServer(option =>
            {
                option.Caching.ClientStoreExpiration = TimeSpan.FromMinutes(5);
                option.Caching.ResourceStoreExpiration = TimeSpan.FromMinutes(5);
                option.Caching.CorsExpiration = TimeSpan.FromMinutes(5);
            })
                .AddSigningCredential(new X509Certificate2(Path.Combine(basePath,
                Configuration["Certificates:CerPath"]),
                Configuration["Certificates:Password"]))
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = dbBuilder =>
                    {
                        dbBuilder.UseMySQL(connectString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                }).AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = dbBuilder =>
                    {
                        dbBuilder.UseMySQL(connectString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                }).AddConfigurationStoreCache()
            .AddResourceOwnerValidator<UserPasswordLoginValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.RegisterConsul();
            app.UseIdentityServer();
        }
    }
}
