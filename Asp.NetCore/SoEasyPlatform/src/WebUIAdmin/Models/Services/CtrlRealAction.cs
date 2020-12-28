using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUIAdmin.Extensions;

namespace WebUIAdmin.Models.Services
{
    /// <summary>
    /// 识别真正的action
    /// </summary>
    public class CtrlRealAction
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private CtrlRealAction() { }

        /// <summary>
        /// 
        /// </summary>
        private static readonly CtrlRealAction _instance = new CtrlRealAction();

        public static CtrlRealAction Instance
        {
            get
            {
                return _instance;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CtrlAuth(string auth, HttpContext context)
        {
            if (auth.Contains("add,edit") || auth.Contains("assignUser,assignMenu"))
            {
                string target = string.Empty;
                string param = context.Request.QueryString();

                if (!string.IsNullOrEmpty(param))
                {
                    if (auth.Contains("add,edit"))
                    {
                        target = auth.AddOrEdit(param);
                    }
                    else
                    {
                        target = auth.assignUserOrMenuType(param);
                    }
                    return target;
                }
            }
            return auth;
        }
    }
}
