using LayuiCmsCore.Models;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.BusinessCore
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IRolesManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Roles> GetAdminRole();
        /// <summary>
        /// 根据角色保存用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public bool SaveByRoleId(int roleId, IEnumerable<string> userIds);
        /// <summary>
        /// 根据用户保存角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="RowIds"></param>
        /// <returns></returns>
        public bool SaveByUserId(int userId, IEnumerable<string> RowIds);

        /// <summary>
        /// 为角色分配菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="MenuIds"></param>
        /// <returns></returns>
        public bool SaveMenuByRoleId(int roleId, IEnumerable<string> MenuIds);
        /// <summary>
        /// 根据用户拿到相关的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RoleCheckModel> GetSelectedRoleByUser(int userId);
    }
}
