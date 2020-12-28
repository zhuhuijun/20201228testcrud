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
    /// 用户查询的类
    /// </summary>
    public class QueryUserOption : PaginationOption
    {
        /// <summary>
        /// 获得/设置 用户登录名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获得/设置 用户显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public QueryData<object> RetrieveData()
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
            Expression<Func<Users, object>> orderByExpression = t => t.RegisterTime;
            OrderByType orderByType = OrderByType.Desc;
            orderByType = Order == "asc" ? OrderByType.Asc : OrderByType.Desc;
            var ret = new QueryData<object>(PageIndex, Limit)
            {
                code = 200,
                msg = "ok"
            };
            UsersManager db = new UsersManager();

            var p = new PageModel() { PageIndex = PageIndex, PageSize = Limit };// 分页查询
            var data = db.UsersDb.GetPageList(condition, p, orderByExpression, orderByType);
            ret.total = p.PageCount;
            ret.rows = data.Select(u => new
            {
                Id = u.ID,
                u.UserName,
                u.DisplayName,
                u.RegisterTime,
                u.ApprovedTime,
                u.ApprovedBy,
                u.Description,
                IsReset = 0
            });
            return ret;
        }
    }
}
