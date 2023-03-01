global using Resp = System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<CUGOJ.Frontend.Controllers.ControllerBase.ApiResponse>>;
using CUGOJ.Share.Defines;
using CUGOJ.Tools.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Frontend.Controllers
{
    public class ControllerBase:Microsoft.AspNetCore.Mvc.ControllerBase
    {

        public class ApiResponse
        {
            public ErrorCodes Code { get; set; }
            public object? Content { get; set; }
            public long ErrorCode { get; set; }
        }
        
        protected static ActionResult<ApiResponse> Code(ErrorCodes code)
        {
            return new ApiResponse
            {
                Code = code
            };
        }
        protected static ActionResult<ApiResponse> Content(object? content, ErrorCodes code = ErrorCodes.Success)
        {
            return new ApiResponse
            {
                Code = code,
                Content = content
            };
        }

        protected static ActionResult<ApiResponse> Exception(Exception ex,ErrorCodes code = ErrorCodes.SystemError)
        {
            if (ex is CUGOJException @e)
            {
                return new ApiResponse { Code = code, Content = e.Message, ErrorCode = e.ErrorCode };
            }
            else
            {
                return new ApiResponse { Code = code, Content = ex.Message, ErrorCode = -1 };
            }
        }

    }
}
