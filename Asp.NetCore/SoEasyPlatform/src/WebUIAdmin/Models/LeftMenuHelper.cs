using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    public class LeftMenuHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public List<LeftMenuModel> Convert2LeftMenu(List<Navigations> datas)
        {
            var target = new List<LeftMenuModel>();
            if (datas != null && datas.Count > 0)
            {
                foreach (var s in datas)
                {
                    if (s.ParentId == 0)
                    {
                        var leftData = convert2Menu(s);
                        leftData.children = WithData(leftData, datas);
                        target.Add(leftData);
                    }
                }
            }
            return target;
        }
        /// <summary>
        /// 递归菜单
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public List<LeftMenuModel> WithData(LeftMenuModel cur, List<Navigations> datas)
        {
            List<LeftMenuModel> tar = new List<LeftMenuModel>();
            foreach (var s in datas)
            {
                if (s.ParentId == cur.id)
                {
                    var leftData = convert2Menu(s);
                    leftData.children = WithData(leftData, datas);
                    tar.Add(leftData);
                }
            }
            return tar;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public LeftMenuModel convert2Menu(Navigations nav)
        {
            var leftData = new LeftMenuModel()
            {
                id = nav.ID,
                title = nav.Name,
                href = nav.Url,
                icon = nav.Icon,
                Order = nav.Order,
                spread = false,
                children = new List<LeftMenuModel>()
            };
            return leftData;
        }
    }
}
