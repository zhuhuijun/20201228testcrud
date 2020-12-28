using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUIAdmin.Models;

namespace WebUIAdmin.Extensions
{
    public static class RequestExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsBSAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Headers != null)
            {
                if (request.Headers["X-Requested-With"] == "XMLHttpRequest"
                    || request.Headers["X-Sourced-By"] == "ajax")
                {
                    return true;
                }

                return request.Headers["X-Requested-With"] == "XMLHttpRequest"
                    || request.Headers["X-Sourced-By"] == "ajax";
            }
            return false;
        }



        /// <summary>
        ///  根据请求信息返回请求串
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <returns>请求串</returns>
        public static string QueryString(this HttpRequest request)
        {
            // post 请求方式获取请求参数方式
            if (request.Method.ToLower().Equals("post"))
            {
                try
                {
                    // post请求方式获取请求参数，.net core 2.0 不支持.Form 直接获取 
                    Stream stream = request.Body;
                    string data = string.Empty;
                    using (var mem = new MemoryStream())
                    {
                        using (var reader = new StreamReader(mem))
                        {
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.CopyTo(mem);
                            mem.Seek(0, SeekOrigin.Begin);
                            data = reader.ReadToEnd();
                        }
                    }
                    return data;
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }
            else
            {
                return request.QueryString.Value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlauth"></param>
        /// <param name="modelDta"></param>
        /// <returns></returns>
        public static string AddOrEdit(this string urlauth, string modelDta)
        {
            if (!string.IsNullOrEmpty(modelDta))
            {
                ParamModel pm = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(modelDta);
                if (pm != null && pm.Id > 0)
                {
                    return "edit";
                }
                return "add";
            }
            return urlauth;
        }
        /// <summary>
        /// 判断是分配用户还是分配菜单的按钮
        /// </summary>
        /// <param name="urlauth"></param>
        /// <param name="modelDta"></param>
        /// <returns></returns>
        public static string assignUserOrMenuType(this string urlauth, string modelDta)
        {
            if (!string.IsNullOrEmpty(modelDta))
            {
                modelDta = modelDta.Replace("?", "");
                string[] paramArr = modelDta.Split("&");
                if(paramArr.Length > 0)
                {
                    Dictionary<string, string> ssKv = new Dictionary<string, string>();
                    foreach(var s in paramArr)
                    {
                        string[] kv = s.Split("=");
                        if(kv.Length == 2)
                        {
                            if (!ssKv.ContainsKey(kv[0]))
                            {
                                ssKv.Add(kv[0], kv[1]);
                            }
                        }
                        if (ssKv.ContainsKey("type"))
                        {
                            return ssKv["type"];
                        }
                    }
                }
            }
            return modelDta;
        }
    }

}
