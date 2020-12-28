using System;
using System.Collections.Generic;
using System.Text;

namespace LayuiCmsCore.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CRUDModel
    {
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
        public object data { get; set; }

        private bool Flag = false;
        public CRUDModel Ok()
        {
            this.code = 200;
            this.data = "ok";
            return this;
        }
        public CRUDModel Fail()
        {
            this.code = 500;
            this.msg = "服务器错误";
            return this;
        }
        public CRUDModel() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        public CRUDModel(bool ret)
        {
            this.Flag = ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CRUDModel DefaultBuild()
        {
            if (Flag)
            {
                return Ok();
            }
            return Fail();
        }

        public CRUDModel DefaultBuild(string msg)
        {
            if (Flag)
            {
                return Ok();
            }
            var t = Fail();
            t.msg = msg;
            return t;
        }
    }
}
