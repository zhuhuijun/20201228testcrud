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
    public class PageUIAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 按钮授权所属菜单
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var buttonAuthorizateSvr = context.HttpContext.RequestServices.GetRequiredService<IMyButtonAuthorization>();
            var controllerName = context.ActionDescriptor.RouteValues["controller"] == null ? "" : context.ActionDescriptor.RouteValues["controller"].ToString();
            var actionName = context.ActionDescriptor.RouteValues["action"] == null ? "" : context.ActionDescriptor.RouteValues["action"].ToString();
            actionName = actionName.Replace("AddUI", "add");
            actionName = actionName.Replace("EditUI", "edit");
            if (buttonAuthorizateSvr == null || !buttonAuthorizateSvr.Authorizate(context.HttpContext, Url, actionName))
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
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
