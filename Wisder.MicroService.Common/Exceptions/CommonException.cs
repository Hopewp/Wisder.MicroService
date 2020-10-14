using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Common.Exceptions
{
    public class CommonException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

        public CommonException(string errorCode, string errorMsg) : base(errorMsg)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }

        public CommonException(string errorCode, string errorMsg, Exception e) : base(errorMsg, e)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }

        public CommonException(string errorMsg) : this("-1", errorMsg)
        {
        }

        public CommonException(string errorMsg, Exception e) : this("-1", errorMsg, e)
        {
        }

        public CommonException(Exception e) : this("-1", e.Message, e)
        {
        }
    }
}
