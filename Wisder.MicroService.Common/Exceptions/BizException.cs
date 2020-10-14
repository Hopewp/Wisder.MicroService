using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Common.Exceptions
{
    public class BizException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public IDictionary<string, object> Infos { set; get; }

        public BizException(string errorCode, string errorMsg) : base(errorMsg)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }

        public BizException(string errorCode, string errorMsg, Exception e) : base(errorMsg, e)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }

        public BizException(string errorMsg) : this("-1", errorMsg)
        {
        }

        public BizException(string errorMsg, Exception e) : this("-1", errorMsg, e)
        {
        }

        public BizException(Exception e) : this("-1", e.Message, e)
        {
        }
    }
}
