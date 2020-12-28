using LayuiCmsCore.BusinessCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUIAdmin.Models;

namespace WebUIAdmin.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUsersManager _userManager;
        public AccountController(IUsersManager um)
        {
            _userManager = um;
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string errmsg)
        {
            ViewBag.ErrorMsg = errmsg;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="remember"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName)
                || string.IsNullOrEmpty(loginModel.UserPwd)
                || string.IsNullOrEmpty(loginModel.CheckCode))
            {
                return RedirectToAction("Login", new { errmsg = "参数错误" });
            }
            int userId = 0;
            bool isSuccess = _userManager.LoginCheck(loginModel.UserName, loginModel.UserPwd, out userId);
            if (isSuccess && userId > 0)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid,userId.ToString()),
                    new Claim(ClaimTypes.Name,loginModel.UserName)
                };
                var identity = new ClaimsIdentity(claims, "loginClaim");
                var userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                    , userPrincipal
                    , new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.Now.AddDays(7),
                        IsPersistent = false,
                        AllowRefresh = false
                    });
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", new { errmsg = "用户名或者密码错误" });
        }

        /// <summary>
        /// Logout this instance.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns>The logout.</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            string targetUrl = Request.PathBase + CookieAuthenticationDefaults.LoginPath;
            return Redirect(targetUrl);
        }
    }
}
