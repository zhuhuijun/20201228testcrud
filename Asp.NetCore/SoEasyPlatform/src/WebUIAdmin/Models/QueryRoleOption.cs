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
    public class QueryRoleOption : PaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public QueryData<object> RetrieveData()
        {
            if (Limit == 0)
            {
                return new QueryData<object>(PageIndex, Limit)
                {
                    code = 500,
                    msg = "error"
                };
            }
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
            Expression<Func<Roles, object>> orderByExpression = t => t.ID;
            OrderByType orderByType = OrderByType.Desc;
            orderByType = Order == "asc" ? OrderByType.Asc : OrderByType.Desc;
            var ret = new QueryData<object>(PageIndex, Limit)
            {
                code = 200,
                msg = "ok"
            };
            RolesManager db = new RolesManager();

            var p = new PageModel() { PageIndex = PageIndex, PageSize = Limit };// 分页查询
            var data = db.RolesDb.GetPageList(condition, p, orderByExpression, orderByType);
            ret.total = p.PageCount;
            ret.rows = data.Select(u => new
            {
                Id = u.ID,
                u.RoleName,
                u.Description
            });
            return ret;
        }
    }
}
