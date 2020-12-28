using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LayuiCmsCore.BusinessCore;
using LayuiCmsCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Sugar.Enties;
using WebUIAdmin.Models;
using WebUIAdmin.Models.Services;

namespace WebUIAdmin.Controllers.Api
{
    /// <summary>
    /// 用户管理的api接口
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersManager _userBll;
        private readonly IRolesManager _roleBll;
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
        /// 获取列表的数据
        /// </summary>
        [HttpGet]
        public QueryData<object> Get([FromQuery] QueryUserOption value)
        {
            return value.RetrieveData();
        }
        /// <summary>
        /// 操作保存
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [ButtonAuthorize(Url = "/User/Index", Auth = "add,edit")]
        public CRUDModel Post([FromBody] UsersVM value)
        {
            bool ret = false;
            if (string.IsNullOrEmpty(value.Id))
            {
                var newOne = new Users()
                {
                    UserName = value.UserName,
                    DisplayName = value.DisplayName,
                    Password = value.Password
                };
                //newOne.Description = string.Format("管理员{0}创建用户", User.Identity.Name);
                newOne.Description = string.Format("管理员{0}创建用户", "Admin");
                //newOne.ApprovedBy = User.Identity.Name;
                newOne.ApprovedBy = "Admin";
                newOne.RegisterTime = DateTime.Now;
                newOne.ApprovedTime = DateTime.Now;
                ret = _userBll.CheckSaveUser(newOne);
                return new CRUDModel(ret).DefaultBuild();
            }
            else
            {
                int myid = 0;
                int.TryParse(value.Id, out myid);
                if (myid == 0) return new CRUDModel().Fail();
                var newOne = new Users()
                {
                    UserName = value.UserName,
                    DisplayName = value.DisplayName,
                    Password = value.Password,
                    ID = myid
                };
                ret = _userBll.UpdateUser(newOne);
                return new CRUDModel(ret).DefaultBuild();
            }

        }

        /// <summary>
        /// 删除用户操作
        /// </summary>
        /// <param name="value"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "/User/Index", Auth = "del")]
        public CRUDModel Delete([FromBody] List<int> value)
        {
            dynamic[] ids = new dynamic[value.Count()];
            for (int i = 0; i < value.Count(); i++)
            {
                ids[i] = value[i];
            }
            var admins = _userBll.GetAdminsUser();
            if (admins != null && admins.Count > 0)
            {
                var t = admins.Where(t => value.Contains(t.ID)).FirstOrDefault();
                if (t != null)
                {
                    return new CRUDModel(true).DefaultBuild();
                }
                var tt = _userBll.Delete(ids);
                return new CRUDModel(tt).DefaultBuild();
            }
            return new CRUDModel(false).DefaultBuild();
        }
        /// <summary>
        /// 操作角色的相关方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="values"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ButtonAuthorize(Url = "/User/Index", Auth = "assignRole")]
        public CRUDModel Put(int id, [FromBody] IEnumerable<string> values, [FromQuery] string type)
        {
            bool ret = false;
            switch (type)
            {
                //选择用户分配角色
                case "assignRole":
                    ret = _roleBll.SaveByUserId(id, values);
                    break;
            }
            return new CRUDModel(ret).DefaultBuild();
        }
    }
}
