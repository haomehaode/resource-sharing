using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ZZULIWeb.Models
{
    public class User : DataBase<UserInfo>
    {
        #region 根据用户ID获取数量

        //根据用户ID获取用户的笔记数量
        //
        public int GetNoteNumByUID(int user_id)
        {
            return base.EF.Notes.Where(n => n.User_ID == user_id).Count();
        }

        /// <summary>
        /// 根据用户ID获取用户笔记评论量
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns></returns>
        public int GetNoteCommentNumByUID(int user_id)
        {
            return base.EF.NotesComments.Where(n => n.User_ID == user_id).Count();
        }

        /// <summary>
        /// 根据用户ID获取推荐量
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetNoteLikeNumByUID(int user_id)
        {
            return base.EF.UserLikes.Where(u => u.User_ID == user_id && u.UL_Type == 16).Count();
        }


        /// <summary>
        /// 根据用户ID获取用户收藏笔记的数量
        /// </summary>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public int GetNoteCollectionNumByUID(int u_id)
        {
            return base.EF.UserCollections.Where(u => u.User_ID == u_id && u.UC_Type == 10).Count();
        }


        /// <summary>
        /// 根据用户ID获取用户的提问数量
        /// </summary>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public int GetQuestionNumByUID(int u_id)
        {
            return base.EF.Questions.Where(q => q.User_ID == u_id).Count();
        }

        /// <summary>
        /// 根据用户ID获取用户的回答数量
        /// </summary>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public int GetAnswerNumByUID(int u_id)
        {
            return base.EF.Answers.Where(a => a.User_ID == u_id && a.A_Target == -1).Count();
        }

        /// <summary>
        /// 根据用户ID获取用户的收藏问题数量
        /// </summary>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public int GetQuestionCollectionNumByUID(int u_id)
        {
            return base.EF.UserCollections.Where(u => u.User_ID == u_id && u.UC_Type == 9).Count();
        }


        /// <summary>
        /// 根据用户ID获取用户关注量
        /// </summary>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public int GetFollowNumByUID(int u_id)
        {
            return base.EF.UserFollows.Where(u => u.User_ID == u_id).Count();
        }

        #endregion

        public void ModifyLastLoginTime(int uid)
        {
            var user = base.EF.UserInfo.Where(u => u.User_ID == uid).FirstOrDefault();
            user.User_LastLoginTime = DateTime.Now;
            base.EF.SaveChanges();
        }

        #region 用户学习过的课程

        /// <summary>
        /// 根据用户ID获取用户最近学习的课程
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public List<UserLearnCoursesRecord> GetLearnedCourse(int u_id)
        {
            return base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == u_id).ToList();
            //return base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == u_id).OrderByDescending(u=>u.ULC_Time).ToList();
            //return base.EF.Courses.Where(c => couid.Contains(c.Cou_ID)).ToList();
            //用户学习课程的ID
            //return base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == u_id).OrderByDescending(u => u.ULC_Time).ToList();
        }
        /// <summary>
        /// 根据用户ID获取用户最近学习的课程
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public List<UserLearnCoursesRecord> GetLearnedCourse_TopN(int u_id, int topN = 10)
        {
            return base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == u_id).OrderByDescending(u => u.ULC_Time).Skip(0).Take(topN).ToList();
            //var couid = base.EF.UserLearnCoursesRecord.OrderByDescending(u => u.ULC_Time).GroupBy(u => u.Cou_ID).Skip(0).Take(topN).Select(u => u.Key);

            //return base.EF.UserLearnCoursesRecord.Where(u => couid.Contains(u.Cou_ID)).OrderByDescending(u => u.ULC_Time).ToList();
            //return base.EF.Courses.Where(c => couid.Contains(c.Cou_ID)).Skip(0).Take(topN).ToList();
            //用户学习课程的ID
            //return base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == u_id).OrderByDescending(u => u.ULC_Time).Skip(0).Take(topN).ToList();
        }

        public List<UserLearnCoursesRecord> GetLearnedCourse_Page(int u_id, int pageIndex = 1, int pageSize = 10)
        {
            return base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == u_id).OrderByDescending(u => u.ULC_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //var couid = base.EF.UserLearnCoursesRecord.OrderByDescending(u => u.ULC_Time).GroupBy(u => u.Cou_ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(u => u.Key);
            //return base.EF.UserLearnCoursesRecord.Where(u => couid.Contains(u.Cou_ID)).OrderByDescending(u => u.ULC_Time).ToList();

            //return base.EF.Courses.Where(c => couid.Contains(c.Cou_ID)).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //return base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == u_id).OrderByDescending(u => u.ULC_Time).ToList();
        }

        public List<UserLearnSectionRecord> GetLearnMoveIDByCouID(int uid, int couid)
        {
            return base.EF.UserLearnSectionRecord.Where(u => u.User_ID == uid && u.Cou_ID == couid).ToList();
        }

        /// <summary>
        /// 根据用户ID，获取用户的课程问题
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<CoursesQuestions> GetCourseQuestion(int uid)
        {
            return base.EF.CoursesQuestions.Where(c => c.User_ID == uid).ToList();
        }

        /// <summary>
        /// 分页查询用户的课程问题
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<CoursesQuestions> GetCourseQuestion_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            return base.EF.CoursesQuestions.Where(c => c.User_ID == uid).OrderByDescending(c => c.CQ_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }


        /// <summary>
        /// 根据用户ID获取课程笔记
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<CoursesNotes> GetCourseNote(int uid)
        {
            return base.EF.CoursesNotes.Where(c => c.User_ID == uid).ToList();
        }

        /// <summary>
        /// 根据用户ID分页获取课程笔记
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<CoursesNotes> GetCourseNote_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            return base.EF.CoursesNotes.Where(c => c.User_ID == uid).OrderByDescending(c => c.CN_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据用户ID获取用户收藏的课程
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<Courses> GetCourseCollectionByUID(int uid)
        {
            var ids = base.EF.UserCollections.Where(u => u.User_ID == uid && u.UC_Type == 8).Select(u => u.UC_Target);
            return base.EF.Courses.Where(c => ids.Contains(c.Cou_ID)).OrderByDescending(c => c.Cou_Time).ToList();
        }

        /// <summary>
        /// 根据用户id分页获取用户收藏课程
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Courses> GetCourseCollection_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            var ids = base.EF.UserCollections.Where(u => u.User_ID == uid && u.UC_Type == 8).OrderByDescending(u => u.UC_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(u => u.UC_Target);
            return base.EF.Courses.Where(c => ids.Contains(c.Cou_ID)).OrderByDescending(c => c.Cou_Time).ToList();
        }

        #endregion

        ///// <summary>
        ///// 验证用户输入的账号和密码，如果通过验证，返回匹配信息
        ///// </summary>
        ///// <param name="account"></param>
        ///// <param name="pwd"></param>
        ///// <returns></returns>
        //public UserInfo Validate(string account, string pwd)
        //{
        //    var list = base.EF.UserInfo.Where(u => u.User_Account == account && u.User_Password == pwd);
        //    if (list.Count() > 0)
        //        return list.FirstOrDefault();
        //    return null; ;
        //}

        /// <summary>
        /// 获取用户学习的进度
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="couid"></param>
        /// <returns></returns>
        public double GetPrecentLearned(int uid, int couid)
        {
            //获取课程的总的节数
            var section = base.EF.Move.Where(m => m.Cou_ID == couid && m.Is_Chapter == false);
            //获取用户已经学的节数
            var learn = base.EF.UserLearnSectionRecord.Where(u => u.User_ID == uid && u.Cou_ID == couid && u.Is_Finish == true);
            return Convert.ToDouble((learn.Count() * 1.0 / section.Count()).ToString("0.00")) * 100;
        }

        #region 用户笔记

        /// <summary>
        /// 根据用户的ID获取用户发布的笔记评论
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<NotesComments> GetNoteCommentsByUID(int uid)
        {
            return base.EF.NotesComments.Where(n => n.User_ID == uid).ToList();
        }

        /// <summary>
        /// 根据用户的ID分页获取用户发布的笔记评论
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<NotesComments> GetNoteCommentsByUID_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            return base.EF.NotesComments.Where(n => n.User_ID == uid).OrderByDescending(n => n.NC_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Notes> GetNoteLike_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            var ids = base.EF.UserLikes.Where(u => u.User_ID == uid && u.UL_Type == 16).OrderByDescending(u => u.UL_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(u => u.UL_Target);
            return base.EF.Notes.Where(n => ids.Contains(n.N_ID)).OrderByDescending(n => n.N_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Notes> GetNoteCollect_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            var ids = base.EF.UserCollections.Where(u => u.User_ID == uid && u.UC_Type == 10).OrderByDescending(u => u.UC_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(u => u.UC_Target);
            return base.EF.Notes.Where(n => ids.Contains(n.N_ID)).OrderByDescending(n => n.N_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        #endregion

        #region 用户问题

        /// <summary>
        /// 根据用户ID分页获取用户对问题的回复
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Answers> GetAnswerByUID(int uid, int pageIndex = 1, int pageSize = 10)
        {
            return base.EF.Answers.Where(a => a.User_ID == uid && a.A_Target == -1).OrderByDescending(a => a.A_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Questions> GetQuestionCollectByUID(int uid)
        {
            var ids = base.EF.UserCollections.Where(u => u.User_ID == uid && u.UC_Type == 9).Select(u => u.UC_Target);
            return base.EF.Questions.Where(q => ids.Contains(q.Q_ID)).ToList();
        }

        public List<Questions> GetQuestionCollectByUID_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            var ids = base.EF.UserCollections.Where(u => u.User_ID == uid && u.UC_Type == 9).OrderByDescending(u => u.UC_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(u => u.UC_Target);
            return base.EF.Questions.Where(q => ids.Contains(q.Q_ID)).OrderByDescending(q => q.Q_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        #endregion


        public List<Follow> GetMyFollowByUID(int uid)
        {
            return base.EF.Follow.Where(f => f.User_ID == uid).ToList();
        }
        public List<Follow> GetMyFollowByUID_Page(int uid, int pageIndex = 1, int pageSize = 10)
        {
            return base.EF.Follow.Where(f => f.User_ID == uid).OrderByDescending(f => f.F_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Courses> GetRecommendCourse(int uid, int topN = 5)
        {
            //获取得分最好的标签ID
            var navids = base.EF.NewAry.Where(n => n.User_ID == uid).OrderByDescending(n => n.Ary_Sco).Select(n => n.Nav_ID).Skip(0).Take(5);

            //获取子标签
            List<int> ids = Common.GetAllTags(navids.ToList());

            //获取学习过的课程
            List<int> learnids = base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == uid).Select(u => u.Cou_ID).ToList();

            //获取贴有指定标签，用户没有学过的课程的ID
            var courseids = base.EF.ObjectTags.Where(o => o.Obj_Type == 32 && ids.Contains(o.Obj_TagID) && !learnids.Contains(o.Obj_ID)).Select(o => o.Obj_ID);

            return base.EF.Courses.Where(c => courseids.Contains(c.Cou_ID)).OrderByDescending(c => c.Cou_Hot).Skip(0).Take(topN).ToList();
        }
        public List<Questions> GetRecommendQuestion(int uid, int topN = 5)
        {
            //获取得分最好的标签ID
            var navids = base.EF.NewAry.Where(n => n.User_ID == uid).OrderByDescending(n => n.Ary_Sco).Select(n => n.Nav_ID).Skip(0).Take(5);

            //获取子标签
            List<int> ids = Common.GetAllTags(navids.ToList());

            ////获取学习过的课程
            //List<int> learnids = base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == uid).Select(u => u.Cou_ID).ToList();

            //获取贴有指定标签，用户没有学过的课程的ID
            var qids = base.EF.ObjectTags.Where(o => o.Obj_Type == 33 && ids.Contains(o.Obj_TagID)).Select(o => o.Obj_ID);

            //return base.EF.Courses.Where(c => courseids.Contains(c.Cou_ID)).OrderByDescending(c => c.Cou_Hot).Skip(0).Take(topN).ToList();
            return base.EF.Questions.Where(q => qids.Contains(q.Q_ID)).OrderByDescending(q => q.Q_Hot).Skip(0).Take(topN).ToList();
        }
        public List<Notes> GetRecommendNote(int uid, int topN = 5)
        {
            //获取得分最好的标签ID
            var navids = base.EF.NewAry.Where(n => n.User_ID == uid).OrderByDescending(n => n.Ary_Sco).Select(n => n.Nav_ID).Skip(0).Take(5);

            //获取子标签
            List<int> ids = Common.GetAllTags(navids.ToList());

            ////获取学习过的课程
            //List<int> learnids = base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == uid).Select(u => u.Cou_ID).ToList();

            //获取贴有指定标签，用户没有学过的课程的ID
            var nids = base.EF.ObjectTags.Where(o => o.Obj_Type == 34 && ids.Contains(o.Obj_TagID)).Select(o => o.Obj_ID);

            return base.EF.Notes.Where(n => nids.Contains(n.N_ID)).OrderByDescending(n => n.N_Hot).Skip(0).Take(topN).ToList();
        }
    }
}