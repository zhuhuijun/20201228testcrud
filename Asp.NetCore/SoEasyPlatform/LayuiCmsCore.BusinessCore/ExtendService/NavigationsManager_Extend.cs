using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.BusinessCore
{
    /// <summary>
    /// 菜单的查询
    /// </summary>
    public partial class NavigationsManager : INavigationsManager
    {
        /// <summary>
        /// 根据用户查询菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Navigations> GetMenuByUserId(int userId)
        {
            string sql = string.Format(@"SELECT * FROM Navigations WHERE ID IN (
                        SELECT DISTINCT NavigationID FROM NavigationRole WHERE RoleID IN(
                        SELECT RoleID FROM UserRole WHERE UserID = {0})
                        ) AND IsResource = 0 ORDER BY `Order`", userId);
            var ret = Db.SqlQueryable<Navigations>(sql).ToList();
            return ret;
        }
    }
}
