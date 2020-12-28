using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    public class IdentityDataHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contex"></param>
        /// <returns></returns>
        public static IdentityModel GetIdentityUserId(HttpContext contex)
        {
            var iden = contex.User.Identities.Where(t => t.AuthenticationType == "loginClaim").FirstOrDefault();
            if (iden == null)
            {
                return null;
            }
            var tt = iden.Claims.Where(t => t.Type == ClaimTypes.Sid).FirstOrDefault();
            int userId = 0;
            Int32.TryParse(tt.Value, out userId);
            IdentityModel im = new IdentityModel();
            im.UserId = userId;
            im.UserName = contex.User.Identity.Name;
            return im;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class IdentityModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
