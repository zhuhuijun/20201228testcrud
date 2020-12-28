using AspectCore.Extensions.Autofac;
using Autofac;
using LayuiCmsCore.BusinessCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIAdmin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultModule : Autofac.Module
    {
        private static ILifetimeScope _container;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        protected override void Load(ContainerBuilder containerBuilder)
        {
            //标识注册服务
            containerBuilder.RegisterAssemblyTypes(typeof(DBLogsManager).Assembly)
                 .Where(t => t.Name.EndsWith("Manager", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
            //获取所有控制器类型并使用属性注入
            //var controllerBaseType = typeof(ControllerBase);
            //containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
            //    .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
            //    .PropertiesAutowired();
            containerBuilder.RegisterDynamicProxy();

            // 手动高亮
            containerBuilder.RegisterBuildCallback(container => _container = container);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ILifetimeScope GetContainer()
        {
            return _container;
        }
    }
}
