using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class HelpUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public static List<SelectListItem> addEmpty(this SelectList old)
        {
            var itemList = new List<SelectListItem>() { new SelectListItem() { Text = "请选择", Value = "0" } };
            itemList.AddRange(old);
            return itemList;
        }

    }
}
