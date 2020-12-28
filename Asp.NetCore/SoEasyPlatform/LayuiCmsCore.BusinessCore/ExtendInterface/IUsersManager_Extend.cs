using LayuiCmsCore.Models;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.BusinessCore
{
    public partial interface IUsersManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool CheckSaveUser(Users userData);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool UpdateUser(Users userData);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAdminsUser();
        /// <summary>
        /// 根据角色来判断用户是不是选中
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<UserCheckModel> GetSelectedUserByRole(int RoleId);
        /// <summary>
        /// 登录的验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userpwd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool LoginCheck(string userName, string userpwd,out int userid);
    }
}
