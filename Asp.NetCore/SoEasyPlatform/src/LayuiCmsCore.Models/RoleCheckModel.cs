using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.Models
{
    /// <summary>
    /// RoleCheckModel
    /// </summary>
    public class RoleCheckModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 用户是不是在此角色中选中
        /// </summary>
        public bool RoleIsChecked { get; set; }
    }
}
