using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.BusinessCore
{
    /// <summary>
    /// 菜单的扩展
    /// </summary>
    public partial interface INavigationsManager
    {
        /// <summary>
        /// 根据用户id来获得菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Navigations> GetMenuByUserId(int userId);
    }
}
