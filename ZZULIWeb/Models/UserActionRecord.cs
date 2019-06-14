using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public static class UserActionRecord
    {
        static ZZULIEntities db = new ZZULIEntities();
        //记录用户的推荐操作
        /// <summary>
        /// 记录用户的推荐操作
        /// </summary>
        /// <param name="u_id">用户ID</param>
        /// <param name="target_id">操作对象ID</param>
        /// <param name="type_id">操作对象类型</param>
        /// <returns></returns>
        public static bool Recommend(int u_id, int target_id, int type_id)
        {
            UserLikes data = new UserLikes() { User_ID = u_id, UL_Type = type_id, UL_Target = target_id, UL_Time = DateTime.Now };
            db.UserLikes.Add(data);
            return db.SaveChanges() > 0 ? true : false;
        }

        //记录用户不推荐操作
        /// <summary>
        /// 记录用户不推荐操作
        /// </summary>
        /// <param name="u_id">用户ID</param>
        /// <param name="target_id">操作对象ID</param>
        /// <param name="type_id">操作对象类型</param>
        /// <returns></returns>
        public static bool NotRecommend(int u_id, int target_id, int type_id)
        {
            UserNotLikes data = new UserNotLikes() { User_ID = u_id, UNL_Type = type_id, UNL_Target = target_id, UNL_Time = DateTime.Now };
            db.UserNotLikes.Add(data);
            return db.SaveChanges() > 0 ? true : false;
        }

        //取消推荐操作
        /// <summary>
        /// //取消推荐操作
        /// </summary>
        /// <param name="u_id">用户ID</param>
        /// <param name="target_id">操作对象ID</param>
        /// <param name="type_id">操作对象类型</param>
        /// <returns></returns>
        public static bool DelRecommend(int u_id, int target_id, int type_id)
        {
            var data = db.UserLikes.Where(ul => ul.User_ID == u_id && ul.UL_Target == target_id && ul.UL_Type == type_id).FirstOrDefault();
            db.UserLikes.Remove(data);
            return db.SaveChanges() > 0 ? true : false;
        }

        //取消不推荐操作
        /// <summary>
        /// 取消不推荐操作
        /// </summary>
        /// <param name="u_id">用户ID</param>
        /// <param name="target_id">操作对象ID</param>
        /// <param name="type_id">操作对象类型</param>
        /// <returns></returns>
        public static bool DelNotRecommend(int u_id, int target_id, int type_id)
        {
            var data = db.UserNotLikes.Where(ul => ul.User_ID == u_id && ul.UNL_Target == target_id && ul.UNL_Type == type_id).FirstOrDefault();
            db.UserNotLikes.Remove(data);
            return db.SaveChanges() > 0 ? true : false;
        }

        //用户收藏操作
        public static bool Collect(int u_id, int target_id, int type_id)
        {
            db.UserCollections.Add(new UserCollections() { UC_Type = type_id, User_ID = u_id, UC_Target = target_id, UC_Time = DateTime.Now });
            return db.SaveChanges() > 0 ? true : false;
        }
        //用户取消收藏操作
        public static bool AbortCollect(int u_id, int target_id, int type_id)
        {
            var data = db.UserCollections.Where(u => u.User_ID == u_id && u.UC_Target == target_id && u.UC_Type == type_id).FirstOrDefault();
            db.UserCollections.Remove(data);
            return db.SaveChanges() > 0 ? true : false;
        }
        /// <summary>
        /// 用户关注分类
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="target_id"></param>
        /// <returns></returns>
        public static bool Follow(int u_id, int target_id)
        {
            db.UserFollows.Add(new UserFollows() { User_ID = u_id, Nav_ID = target_id, UF_Time = DateTime.Now });
            return db.SaveChanges() > 0 ? true : false;
        }
        /// <summary>
        /// 用户取消关注分类
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="target_id"></param>
        /// <returns></returns>
        public static bool AbortFollow(int u_id, int target_id)
        {
            var data = db.UserFollows.Where(u => u.User_ID == u_id && u.Nav_ID == target_id).FirstOrDefault();
            db.UserFollows.Remove(data);
            return db.SaveChanges() > 0 ? true : false;
        }
    }
}