using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUIAdmin.Models.Services;

namespace WebUIAdmin.Models.TagesHelper
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement(Attributes = "bsp-auth")]
    public class ShowButtonTagHelper : TagHelper
    {
        private IMyButtonAuthorization authorizationServices;
        /// <summary>
        /// 获得 请求上线文
        /// </summary>
        public HttpContext HttpContext { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttonAuth"></param>
        public ShowButtonTagHelper(IMyButtonAuthorization buttonAuth, IHttpContextAccessor httpContextAccessor)
        {
            authorizationServices = buttonAuth;
            HttpContext = httpContextAccessor.HttpContext;
        }

        public string BspAuth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["Controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["Action"].ToString();
            //var currentUrl = ViewContext.RouteData.Values.Values;   //jion是个扩展方法
            string path = $"/{currentController}/{currentAction}";
            var identity = ViewContext.HttpContext.User.Identity;
            var pp = new ClaimsPrincipal(identity);
            bool isAuth = authorizationServices.Authorizate(HttpContext, path, BspAuth);
            if (!isAuth)
            {
                output.SuppressOutput();
            }
            else
            {
                // 授权通过生成相对应的 HTML 到上下文中
                base.Process(context, output);
                return;
            }
        }
    }
}
