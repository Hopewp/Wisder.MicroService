using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.Pollys
{
    public class PollyHttpClientOptions
    {
        /// <summary>
        /// 超时时间设置，单位为秒
        /// </summary>
        public int TimeoutTime { set; get; } = 20;

        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int RetryCount { set; get; } = 3;

        /// <summary>
        /// 执行多少次异常，开启短路器
        /// </summary>
        public int CircuitBreakerOpenFallCount { set; get; } = 3;

        /// <summary>
        /// 断路器开启的时间,秒
        /// </summary>
        public int CircuitBreakerDownTime { set; get; } = 3;

        /// <summary>
        /// 降级处理
        /// </summary>
        public string ResponseMessage { set; get; } = "系统繁忙，请重试";
    }
}
