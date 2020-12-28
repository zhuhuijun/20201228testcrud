using LayuiCmsCore.BusinessCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUIAdmin.Models;

namespace WebUIAdmin.Controllers
{
    /// <summary>
    /// 用户管理的功能
    /// </summary>
    [AllowAnonymous]
    public partial class OthersController : Controller
    {

        /// <summary>
        /// IconPage管理的界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult IconPage(string oldicon)
        {
            ViewBag.OldIcon = oldicon;
            return View();
        }

    }
}
