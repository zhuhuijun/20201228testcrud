using Sugar.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace LayuiCmsCore.BusinessCore
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=s100;uid=admin;pwd=zzbj891016;database=crud02",
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据

        public SimpleClient<DBLogs> DBLogsDb { get { return new SimpleClient<DBLogs>(Db); } }//用来处理DBLogs表的常用操作
        public SimpleClient<Dicts> DictsDb { get { return new SimpleClient<Dicts>(Db); } }//用来处理Dicts表的常用操作
        public SimpleClient<Exceptions> ExceptionsDb { get { return new SimpleClient<Exceptions>(Db); } }//用来处理Exceptions表的常用操作
        public SimpleClient<Groups> GroupsDb { get { return new SimpleClient<Groups>(Db); } }//用来处理Groups表的常用操作
        public SimpleClient<LoginLogs> LoginLogsDb { get { return new SimpleClient<LoginLogs>(Db); } }//用来处理LoginLogs表的常用操作
        public SimpleClient<Logs> LogsDb { get { return new SimpleClient<Logs>(Db); } }//用来处理Logs表的常用操作
        public SimpleClient<Messages> MessagesDb { get { return new SimpleClient<Messages>(Db); } }//用来处理Messages表的常用操作
        public SimpleClient<NavigationRole> NavigationRoleDb { get { return new SimpleClient<NavigationRole>(Db); } }//用来处理NavigationRole表的常用操作
        public SimpleClient<Navigations> NavigationsDb { get { return new SimpleClient<Navigations>(Db); } }//用来处理Navigations表的常用操作
        public SimpleClient<Notifications> NotificationsDb { get { return new SimpleClient<Notifications>(Db); } }//用来处理Notifications表的常用操作
        public SimpleClient<RejectUsers> RejectUsersDb { get { return new SimpleClient<RejectUsers>(Db); } }//用来处理RejectUsers表的常用操作
        public SimpleClient<ResetUsers> ResetUsersDb { get { return new SimpleClient<ResetUsers>(Db); } }//用来处理ResetUsers表的常用操作
        public SimpleClient<RoleApp> RoleAppDb { get { return new SimpleClient<RoleApp>(Db); } }//用来处理RoleApp表的常用操作
        public SimpleClient<RoleGroup> RoleGroupDb { get { return new SimpleClient<RoleGroup>(Db); } }//用来处理RoleGroup表的常用操作
        public SimpleClient<Roles> RolesDb { get { return new SimpleClient<Roles>(Db); } }//用来处理Roles表的常用操作
        public SimpleClient<Tasks> TasksDb { get { return new SimpleClient<Tasks>(Db); } }//用来处理Tasks表的常用操作
        public SimpleClient<Traces> TracesDb { get { return new SimpleClient<Traces>(Db); } }//用来处理Traces表的常用操作
        public SimpleClient<UserGroup> UserGroupDb { get { return new SimpleClient<UserGroup>(Db); } }//用来处理UserGroup表的常用操作
        public SimpleClient<UserRole> UserRoleDb { get { return new SimpleClient<UserRole>(Db); } }//用来处理UserRole表的常用操作
        public SimpleClient<Users> UsersDb { get { return new SimpleClient<Users>(Db); } }//用来处理Users表的常用操作


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }


        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        public virtual List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel, orderByExpression, orderByType);
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual T GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.Delete(id);
        }


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(T data)
        {
            return CurrentDb.Delete(data);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic[] ids)
        {
            return CurrentDb.AsDeleteable().In(ids).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual bool UpdateFixedColumn(T obj, Expression<Func<T, object>> columns)
        {
            return Db.Updateable(obj).UpdateColumns(columns).ExecuteCommand() > 0;
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<T> objs)
        {
            return CurrentDb.UpdateRange(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(T obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<T> objs)
        {
            return CurrentDb.InsertRange(objs);
        }


        //自已扩展更多方法 
    }

}