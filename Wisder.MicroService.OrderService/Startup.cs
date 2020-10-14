using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wisder.MicroService.Common.Entity;
using Wisder.MicroService.OrderService.Repositories;
using Wisder.MicroService.OrderService.Services;
using Wisder.MicroService.Core.Caches;
using Wisder.MicroService.Core.ServiceRegistration;

namespace Wisder.MicroService.OrderService
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
            var dataCenterId = Configuration.GetValue<long>("DataCenterId");
            var serverId = Configuration.GetValue<long>("ServerId");
            services.AddSingleton<IdBuilder>(p => new IdBuilder(dataCenterId, serverId));

            services.AddSingleton<IFreeSql>((provider) =>
            {
                var connectString = Configuration.GetConnectionString("OrderServiceDB");
                var fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.MySql, connectString).Build();
                return fsql;
            });
            var redisConnectString = Configuration.GetConnectionString("WisderMicroServiceRedis");
            services.AddDistributedRedisCache(redisConnectString);

            services.AddScoped<UnitOfWorkManager>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderServiceImpl>();

            services.AddConsul();

            services.AddControllers();
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
