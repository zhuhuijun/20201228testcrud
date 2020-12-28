using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersVM
    {
        public string Id { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Password { get; set; }

        /// <summary>
        /// Desc:密码盐
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string PassSalt { get; set; }

        /// <summary>
        /// Desc:显示名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DisplayName { get; set; }
    }
}
