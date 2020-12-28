using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.Models
{
    /// <summary>
    /// 分页配置项类
    /// </summary>
    public class PaginationOption
    {
        //
        // 摘要:
        //     获得/设置 每页显示行数
        public int Limit { get; set; }
        //
        // 摘要:
        //     获得/设置 当前数据偏移量 Offset / Limit 的页码 第一页换算方法为 Offset / Limit + 1
        public int Offset { get; set; }
        //
        // 摘要:
        //     获得/设置 排序列名称
        public string Sort { get; set; }
        //
        // 摘要:
        //     获得/设置 排序方式 asc/desc 默认 asc 排序
        public string Order { get; set; }
        //
        // 摘要:
        //     获得/设置 搜索内容
        public string Search { get; set; }
        //
        // 摘要:
        //     获得 当前页码，内部自动通过Limit Offset属性计算获得
        public int PageIndex { get; set; }
        /// <summary>
        /// 查询参数
        /// </summary>
        public string SearchData { get; set; }
    }
}
