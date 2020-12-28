using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.Models
{
    public class MenuTreeModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// msg
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }
    }
}
