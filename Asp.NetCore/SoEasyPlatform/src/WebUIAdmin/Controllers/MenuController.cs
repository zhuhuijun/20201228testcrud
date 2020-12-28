using LayuiCmsCore.BusinessCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUIAdmin.Models;
using WebUIAdmin.Models.Services;

namespace WebUIAdmin.Controllers
{
    /// <summary>
    /// 菜单管理的功能
    /// </summary>
    [Authorize]
    [PageUIAuthorize(Url = "/Menu/Index")]
    public class MenuController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private INavigationsManager _navBll;
        public MenuController(INavigationsManager nav)
        {
            _navBll = nav;
        }

        /// <summary>
        /// 菜单管理的界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户管理的界面
        /// </summary>
        /// <returns></returns>
        public IActionResult AddUI()
        {
            List<Navigations> navs = _navBll.GetList(tt => tt.IsResource == 0).OrderBy(tt => tt.Order).ToList();
            var parentSel = new SelectList(navs, "ID", "Name");
            ViewBag.ParentId = parentSel.addEmpty();
            return View();
        }
        /// <summary>
        /// 编辑菜单的页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditUI(int id)
        {
            if (id < 1)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "参数错误" });
            }
            var naveinfo = _navBll.GetById(id);
            if (null == naveinfo)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "无法查询到相关数据" });
            }
            List<Navigations> navs = _navBll.GetList(tt => tt.IsResource == 0).OrderBy(tt => tt.Order).ToList();
            var parentSel = new SelectList(navs, "ID", "Name", naveinfo.ParentId);
            ViewBag.ParentId = parentSel.addEmpty();
            return View(naveinfo);
        }
    }
}
