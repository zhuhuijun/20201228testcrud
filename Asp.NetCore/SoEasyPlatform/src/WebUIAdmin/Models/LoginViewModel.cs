using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    /// <summary>
    /// 用户登录的实体
    /// </summary>
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string CheckCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}
