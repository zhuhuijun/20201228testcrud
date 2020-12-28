using LayuiCmsCore.Models;
using Longbow.Security.Cryptography;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace LayuiCmsCore.BusinessCore
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UsersManager : IUsersManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool CheckSaveUser(Users userData)
        {
            if (!UserChecker(userData)) return false;
            // 已经存在或者已经在新用户中了
            var users = GetList(t => t.UserName == userData.UserName).FirstOrDefault();
            if (userData.ID == 0 && users != null)
            {
                return false;
            }
            var passSalt = LgbCryptography.GenerateSalt();
            var newPassword = LgbCryptography.ComputeHash(userData.Password, passSalt);
            userData.PassSalt = passSalt;
            userData.Password = newPassword;
            var tt = Insert(userData);
            return tt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool UserChecker(Users user)
        {
            if (user.Description.Length > 500) user.Description = user.Description.Substring(0, 500);
            if (user.UserName.Length > 16) user.UserName = user.UserName.Substring(0, 16);
            if (user.Password.Length > 50) user.Password = user.Password.Substring(0, 50);
            if (user.DisplayName.Length > 20) user.DisplayName = user.DisplayName.Substring(0, 20);
            var pattern = @"^[a-zA-Z0-9_@.]*$";
            return Regex.IsMatch(user.UserName, pattern);
        }
        /// <summary>
        /// 获得管理员
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAdminsUser()
        {
            var admins = this.GetList(u => u.UserName == "Admin" || u.UserName == "User").ToList();
            return admins;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool UserCheckerUpdate(Users user)
        {
            if (user.UserName.Length > 16) user.UserName = user.UserName.Substring(0, 16);
            if (user.Password.Length > 50) user.Password = user.Password.Substring(0, 50);
            if (user.DisplayName.Length > 20) user.DisplayName = user.DisplayName.Substring(0, 20);
            var pattern = @"^[a-zA-Z0-9_@.]*$";
            return Regex.IsMatch(user.UserName, pattern);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool UpdateUser(Users userData)
        {
            var usersOne = new Users()
            {
                ID = userData.ID,
                Password = userData.Password,
                DisplayName = userData.DisplayName,
                UserName = userData.UserName
            };
            if (!UserCheckerUpdate(usersOne)) return false;
            var admins = this.GetAdminsUser();
            if (admins != null && admins.Count > 0)
            {
                var t = admins.Where(t => t.ID == usersOne.ID).FirstOrDefault();
                if (t != null)
                {
                    return true;
                }
                var passSalt = LgbCryptography.GenerateSalt();
                var newPassword = LgbCryptography.ComputeHash(userData.Password, passSalt);
                usersOne.PassSalt = passSalt;
                usersOne.Password = newPassword;
                var tt = UpdateFixedColumn(usersOne, t => new { t.UserName, t.DisplayName, t.PassSalt, t.Password });
                return tt;
            }
            return false;
        }
        /// <summary>
        /// 根据角色来判断用户是不是选中
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<UserCheckModel> GetSelectedUserByRole(int RoleId)
        {
            var noadmins = this.GetList(u => (u.UserName != "Admin" && u.UserName != "User")).ToList();
            var relsData = UserRoleDb.GetList(t => t.RoleID == RoleId);
            List<UserCheckModel> rets = new List<UserCheckModel>();
            if (noadmins != null && noadmins.Count > 0)
            {
                rets.AddRange(noadmins.Select(t => new UserCheckModel()
                {
                    Id = t.ID,
                    UserName = t.UserName,
                    UserIsChecked = false
                }));
                for (int i = 0; i < rets.Count; i++)
                {
                    var cur = rets[i];
                    var myrel = relsData.FirstOrDefault(t => t.UserID == cur.Id);
                    if (null != myrel)
                    {
                        rets[i].UserIsChecked = true;
                    }
                }
            }
            return rets;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        public bool LoginCheck(string userName, string userpwd,out int userid)
        {
            var userOne = GetList(t => t.UserName == userName).FirstOrDefault();
            userid = 0;
            if (userOne == null)
            {
                return false;
            }
            var passSalt = userOne.PassSalt;
            var newPassword = LgbCryptography.ComputeHash(userpwd, passSalt);
            if (userOne.Password == newPassword)
            {
                userid = userOne.ID;
                return true;
            }
            return false;
        }

    }
}
