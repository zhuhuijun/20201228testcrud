using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMyButtonAuthorization
    {
        /// <summary>
        /// 授权方法
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="url">按钮所属菜单配置路径</param>
        /// <param name="auths">按钮授权码</param>
        /// <returns></returns>
        bool Authorizate(HttpContext context, string url, string auths);
    }
}
