using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.Models
{
    /// <summary>
    /// 查询Model
    /// </summary>
    public class SearchDataModel
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// 查询值
        /// </summary>
        public string ParamVal { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public int OpType { get; set; }
    }
}
