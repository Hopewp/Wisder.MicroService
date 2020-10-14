using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.Exceptions
{
    public class FrameworkException : Exception
    {
        public string ErrorCode { get; } // 业务异常编号
        public string ErrorMsg { get; }// 业务异常信息

        public IDictionary<string, object> Infos { set; get; } // 业务异常详细信息

        public FrameworkException(string errorCode, string errorMsg) : base(errorMsg)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }

        public FrameworkException(string errorCode, string errorMsg, Exception e) : base(errorMsg, e)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }

        public FrameworkException(string errorMsg) : base(errorMsg)
        {
            ErrorCode = "-1";
            ErrorMsg = errorMsg;
        }

        public FrameworkException(string errorMsg, Exception e) : base(errorMsg, e)
        {
            ErrorCode = "-1";
            ErrorMsg = errorMsg;
        }

        public FrameworkException(Exception e)
        {
            ErrorCode = "-1";
            ErrorMsg = e.Message;
        }
    }
}
