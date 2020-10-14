using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wisder.MicroService.Core.ServiceDiscovery.ServiceProvider
{
    public class ConsulServiceProvider : IServiceProvider, IDisposable
    {
        private readonly ConsulClient _client;
        private readonly Dictionary<string, List<ServiceNode>> cacheServiceNode = new Dictionary<string, List<ServiceNode>>();
        private readonly Dictionary<string, DateTime> cacheServiceNodeTime = new Dictionary<string, DateTime>();
        private readonly Timer timer;
        public ConsulServiceProvider(Uri consulServerUri)
        {
            _client = new ConsulClient(config =>
            {
                config.Address = consulServerUri;
            });
            timer = new Timer(async (obj) =>
            {
                lock (cacheServiceNode)
                {
                    var keys = cacheServiceNode.Keys.ToList();
                    foreach (var serviceName in keys)
                    {
                        GetServiceTask(serviceName).Wait();
                    }
                }
            }, null, 200, 200);
        }

        public void Dispose()
        {
            cacheServiceNode.Clear();
            cacheServiceNodeTime.Clear();
            timer?.Dispose();
            _client?.Dispose();
        }

        public async Task<List<ServiceNode>> GetServiceAsync(string serviceName)
        {
            //await Task.Run(() =>
            //{
            if (!cacheServiceNode.ContainsKey(serviceName))
            {
                lock (cacheServiceNode)
                {
                    if (!cacheServiceNode.ContainsKey(serviceName))
                    {
                        GetServiceTask(serviceName).Wait();
                    }
                }
            }
            //});
            return cacheServiceNode[serviceName];
        }

        private async Task GetServiceTask(string serviceName)
        {
            var serviceRes = await _client.Health.Service(serviceName, "", true);
            var serviceList = serviceRes.Response.Select(p => new ServiceNode() { Url = $"{p.Service.Address}:{p.Service.Port}" }).ToList();
            cacheServiceNode[serviceName] = serviceList;
            cacheServiceNodeTime[serviceName] = DateTime.Now;
        }
    }
}
