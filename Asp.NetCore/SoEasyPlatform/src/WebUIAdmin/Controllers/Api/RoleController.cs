using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LayuiCmsCore.BusinessCore;
using LayuiCmsCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sugar.Enties;
using WebUIAdmin.Models;
using WebUIAdmin.Models.Services;

namespace WebUIAdmin.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRolesManager _roleBll;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_user"></param>
        public RoleController(IRolesManager _user)
        {
            _roleBll = _user;
        }
        /// <summary>
        /// 获取列表的数据
        /// </summary>
        [HttpGet]
        public QueryData<object> Get([FromQuery] QueryRoleOption value)
        {
            return value.RetrieveData();
        }

        /// <summary>
        /// 操作保存
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [ButtonAuthorize(Url = "/Role/Index", Auth = "add,edit")]
        public CRUDModel Post([FromBody] Roles value)
        {
            bool ret = false;
            if (value.ID==0)
            {
                ret = _roleBll.Insert(value);
                return new CRUDModel(ret).DefaultBuild();
            }
            else
            {
                if (value.ID == 0) return new CRUDModel().Fail();
                ret = _roleBll.Update(value);
                return new CRUDModel(ret).DefaultBuild();
            }

        }

        /// <summary>
        /// 删除用户操作
        /// </summary>
        /// <param name="value"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "/Role/Index", Auth = "del")]
        public CRUDModel Delete([FromBody] List<int> value)
        {
            dynamic[] ids = new dynamic[value.Count()];
            for (int i = 0; i < value.Count(); i++)
            {
                ids[i] = value[i];
            }
            var admins = _roleBll.GetAdminRole();
            if (admins != null && admins.Count > 0)
            {
                var t = admins.Where(t => value.Contains(t.ID)).FirstOrDefault();
                if (t != null)
                {
                    return new CRUDModel(true).DefaultBuild();
                }
                var tt = _roleBll.Delete(ids);
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
        [ButtonAuthorize(Url = "/Role/Index", Auth = "assignUser,assignMenu")]
        public CRUDModel Put(int id, [FromBody] IEnumerable<string> values, [FromQuery] string type)
        {
            bool ret = false;
            switch (type)
            {
                //选择角色给分配给用户
                case "assignUser":
                    ret = _roleBll.SaveByRoleId(id, values);
                    break;
                //为角色分配菜单
                case "assignMenu":
                    ret = _roleBll.SaveMenuByRoleId(id, values);
                    MyMenuHelper.ClearCache();
                    break;
            }
            return new CRUDModel(ret).DefaultBuild();
        }
    }
}
