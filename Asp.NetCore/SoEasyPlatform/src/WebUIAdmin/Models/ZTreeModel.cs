using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    /// <summary>
    /// ztree展示的model
    /// </summary>
    public class ZTreeModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public int pId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 是不是要展开
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        /// 是不是父亲节点
        /// </summary>
        public bool isParent { get; set; }
        /// <summary>
        /// 选中的节点
        /// </summary>
        public bool @checked { get; set; }
    }
}
