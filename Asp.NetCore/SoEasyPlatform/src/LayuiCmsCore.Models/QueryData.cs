using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayuiCmsCore.Models
{
    /// <summary>
    /// QueryData 泛型操作类，配合 bootstrap-table 组件使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryData<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        private QueryData() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="l"></param>
        public QueryData(int pi, int l)
        {
            this.pageindex = pi;
            this.limit = l;
        }
        /// <summary>
        /// code
        /// </summary>
        public long code { get; set; }
        /// <summary>
        /// msg
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 获得/设置 数据总数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int maxpagesize
        {
            get
            {
                if(limit == 0)
                {
                    return 0;
                }
                if (total % limit == 0)
                {
                    return (total / limit);
                }
                return (total / limit) + 1;
            }
        }

        public int limit { get; set; }
        /// <summary>
        /// 获得/设置 当前页记录条数
        /// </summary>
        public IEnumerable<T> rows { get; set; } = Enumerable.Empty<T>();
    }
}
