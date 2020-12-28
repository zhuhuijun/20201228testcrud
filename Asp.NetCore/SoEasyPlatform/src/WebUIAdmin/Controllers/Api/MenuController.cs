using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LayuiCmsCore.BusinessCore;
using LayuiCmsCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUIAdmin.Models;
using WebUIAdmin.Models.Services;

namespace WebUIAdmin.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private INavigationsManager _navBll;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_user"></param>
        public MenuController(INavigationsManager _nav)
        {
            _navBll = _nav;
        }
        /// <summary>
        /// 获取列表的数据
        /// </summary>
        [HttpGet]
        public MenuTreeModel Get([FromQuery] QueryMenuOption value)
        {
            return value.RetrieveData();
        }
        /// <summary>
        /// 操作保存
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [ButtonAuthorize(Url = "/Menu/Index", Auth = "add,edit")]
        public CRUDModel Post([FromBody] Sugar.Enties.Navigations value)
        {
            bool ret = false;
            if (value.ID == 0)
            {
                ret = _navBll.Insert(value);
                return new CRUDModel(ret).DefaultBuild();
            }
            else
            {
                if (value.ID == 0) return new CRUDModel().Fail();
                ret = _navBll.Update(value);
                return new CRUDModel(ret).DefaultBuild();
            }

        }
        /// <summary>
        /// 删除菜单操作
        /// </summary>
        /// <param name="value"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "/Menu/Index", Auth = "del")]
        public CRUDModel Delete([FromBody] List<int> value)
        {
            dynamic[] ids = new dynamic[value.Count()];
            for (int i = 0; i < value.Count(); i++)
            {
                ids[i] = value[i];
            }
            var tt = _navBll.Delete(ids);
            return new CRUDModel(tt).DefaultBuild();
        }
    }
}
