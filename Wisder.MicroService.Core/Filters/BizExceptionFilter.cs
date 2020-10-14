using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Wisder.MicroService.Common.Exceptions;

namespace Wisder.MicroService.Core.Filters
{
    public class BizExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BizException bizException)
            {
                // 将异常转换成为json结果
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorCode = bizException.ErrorCode;
                exceptionResult.ErrorMsg = bizException.ErrorMsg;
                if (bizException.Infos != null)
                {
                    exceptionResult.Infos = bizException.Infos;
                }
                context.Result = new JsonResult(exceptionResult);
            }
            else
            {
                // 处理其他类型异常Exception
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorCode = -1;
                exceptionResult.ErrorMsg = context.Exception.Message;

                // 包装异常信息进行异常返回
                context.Result = new JsonResult(exceptionResult);
            }
        }
    }
}
