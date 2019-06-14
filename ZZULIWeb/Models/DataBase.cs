using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Reflection;

namespace ZZULIWeb.Models
{
    public class DataBase<T> where T : class,new()
    {
        protected ZZULIEntities EF = new ZZULIEntities();

        /// <summary>
        /// 查询符合条件的数据，返回列表集合
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public List<T> GetList_Where(Expression<Func<T, bool>> whereLambda)
        {
            List<T> list = EF.Set<T>().Where(whereLambda).ToList();
            return list;
        }

        /// <summary>
        /// 查询符合条件的数据，返回列表集合TopN
        /// </summary>
        /// <typeparam name="TR">排序方法的返回值类型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <param name="topN">TopN</param>
        /// <param name="isOrderByDescending">是否为倒序排列</param>
        /// <returns></returns>
        public List<T> GetList_TopN<TR>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TR>> orderLambda, int topN, bool isOrderByDescending = true)
        {
            var list = EF.Set<T>().Where(whereLambda);
            if (list != null && list.Count() > 0)
            {
                if (!isOrderByDescending)
                    return list.OrderBy(orderLambda).Skip(0).Take(topN).ToList();
                else
                    return list.OrderByDescending(orderLambda).Skip(0).Take(topN).ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从数据库查询分页数据
        /// </summary>
        /// <typeparam name="TR">排序方法的返回值类型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="isOrderByDescending">是否为倒序排列</param>
        /// <returns></returns>
        public List<T> GetList_Page<TR>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TR>> orderLambda, int pageIndex, int pageSize, bool isOrderByDescending = true)
        {
            var list = EF.Set<T>().Where(whereLambda);
            if (list != null && list.Count() > 0)
            {
                if (!isOrderByDescending)
                    return list.OrderBy(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                else
                    return list.OrderByDescending(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 向数据库中添加一条数据
        /// </summary>
        /// <param name="model">要添加数据的实体对象</param>
        /// <returns></returns>
        public int Add(T model)
        {
            EF.Set<T>().Add(model);
            return EF.SaveChanges();
        }

        /// <summary>
        /// 根据ID从数据库中删除数据
        /// </summary>
        /// <param name="model">要删除数据的实体对象（该对象只用包含要删除对象的ID即可）</param>
        /// <returns></returns>
        public int Del(T model)
        {
            EF.Set<T>().Attach(model);
            EF.Set<T>().Remove(model);
            return EF.SaveChanges();
        }

        /// <summary>
        /// 删除满足条件的所有数据
        /// </summary>
        /// <param name="whereLambda">删除条件</param>
        /// <returns></returns>
        public int Del(Expression<Func<T, bool>> whereLambda)
        {
            List<T> list = EF.Set<T>().Where(whereLambda).ToList();
            EF.Set<T>().RemoveRange(list);
            return EF.SaveChanges();
        }

        /// <summary>
        /// 修改数据库中的值
        /// </summary>
        /// <param name="model">修改后的实体对象</param>
        /// <param name="proNames">修改了哪些属性名</param>
        /// <returns></returns>
        public int Modify(T model, params string[] proNames)
        {
            DbEntityEntry entry = EF.Entry<T>(model);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            foreach (string s in proNames)
            {
                entry.Property(s).IsModified = true;
            }
            return EF.SaveChanges();
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model">修改后的实体对象</param>
        /// <param name="whereLambda">筛选条件</param>
        /// <param name="proNames">修改了哪些属性名</param>
        /// <returns></returns>
        public int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] proNames)
        {
            List<T> listModifing = EF.Set<T>().Where(whereLambda).ToList();
            DbEntityEntry entry = EF.Entry<T>(model);

            Type type = typeof(T);
            List<PropertyInfo> proInfos = type.GetProperties().ToList();
            Dictionary<string, PropertyInfo> dicPro = new Dictionary<string, PropertyInfo>();
            proInfos.ForEach(p => dicPro.Add(p.Name, p));

            PropertyInfo proInfo;

            foreach (T t in listModifing)
            {
                foreach (string s in proNames)
                {
                    if (dicPro.ContainsKey(s))
                    {
                        proInfo = dicPro[s];
                        object newValue = proInfo.GetValue(model);
                        proInfo.SetValue(t, newValue);
                    }
                }
            }
            return EF.SaveChanges();
        }
    }
}