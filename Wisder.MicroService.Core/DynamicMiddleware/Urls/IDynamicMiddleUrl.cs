using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.DynamicMiddleware.Urls
{
    public interface IDynamicMiddleUrl
    {
        /// <summary>
        ///  获取中台Url
        /// </summary>
        /// <param name="urlShcme">中台url</param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public string GetMiddleUrl(string urlShcme, string serviceName);
    }
}
