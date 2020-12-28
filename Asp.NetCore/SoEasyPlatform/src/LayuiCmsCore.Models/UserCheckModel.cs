using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.Models
{
    public class UserCheckModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户是不是在此角色中选中
        /// </summary>
        public bool UserIsChecked { get; set; }
    }
}
