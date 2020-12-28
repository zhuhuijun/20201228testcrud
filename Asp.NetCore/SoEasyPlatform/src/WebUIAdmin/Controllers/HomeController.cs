using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LayuiCmsCore.BusinessCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUIAdmin.Models;

namespace WebUIAdmin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INavigationsManager _navService;

        public HomeController(ILogger<HomeController> logger, INavigationsManager navService)
        {
            _logger = logger;
            _navService = navService;
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            int userId = IdentityDataHelper.GetIdentityUserId(HttpContext).UserId;
            var navLists = _navService.GetMenuByUserId(userId);
            var menuHelper = new LeftMenuHelper();
            var datas = menuHelper.Convert2LeftMenu(navLists);
            ViewBag.MenuData = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
            ViewBag.UserName = IdentityDataHelper.GetIdentityUserId(HttpContext).UserName;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Main()
        {
            return View();
        }
        public IActionResult ArticleList()
        {
            return View();
        }
    }
}
