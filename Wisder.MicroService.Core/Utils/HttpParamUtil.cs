using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Wisder.MicroService.Core.Utils
{
    public class HttpParamUtil
    {
        public static string DicToHttpUrlParam(IDictionary<string, object> middleParam)
        {
            if (middleParam == null) return "";
            var validparams = middleParam.Where(p => !string.IsNullOrEmpty(p.Key) && p.Value != null).Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value.ToString(), Encoding.UTF8)}").ToArray();
            if (validparams.Length > 0)
            {
                string urlParam = $"?{string.Join("&", validparams)}";
                return urlParam;
            }
            return "";
        }
    }
}
