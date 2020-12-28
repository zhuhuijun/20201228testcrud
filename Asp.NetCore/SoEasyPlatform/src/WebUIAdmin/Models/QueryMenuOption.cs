using LayuiCmsCore.BusinessCore;
using LayuiCmsCore.Models;
using SqlSugar;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    /// <summary>
    /// 角色查询的类
    /// </summary>
    public class QueryMenuOption : PaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MenuTreeModel RetrieveData()
        {
            List<IConditionalModel> condition = new List<IConditionalModel>();
            if (!string.IsNullOrEmpty(SearchData))
            {
                var models = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SearchDataModel>>(SearchData);
                if (models != null && models.Count > 0)
                {
                    foreach (var pp in models)
                    {
                        if (!string.IsNullOrEmpty(pp.ParamVal))
                        {
                            condition.Add(new ConditionalModel()
                            {
                                FieldName = pp.ParamName,
                                FieldValue = pp.ParamVal,
                                ConditionalType = (ConditionalType)pp.OpType
                            });
                        }
                    }
                }
            }
            Expression<Func<Navigations, object>> orderByExpression = t => t.Order;
            OrderByType orderByType = OrderByType.Asc;
            var ret = new MenuTreeModel()
            {
                code = 0,
                msg = string.Empty
            };
            NavigationsManager db = new NavigationsManager();
            var p = new PageModel() { PageIndex = 1, PageSize = 1000 };// 分页查询
            var data = db.NavigationsDb.GetPageList(condition, p, orderByExpression, orderByType);
            ret.count = p.PageCount;
            ret.data = data.Select(u => new
            {
                authorityId = u.ID,
                parentId = u.ParentId ,
                authorityName = u.Name,
                Icon = u.Icon,
                IsResource =u.IsResource,
                open =false,
                Url=u.Url,
                u.Order
            });
            return ret;
        }
    }
}
