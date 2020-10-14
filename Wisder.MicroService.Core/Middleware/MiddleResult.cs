using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.Middleware
{
    public class MiddleResult
    {
        public const string SUCCESS = "0";
        public string ErrorCode { set; get; } // 是否成功状态
        public string ErrorMessage { set; get; } // 失败信息
        public IDictionary<string, object> resultDic { set; get; }// 用于非结果集返回
        public IList<IDictionary<string, object>> resultList { set; get; }// 用于结果集返回

        public dynamic Result { set; get; }// 返回动态结果(通用)

        public MiddleResult()
        {
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }
        /// <summary>
        /// 中台结果串转换成为MiddleResult
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static MiddleResult JsonToMiddleResult(string jsonStr)
        {
            MiddleResult result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);
            return result;
        }

        public MiddleResult(string errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public MiddleResult(string errorCode, string errorMessage, IDictionary<string, object> resultDic, IList<IDictionary<string, object>> resultList) : this(errorCode, errorMessage)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
            this.resultDic = resultDic;
            this.resultList = resultList;
        }
    }
}
