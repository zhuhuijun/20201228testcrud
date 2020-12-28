using LayuiCmsCore.BusinessCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUIAdmin.Models;
using WebUIAdmin.Models.Services;

namespace WebUIAdmin.Controllers
{
    /// <summary>
    /// 用户管理的功能
    /// </summary>
    [Authorize]
    [PageUIAuthorize(Url = "/Role/Index")]
    public partial class RoleController : Controller
    {
        private IRolesManager _Bll;
        private IUsersManager _UserBll;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_user"></param>
        public RoleController(IUsersManager _user, IRolesManager _role)
        {
            _Bll = _role;
            _UserBll = _user;
        }

        /// <summary>
        /// 角色管理的界面
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
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditUI(int id)
        {
            if (id < 1)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "参数错误" });
            }
            var userinfo = _Bll.GetById(id);
            if (null == userinfo)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "无法查询到相关数据" });
            }
            return View(userinfo);
        }

        /// <summary>
        /// 为角色分配给用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult assignUser(int id)
        {
            if (id < 1)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "参数错误" });
            }
            var userinfo = _Bll.GetById(id);
            if (null == userinfo)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "无法查询到相关数据" });
            }
            //使用ViewData传值
            ViewBag.UserDatas = _UserBll.GetSelectedUserByRole(id);
            ViewBag.RowId = id;
            return View();
        }

        /// <summary>
        /// 为角色分配给菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult assignMenu(int id)
        {
            if (id < 1)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "参数错误" });
            }
            var userinfo = _Bll.GetById(id);
            if (null == userinfo)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "无法查询到相关数据" });
            }
            ViewBag.RowId = id;
            NavigationsManager db = new NavigationsManager();
            var data = db.NavigationsDb.GetList().OrderBy(t=>t.Order).ToList();
            var roleAndMenus = db.NavigationRoleDb.GetList(t => t.RoleID == id).ToList();
            var ztree = ZTreeWrapeHelper.translateTreeData(data, roleAndMenus);
            ViewBag.MenuData = Newtonsoft.Json.JsonConvert.SerializeObject(ztree);
            return View();
        }
    }
}
