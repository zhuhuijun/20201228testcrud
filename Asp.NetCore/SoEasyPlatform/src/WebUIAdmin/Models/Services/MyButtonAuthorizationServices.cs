using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models.Services
{
    public static class MyButtonAuthorizationServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyButtonAuthorization(this IServiceCollection services, Func<int, string, string, bool>? func = null)
        {
            services.AddSingleton<IMyButtonAuthorization>(new DefaultButtonAuthorization(func));
            return services;
        }
    }
}
