using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUIAdmin.Extensions;

namespace WebUIAdmin.Models.Services
{
    /// <summary>
    /// 按钮权限检查过滤器
    /// </summary>
    public class ButtonAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 按钮授权所属菜单
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 按钮授权标识码
        /// </summary>
        public string Auth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var buttonAuthorizateSvr = context.HttpContext.RequestServices.GetRequiredService<IMyButtonAuthorization>();
            Auth = CtrlRealAction.Instance.CtrlAuth(Auth, context.HttpContext);
            if (buttonAuthorizateSvr == null || !buttonAuthorizateSvr.Authorizate(context.HttpContext, Url, Auth))
            {
                if (context.HttpContext.Request.IsBSAjaxRequest())
                {
                    context.Result = new JsonResult(new { code = 401, msg = "没有此功能的权限,请联系管理员授权" });
                    return;
                }
                else
                {
                    var result = new ViewResult { ViewName = "~/Views/Shared/NotAccount.cshtml" };
                    context.Result = result;
                    return;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
