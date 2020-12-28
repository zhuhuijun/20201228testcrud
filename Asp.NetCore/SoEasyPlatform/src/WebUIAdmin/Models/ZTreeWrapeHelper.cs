using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ZTreeWrapeHelper
    {
        /// <summary>
        /// 处理ztree的数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        public static List<ZTreeModel> translateTreeData(List<Navigations> sourceData, List<NavigationRole> rels)
        {
            List<ZTreeModel> trees = new List<ZTreeModel>();
            if (null != sourceData)
            {
                int extendCount = 0;
                foreach (var s in sourceData)
                {
                    var ztree = new ZTreeModel()
                    {
                        id = s.ID,
                        pId = s.ParentId.Value,
                        name = s.Name
                    };
                    var hasChild = sourceData.Where(t => t.ParentId == ztree.id).Count();
                    if (hasChild > 0)
                    {
                        ztree.isParent = true;
                    }
                    if (ztree.isParent)
                    {
                        ztree.open = extendCount >= 3 ? false : true;
                        extendCount++;
                    }
                    trees.Add(ztree);
                }
            }
            trees = mapNavigationAndRole(trees, rels);
            return trees;
        }
        /// <summary>
        /// 映射角色的选中的菜单
        /// </summary>
        /// <param name="oldData"></param>
        /// <param name="rels"></param>
        private static List<ZTreeModel> mapNavigationAndRole(List<ZTreeModel> oldData, List<NavigationRole> rels)
        {
            for (int i = 0; i < oldData.Count; i++)
            {
                var cur = oldData[i];
                var hadRecord = rels.Where(t => t.NavigationID == cur.id).FirstOrDefault();
                if (hadRecord != null)
                {
                    cur.@checked = true;
                }
            }
            return oldData;
        }
    }
}
