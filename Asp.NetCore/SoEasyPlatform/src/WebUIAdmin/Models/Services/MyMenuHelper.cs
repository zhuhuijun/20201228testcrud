using LayuiCmsCore.BusinessCore;
using LayuiCmsCore.Utils;
using Longbow.Cache;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class MyMenuHelper
    {
        public static List<string> DefaultUI = new List<string>()
        {
            "Index"
        };
        public static string BootstrapMenu_RetrieveMenus = "BootstrapMenu-RetrieveMenus";
        /// <summary>
        /// 判断用户的按钮是否有权限
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="url"></param>
        /// <param name="authkey"></param>
        /// <returns></returns>
        public static bool AuthorizateButtons(int userid, string url, string authkey)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(authkey))
            {
                return false;
            }
            List<Navigations> userMenus = null;
            string key = $"{BootstrapMenu_RetrieveMenus}_{userid}";
            userMenus = CacheManager.Get<List<Navigations>>(key);
            if (userMenus == null)
            {
                NavigationsManager db = new NavigationsManager();
                string sql = string.Format(@"SELECT * FROM Navigations WHERE Id IN (
                            select distinct NR.NavigationID from UserRole UR INNER JOIN NavigationRole NR
                            ON UR.RoleID = NR.RoleID LEFT JOIN Users UU ON UR.UserID =UU.ID WHERE UU.ID={0})", userid);
                userMenus = db.Db.SqlQueryable<Navigations>(sql).ToList();
                CacheManager.GetOrAdd<List<Navigations>>(key, key => userMenus, BootstrapMenu_RetrieveMenus).ToList();
            }
            var currMenus = userMenus.Where(t => t.Url == url).FirstOrDefault();
            if (null == currMenus)
            {
                return false;
            }
            var pid = currMenus.ID;
            var childrens = userMenus.Where(t => t.ParentId == pid).Select(m => m.Url).ToList();
            List<string> menus = new List<string>();
            menus.AddRange(DefaultUI);
            menus.AddRange(childrens);

            var keys = authkey.SpanSplitAny(",. ;", StringSplitOptions.RemoveEmptyEntries);
            return keys.Any(m => menus.Any(k => k == m));
        }
        /// <summary>
        /// 
        /// </summary>
        public static void ClearCache()
        {
            var cacheKeys = new List<string>();
            cacheKeys.Add(BootstrapMenu_RetrieveMenus + "*");
            CacheManager.Clear(cacheKeys);
        }
    }
}
