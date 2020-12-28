using LayuiCmsCore.BusinessCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUIAdmin.Models.Services;

namespace WebUIAdmin.Controllers
{
    /// <summary>
    /// 用户管理的功能
    /// </summary>
    [Authorize]
    [PageUIAuthorize(Url = "/User/Index")]
    public partial class UserController : Controller
    {
        private IUsersManager _userBll;
        private IRolesManager _roleBll;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_user"></param>
        public UserController(IUsersManager _user, IRolesManager _role)
        {
            _userBll = _user;
            _roleBll = _role;
        }

        /// <summary>
        /// 用户管理的界面
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
            var userinfo = _userBll.GetById(id);
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
        public IActionResult AssignRole(int id)
        {
            if (id < 1)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "参数错误" });
            }
            var userinfo = _userBll.GetById(id);
            if (null == userinfo)
            {
                return View("Error", new WebUIAdmin.Models.ErrorViewModel() { RequestId = "无法查询到相关数据" });
            }
            //使用ViewData传值
            ViewBag.UserDatas = _roleBll.GetSelectedRoleByUser(id);
            ViewBag.UserId = id;
            return View();
        }
    }
}
