using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.ServiceRegistration
{
    public class ConsulServiceOptions
    {
        ///// <summary>
        ///// Consul服务地址
        ///// </summary>
        //public string RegistryAddress { get; set; }
        /// <summary>
        /// 微服务名称
        /// </summary>
        public string ServiceName { get; set; }
        // 服务标签(版本)
        public string[] ServiceTags { set; get; }
        /// <summary>
        /// 微服务通信协议
        /// </summary>
        public string ServiceScheme { get; set; }
        ///// <summary>
        ///// 微服务主机地址
        ///// </summary>
        //public string ServiceAddress { get; set; }
        /// <summary>
        /// 微服务端口号
        /// </summary>
        public int ServicePort { get; set; }
        /// <summary>
        /// 健康检查接口路径
        /// </summary>
        public string HealthCheck { get; set; }
    }
}
