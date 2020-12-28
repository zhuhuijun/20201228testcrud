using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models.Services
{
    public class DefaultButtonAuthorization : IMyButtonAuthorization
    {
        private readonly Func<int, string, string, bool> _proxy;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="func">通过指定用户名获取按钮授权集合回调方法</param>
        public DefaultButtonAuthorization(Func<int, string, string, bool> func = null)
        {
            _proxy = func;
        }
        public bool Authorizate(HttpContext context, string url, string auths)
        {
            if (context.User.IsInRole("Administrators")) return true;

            int uid = IdentityDataHelper.GetIdentityUserId(context).UserId;
            if (uid <= 0)
            {
                return false;
            }
            if (null == _proxy)
            {
                return false;
            }
            return _proxy.Invoke(uid, url, auths);
        }
    }
}
