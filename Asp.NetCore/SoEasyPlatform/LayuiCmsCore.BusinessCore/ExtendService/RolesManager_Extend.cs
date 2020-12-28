using LayuiCmsCore.Models;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayuiCmsCore.BusinessCore
{
    public partial class RolesManager : IRolesManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Roles> GetAdminRole()
        {
            var admins = this.GetList(u => u.RoleName == "Administrators" || u.RoleName == "Default");
            return admins;
        }
        /// <summary>
        /// 根据角色保存用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public bool SaveByRoleId(int roleId, IEnumerable<string> userIds)
        {
            bool ret = false;
            try
            {
                Db.Ado.BeginTran();
                Db.Ado.ExecuteCommand($"delete from UserRole where RoleID = {roleId}");
                List<UserRole> userAndRole = new List<UserRole>();
                userAndRole.AddRange(userIds.Select(g => new UserRole()
                {
                    UserID = Int32.Parse(g),
                    RoleID = roleId
                }));
                Db.Insertable(userAndRole.ToArray()).ExecuteCommand();
                Db.Ado.CommitTran();
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                Db.Ado.RollbackTran();
                throw ex;
            }
            return ret;
        }
        /// <summary>
        /// 根据用户保存角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="RowIds"></param>
        /// <returns></returns>
        public bool SaveByUserId(int userId, IEnumerable<string> RowIds)
        {
            bool ret = false;
            try
            {
                Db.Ado.BeginTran();
                Db.Ado.ExecuteCommand($"delete from UserRole where UserId = {userId}");
                List<UserRole> userAndRole = new List<UserRole>();
                userAndRole.AddRange(RowIds.Select(g => new UserRole()
                {
                    UserID = userId,
                    RoleID = Int32.Parse(g)
                }));
                Db.Insertable(userAndRole.ToArray()).ExecuteCommand();
                Db.Ado.CommitTran();
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                Db.Ado.RollbackTran();
                throw ex;
            }
            return ret;
        }
        /// <summary>
        /// 为角色分配菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="MenuIds"></param>
        /// <returns></returns>
        public bool SaveMenuByRoleId(int roleId, IEnumerable<string> MenuIds)
        {
            bool ret = false;
            try
            {
                Db.Ado.BeginTran();
                Db.Ado.ExecuteCommand($"delete from NavigationRole where RoleID = {roleId}");
                List<NavigationRole> menusRole = new List<NavigationRole>();
                menusRole.AddRange(MenuIds.Select(g => new NavigationRole()
                {
                    NavigationID = Int32.Parse(g),
                    RoleID = roleId
                }));
                Db.Insertable(menusRole.ToArray()).ExecuteCommand();
                Db.Ado.CommitTran();
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                Db.Ado.RollbackTran();
                throw ex;
            }
            return ret;
        }
        /// <summary>
        /// 根据相关的用户找角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RoleCheckModel> GetSelectedRoleByUser(int userId)
        {
            var roles = this.GetList().ToList();
            var relsData = UserRoleDb.GetList(t => t.UserID == userId);
            List<RoleCheckModel> rets = new List<RoleCheckModel>();
            if (roles != null && roles.Count > 0)
            {
                rets.AddRange(roles.Select(t => new RoleCheckModel()
                {
                    Id = t.ID,
                    RoleName = t.RoleName,
                    RoleIsChecked = false
                }));
                for (int i = 0; i < rets.Count; i++)
                {
                    var cur = rets[i];
                    var myrel = relsData.FirstOrDefault(t => t.RoleID == cur.Id);
                    if (null != myrel)
                    {
                        rets[i].RoleIsChecked = true;
                    }
                }
            }
            return rets;
        }
    }
}
