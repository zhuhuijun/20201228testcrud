using Sugar.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace LayuiCmsCore.BusinessCore
{
    public partial interface IGroupsManager
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Groups> GetList();

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public List<Groups> GetList(Expression<Func<Groups, bool>> whereExpression);


        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public List<Groups> GetPageList(Expression<Func<Groups, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        public List<Groups> GetPageList(Expression<Func<Groups, bool>> whereExpression
            , PageModel pageModel
            , Expression<Func<Groups, object>> orderByExpression = null
            , OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public Groups GetById(dynamic id);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(dynamic id);


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Groups data);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(dynamic[] ids);

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<Groups, bool>> whereExpression);


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Update(Groups obj);
        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Update(List<Groups> objs);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Insert(Groups obj);


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Insert(List<Groups> objs);
    }
}