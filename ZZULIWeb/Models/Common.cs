using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZZULIWeb.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Threading;

namespace ZZULIWeb.Models
{
    static public class Common
    {
        static ZZULIEntities db = null;

        //防止共用上下文，导致错误
        private static ZZULIEntities GetEntity()
        {
            if (db == null)
            {
                return new ZZULIEntities();
            }
            switch (db.Database.Connection.State)
            {
                case System.Data.ConnectionState.Broken:
                case System.Data.ConnectionState.Closed:
                    return db;
                case System.Data.ConnectionState.Connecting:
                case System.Data.ConnectionState.Executing:
                case System.Data.ConnectionState.Fetching:
                case System.Data.ConnectionState.Open:
                    return new ZZULIEntities();
                default:
                    return new ZZULIEntities();
            }
        }

        //长轮询
        public static bool Link(string type)
        {
            ZZULIEntities ef = GetEntity();

            DateTime start = DateTime.Now;
            switch (type)
            {
                case "cc":
                    while (true)
                    {

                        var data = ef.CoursesComment.Where(c => c.CCom_Time > start);
                        if (data != null && data.Count() > 0)
                        {
                            return true;
                        }
                        Thread.Sleep(2000);
                    }
                case "cn":
                    while (true)
                    {

                        var data = ef.CoursesNotes.Where(n => n.CN_Time > start);
                        if (data != null && data.Count() > 0)
                        {
                            return true;
                        }
                        Thread.Sleep(2000);
                    }
                case "cq":
                    while (true)
                    {

                        var data = ef.CoursesQuestions.Where(n => n.CQ_Time > start);
                        if (data != null && data.Count() > 0)
                        {
                            return true;
                        }
                        Thread.Sleep(2000);
                    }
                default:
                    return false;
            }
        }

        //获取指定时间距离现的时间
        public static string GetTimeStringToNow(DateTime date)
        {
            TimeSpan ts = DateTime.Now - date;
            string res = ts.TotalSeconds < 60 ? "刚刚" : ts.TotalMinutes < 60 ? Convert.ToInt32(ts.TotalMinutes) + "分钟前" : ts.TotalHours < 24 ? Convert.ToInt32(ts.TotalHours) + "小时前" : ts.TotalDays < 30 ? Convert.ToInt32(ts.TotalDays) + "天前" : ts.TotalDays / 30.0 < 12 ? Convert.ToInt32(ts.TotalDays / 30.0) + "个月前" : ts.TotalDays < 365 ? Convert.ToInt32(ts.TotalDays / 30.0) + "个月前" : DateTime.Now.Year - date.Year + "年前";
            return res;
        }

        public static string GetChineseOfNum(int num)
        {
            if (num == 0)
            {
                return "";
            }
            string res = "";
            switch (num.ToString().Length)
            {
                case 1:
                    res = Chinese(num);
                    break;
                case 2:
                    res = (num / 10 == 1 ? "" : Chinese(num / 10)) + "十" + (num % 10 == 0 ? "" : Chinese(num % 10));
                    break;
                case 3:
                    res = Chinese(num / 100) + "百" + (((num / 10) % 10) == 0 ? (num % 10 == 0 ? "" : "零") : Chinese(num / 10 % 10) + "十") + (num % 10 == 0 ? "" : Chinese(num % 10));
                    break;
                case 4:
                    break;
                default:
                    break;
            }
            return res;
        }
        static string Chinese(int num)
        {
            string res = "";
            switch (num)
            {
                case 0:
                    res = "零";
                    break;
                case 1:
                    res = "一";
                    break;
                case 2:
                    res = "二";
                    break;
                case 3:
                    res = "三";
                    break;
                case 4:
                    res = "四";
                    break;
                case 5:
                    res = "五";
                    break;
                case 6:
                    res = "六";
                    break;
                case 7:
                    res = "七";
                    break;
                case 8:
                    res = "八";
                    break;
                case 9:
                    res = "九";
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="target_id"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool Like(int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            bool res = false;
            switch (optionType)
            {
                case 11://课程
                    var course = ef.Courses.Where(cou => cou.Cou_ID == target_id).FirstOrDefault();
                    course.Cou_Likes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseHotByCou_ID(target_id);
                        res = true;
                    }
                    break;
                case 12://课程评论
                    var ccom = ef.CoursesComment.Where(cc => cc.CCom_ID == target_id).FirstOrDefault();
                    ccom.CCom_Likes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseHotByCou_ID(target_id);
                        res = true;
                    }
                    break;
                case 13://课程问题回复
                    var couA = ef.CoursesAnswers.Where(ca => ca.CA_ID == target_id).FirstOrDefault();
                    couA.CA_Likes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseQuestionHot(target_id);
                        res = true;
                    }
                    break;
                case 14://课程笔记
                    var couN = ef.CoursesNotes.Where(cn => cn.CN_ID == target_id).FirstOrDefault();
                    couN.CN_Likes++;
                    if (ef.SaveChanges() > 0)
                    {
                        //更新课程笔记热度
                        ef.SP_UpdateCourseNoteHot(target_id);
                        res = true;
                    }
                    break;
                case 15://问题回复
                    var answer = ef.Answers.Where(a => a.A_ID == target_id).FirstOrDefault();
                    answer.A_Likes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateQuestionHotByQ_ID(target_id);
                        res = true;
                    }
                    break;
                case 16://笔记
                    var note = ef.Notes.Where(n => n.N_ID == target_id).FirstOrDefault();
                    note.N_Likes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateNoteHotByN_ID(target_id);
                        res = true;
                    }

                    break;
                case 17://笔记评论
                    var ncom = ef.NotesComments.Where(nc => nc.NC_ID == target_id).FirstOrDefault();
                    ncom.NC_Likes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateNoteHotByN_ID(target_id);
                        res = true;
                    }

                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="target_id"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool AbortLike(int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            bool res = false;
            switch (optionType)
            {
                case 11://课程
                    var course = ef.Courses.Where(cou => cou.Cou_ID == target_id).FirstOrDefault();
                    course.Cou_Likes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseHotByCou_ID(target_id);
                        res = true;
                    }
                    break;
                case 12://课程评论
                    var ccom = ef.CoursesComment.Where(cc => cc.CCom_ID == target_id).FirstOrDefault();
                    ccom.CCom_Likes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseHotByCou_ID(target_id);
                        res = true;
                    }
                    break;
                case 13://课程问题回复
                    var couA = ef.CoursesAnswers.Where(ca => ca.CA_ID == target_id).FirstOrDefault();
                    couA.CA_Likes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseQuestionHot(target_id);
                        res = true;
                    }
                    break;
                case 14://课程笔记
                    var couN = ef.CoursesNotes.Where(cn => cn.CN_ID == target_id).FirstOrDefault();
                    couN.CN_Likes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseNoteHot(target_id);
                        res = true;
                    }
                    break;
                case 15://问题回复
                    var answer = ef.Answers.Where(a => a.A_ID == target_id).FirstOrDefault();
                    answer.A_Likes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateQuestionHotByQ_ID(target_id);
                        res = true;
                    }
                    break;
                case 16://笔记
                    var note = ef.Notes.Where(n => n.N_ID == target_id).FirstOrDefault();
                    note.N_Likes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateNoteHotByN_ID(target_id);
                        res = true;
                    }
                    break;
                case 17://笔记评论
                    var ncom = ef.NotesComments.Where(nc => nc.NC_ID == target_id).FirstOrDefault();
                    ncom.NC_Likes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateNoteHotByN_ID(target_id);
                        res = true;
                    }
                    break;
                default:
                    break;
            }
            return res;
        }


        /// <summary>
        /// 踩
        /// </summary>
        /// <param name="target_id"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool NotLike(int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            bool res = false;
            switch (optionType)
            {
                case 18://课程
                    var course = ef.Courses.Where(c => c.Cou_ID == target_id).FirstOrDefault();
                    course.Cou_NotLikes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseHotByCou_ID(target_id);
                        res = true;
                    }
                    break;
                case 19://问题回复
                    var answer = ef.Answers.Where(a => a.A_ID == target_id).FirstOrDefault();
                    answer.A_NotLikes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateQuestionHotByQ_ID(target_id);
                        res = true;
                    }
                    break;
                case 20://笔记
                    var note = ef.Notes.Where(n => n.N_ID == target_id).FirstOrDefault();
                    note.N_NotLikes++;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateNoteHotByN_ID(target_id);
                        res = true;
                    }
                    break;
                default:
                    res = false;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 取消踩
        /// </summary>
        /// <param name="target_id"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool AbortNotLike(int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            bool res = false;
            switch (optionType)
            {
                case 18://课程
                    var course = ef.Courses.Where(c => c.Cou_ID == target_id).FirstOrDefault();
                    course.Cou_NotLikes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateCourseHotByCou_ID(target_id);
                        res = true;
                    }
                    break;
                case 19://问题回复
                    var answer = ef.Answers.Where(a => a.A_ID == target_id).FirstOrDefault();
                    answer.A_NotLikes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateQuestionHotByQ_ID(target_id);
                        res = true;
                    }
                    break;
                case 20://笔记
                    var note = ef.Notes.Where(n => n.N_ID == target_id).FirstOrDefault();
                    note.N_NotLikes--;
                    if (ef.SaveChanges() > 0)
                    {
                        ef.SP_UpdateNoteHotByN_ID(target_id);
                        res = true;
                    }
                    break;
                default:
                    res = false;
                    break;
            }
            return res;
        }

        //private static UserInfo userInfo;
        private static UserInfo userInfo;
        public static UserInfo UserInfo
        {
            get
            {
                if (userInfo == null)
                {
                    if (System.Web.HttpContext.Current.Session["UserInfo"] == null)//session 失效
                    {
                        HttpCookie cook = System.Web.HttpContext.Current.Request.Cookies["UserInfo"];
                        if (cook != null)//自动登录
                        {
                            HttpCookie cookie = new HttpCookie("UserInfo");
                            cookie.Expires = DateTime.Now.AddDays(30);
                            cookie.Value = System.Web.HttpContext.Current.Request.Cookies["UserInfo"].Value;
                            System.Web.HttpContext.Current.Request.Cookies.Add(cookie);

                            System.Web.HttpContext.Current.Session.Timeout = 30;
                            System.Web.HttpContext.Current.Session.Add("UserInfo", System.Web.HttpContext.Current.Request.Cookies["UserInfo"].Value);

                            int uid = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserInfo"]);
                            userInfo = new ZZULIEntities().UserInfo.Where(u => u.User_ID == uid).FirstOrDefault();
                        }
                    }
                    else
                    {
                        int uid = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserInfo"]);
                        userInfo = new ZZULIEntities().UserInfo.Where(u => u.User_ID == uid).FirstOrDefault();
                    }
                }
                //if (System.Web.HttpContext.Current.Session["UserInfo"] == null && userInfo == null)//未登录
                //{
                //}
                //else if (userInfo == null)//Session 存在，userinfo为空        中途关闭网站
                //{
                //    int uid = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserInfo"]);
                //    userInfo = new ZZULIEntities().UserInfo.Where(u => u.User_ID == uid).FirstOrDefault();
                //}
                //else if (System.Web.HttpContext.Current.Session["UserInfo"] == null) //userinfo 不为空，Session为空  登录失效
                //{
                //    System.Web.HttpContext.Current.Session.Timeout = 30;
                //    System.Web.HttpContext.Current.Session.Add("UserInfo", userInfo.User_ID);
                //}
                //else//Session 存在    userinfo存在          登录状态
                //{

                //}
                return userInfo;
            }
            set { userInfo = value; }
            //set { userInfo = value; }
        }

        public static bool Collect(int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            switch (optionType)
            {
                case 8:
                    var course = ef.Courses.Where(c => c.Cou_ID == target_id).FirstOrDefault();
                    course.Cou_CollectNum++;
                    break;
                case 9:
                    var question = ef.Questions.Where(q => q.Q_ID == target_id).FirstOrDefault();
                    question.Q_CollectNum++;
                    break;
                case 10:
                    var note = ef.Notes.Where(n => n.N_ID == target_id).FirstOrDefault();
                    note.N_CollectNum++;
                    break;
                default:
                    break;
            }
            if (ef.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public static bool AbortCollect(int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            var data = ef.UserCollections.Where(u => u.UC_Target == target_id && u.UC_Type == optionType && u.User_ID == Common.UserInfo.User_ID).FirstOrDefault();
            ef.UserCollections.Remove(data);

            switch (optionType)
            {
                case 8:
                    var course = ef.Courses.Where(c => c.Cou_ID == target_id).FirstOrDefault();
                    course.Cou_CollectNum--;
                    break;
                case 9:
                    var question = ef.Questions.Where(q => q.Q_ID == target_id).FirstOrDefault();
                    question.Q_CollectNum--;
                    break;
                case 10:
                    var note = ef.Notes.Where(n => n.N_ID == target_id).FirstOrDefault();
                    note.N_CollectNum--;
                    break;
                default:
                    break;
            }
            if (ef.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 记录用户的浏览行为
        /// </summary>
        /// <param name="uid">用户id，如果用户尚未登录，则为-1</param>
        /// <param name="target">浏览的对象</param>
        /// <param name="optionType">浏览对象的类型</param>
        public static bool PageView(int target, int optionType, int uid = 0)
        {
            ZZULIEntities ef = GetEntity();
            if (uid == 0)
            {
                ef.UserPageViews.Add(new UserPageViews() { UPV_Target = target, UPV_Type = optionType, UPV_Time = DateTime.Now });
            }
            else
            {
                ef.UserPageViews.Add(new UserPageViews() { UPV_Target = target, UPV_Type = optionType, User_ID = uid, UPV_Time = DateTime.Now });

            }
            return ef.SaveChanges() > 0 ? true : false;
        }

        /// <summary>
        /// 修改浏览量的值
        /// </summary>
        /// <param name="target"></param>
        /// <param name="optionType"></param>
        /// <returns></returns>
        public static bool ModifyPageView(int target, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            switch (optionType)
            {
                case 1://浏览课程
                    break;
                case 2://浏览问题
                    var question = ef.Questions.Where(q => q.Q_ID == target).FirstOrDefault();
                    question.Q_PageViews++;
                    break;
                case 3://浏览笔记
                    var note = ef.Notes.Where(n => n.N_ID == target).FirstOrDefault();
                    note.N_PageViews++;
                    break;
                default:
                    break;
            }
            return ef.SaveChanges() > 0 ? true : false;
        }

        /// <summary>
        /// 根据目标和操作类型，判断用户是否已经执行过该操作，比如是否已经为某课程点赞
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="target_id"></param>
        /// <param name="optionType"></param>
        /// <returns></returns>
        public static bool IsLike(int u_id, int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            var data = ef.UserLikes.Where(u => u.User_ID == u_id && u.UL_Target == target_id && u.UL_Type == optionType);
            if (data != null && data.Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据目标和操作类型，判断用户是否已经执行过该操作，比如是否已经不推荐某课程
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="target_id"></param>
        /// <param name="optionType"></param>
        /// <returns></returns>
        public static bool IsNotLike(int u_id, int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            if (ef.UserNotLikes.Where(u => u.User_ID == u_id && u.UNL_Target == target_id && u.UNL_Type == optionType).Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据目标和操作类型，判断用户是否已经执行过该操作，比如是否已经收藏某课程
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="target_id"></param>
        /// <param name="optionType"></param>
        /// <returns></returns>
        public static bool IsCollect(int u_id, int target_id, int optionType)
        {
            ZZULIEntities ef = GetEntity();
            if (ef.UserCollections.Where(u => u.User_ID == u_id && u.UC_Target == target_id && u.UC_Type == optionType).Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 关注分类
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="target_id"></param>
        /// <returns></returns>
        public static bool Follow(int u_id, int target_id)
        {
            ZZULIEntities ef = GetEntity();
            var data = ef.Navigations.Where(n => n.Nav_ID == target_id).FirstOrDefault();
            data.Nav_FollowNum++;
            return ef.SaveChanges() > 0 ? true : false;
        }
        /// <summary>
        /// 取消关注分类
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="target_id"></param>
        /// <returns></returns>
        public static bool AbortFollow(int u_id, int target_id)
        {
            ZZULIEntities ef = GetEntity();
            var data = ef.Navigations.Where(n => n.Nav_ID == target_id).FirstOrDefault();
            data.Nav_FollowNum--;
            return ef.SaveChanges() > 0 ? true : false;
        }

        /// <summary>
        /// 根据tag字符串获取tag对应的int型id
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        static int GetTagsID(string tag)
        {
            ZZULIEntities ef = GetEntity();
            return ef.Navigations.Where(n => n.Nav_Name == tag).FirstOrDefault().Nav_ID;
        }
        //______________________________________________________课程评论
        /// <summary>
        /// 提交课程评论
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="couid">课程ID</param>
        /// <param name="movid">章节ID</param>
        /// <param name="content">评论内容</param>
        /// <returns></returns>
        public static bool Submit_CourseComment(int uid, int couid, int movid, string content)
        {
            ZZULIEntities ef = GetEntity();
            CoursesComment cc = new CoursesComment() { User_ID = uid, Cou_ID = couid, Mov_ID = movid, CCom_Content = content, CCom_Likes = 0, CCom_Time = DateTime.Now };
            ef.CoursesComment.Add(cc);
            return ef.SaveChanges() > 0 ? true : false;
        }
        /// <summary>
        /// 提交课程评论，返回新增数据的ID
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="couid"></param>
        /// <param name="movid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int Submit_CourseComment(int uid, int couid, int movid, string content, bool getNewID = true)
        {
            ZZULIEntities ef = GetEntity();
            CoursesComment cc = new CoursesComment() { User_ID = uid, Cou_ID = couid, Mov_ID = movid, CCom_Content = content, CCom_Likes = 0, CCom_Time = DateTime.Now };
            ef.CoursesComment.Add(cc);
            ef.SaveChanges();
            return cc.CCom_ID;
        }

        //______________________________________________________课程笔记

        public static bool Submit_CourseNote(int uid, int couid, int movid, string content)
        {
            ZZULIEntities ef = GetEntity();
            CoursesNotes cn = new CoursesNotes() { User_ID = uid, Cou_ID = couid, Mov_ID = movid, CN_Content = content, CN_Time = DateTime.Now };
            ef.CoursesNotes.Add(cn);
            return ef.SaveChanges() > 0 ? true : false;
        }

        public static int Submit_CourseNote(int uid, int couid, int movid, string content, bool GetNewID = true)
        {
            ZZULIEntities ef = GetEntity();
            CoursesNotes cn = new CoursesNotes() { User_ID = uid, Cou_ID = couid, Mov_ID = movid, CN_Content = content, CN_Time = DateTime.Now };
            ef.CoursesNotes.Add(cn);
            ef.SaveChanges();
            return cn.CN_ID;
        }

        //-----------------------------------------------------提交课程问题
        public static bool Submit_CourseQuestion(int uid, int cid, int mid, string title, string content)
        {
            ZZULIEntities ef = GetEntity();
            ef.CoursesQuestions.Add(new CoursesQuestions() { User_ID = uid, Cou_ID = cid, Mov_ID = mid, CQ_Title = title, CQ_Content = content, CQ_Time = DateTime.Now });
            return ef.SaveChanges() > 0 ? true : false;
        }


        //______________________________________________________提交问题

        public static bool Submit_Qeustion(int uid, string title, string content, string content_style, List<string> tags)
        {
            ZZULIEntities ef = GetEntity();
            //提交问题
            Questions ques = new Questions() { User_ID = uid, Q_Title = title, Q_Content = content, Q_Content_Style = content_style, Q_Time = DateTime.Now };
            ef.Questions.Add(ques);
            ef.SaveChanges();
            int qid = ques.Q_ID;

            //提交标签
            var taglist = ef.Navigations.Where(n => n.Nav_ID > 0);
            List<string> exits_id = taglist.Select(n => n.Nav_Name).ToList();
            tags.ForEach(t =>
            {
                //如果用户提交的标签不存在，就将该标签添加到数据库
                if (!exits_id.Contains(t))
                {
                    Navigations nav = new Navigations() { Nav_Name = t, Nav_Type = 3, Nav_UseNum = 1, Ower_ID = 0, Nav_Describe = "", Nav_FollowNum = 0 };
                    ef.Navigations.Add(nav);
                    ef.SaveChanges();
                    int tid = nav.Nav_ID;
                    ef.ObjectTags.Add(new ObjectTags() { Obj_ID = qid, Obj_TagID = tid, Obj_Type = 33 });
                }
                else//如果用户提交的标签已经存在，就将标签的使用量加1
                {
                    var ta = taglist.Single(tag => tag.Nav_Name == t);
                    ef.ObjectTags.Add(new ObjectTags() { Obj_ID = qid, Obj_TagID = ta.Nav_ID, Obj_Type = 33 });
                    ta.Nav_UseNum++;
                }
            });
            //保存用户为问题设置的标签

            return ef.SaveChanges() > 0 ? true : false;
        }



        //-----------------------------------------------------提交笔记
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="content_style"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static bool Submit_Note(int uid, string title, string content, string content_style, List<string> tags)
        {
            ZZULIEntities ef = GetEntity();
            //提交笔记
            Notes note = new Notes() { User_ID = uid, N_Title = title, N_Content = content, N_Content_Style = content_style, N_Time = DateTime.Now, N_IsOriginal = true };
            ef.Notes.Add(note);
            ef.SaveChanges();
            int nid = note.N_ID;

            //提交标签
            var taglist = ef.Navigations.Where(n => n.Nav_ID > 0);
            List<string> exits_id = taglist.Select(n => n.Nav_Name).ToList();
            tags.ForEach(t =>
            {
                //如果用户提交的标签不存在，就将该标签添加到数据库
                if (!exits_id.Contains(t))
                {
                    Navigations nav = new Navigations() { Nav_Name = t, Nav_Type = 3, Nav_UseNum = 1, Ower_ID = 0, Nav_Describe = "", Nav_FollowNum = 0 };
                    ef.Navigations.Add(nav);
                    ef.SaveChanges();
                    int tid = nav.Nav_ID;
                    ef.ObjectTags.Add(new ObjectTags() { Obj_ID = nid, Obj_TagID = tid, Obj_Type = 34 });
                }
                else//如果用户提交的标签已经存在，就将标签的使用量加1
                {
                    var ta = taglist.Single(tag => tag.Nav_Name == t);
                    ef.ObjectTags.Add(new ObjectTags() { Obj_ID = nid, Obj_TagID = ta.Nav_ID, Obj_Type = 34 });
                    ta.Nav_UseNum++;
                }
            });
            //保存用户为笔记设置的标签

            return ef.SaveChanges() > 0 ? true : false;
        }


        //-----------------------------------------------------提交笔记评论
        public static bool Submit_NoteComment(int uid, int nid, string content)
        {
            ZZULIEntities ef = GetEntity();
            ef.NotesComments.Add(new NotesComments() { N_ID = nid, User_ID = uid, NC_Content = content, NC_Time = DateTime.Now });
            return ef.SaveChanges() > 0 ? true : false;
        }

        //-----------------------------------------------------提交问题回复（直接回复）
        public static bool Submit_QuestionAnswer(int uid, int qid, string content)
        {
            ZZULIEntities ef = GetEntity();
            ef.Answers.Add(new Answers() { User_ID = uid, Q_ID = qid, A_Content = content, A_Target = -1, A_Time = DateTime.Now });
            return ef.SaveChanges() > 0 ? true : false;
        }

        //-----------------------------------------------------回复问题的回复
        public static bool Submit_QuestionAnswerOfAnswer(int uid, int qid, int aid, string content)
        {
            ZZULIEntities ef = GetEntity();
            ef.Answers.Add(new Answers() { User_ID = uid, Q_ID = qid, A_Target = aid, A_Content = content, A_Time = DateTime.Now });
            return ef.SaveChanges() > 0 ? true : false;
        }



        /// <summary>
        /// 判断标签是否存在
        /// </summary>
        /// <param name="tag_name"></param>
        /// <returns></returns>
        static bool TagIsExist(string tag_name)
        {
            ZZULIEntities ef = GetEntity();
            var all_tags = ef.Navigations.Where(n => n.Ower_ID != -1);
            var list = (from l in all_tags select new { Nav_ID = l.Nav_ID, Nav_Name = l.Nav_Name }).ToList();

            bool isExist = false;
            list.ForEach(l => { if (l.Nav_Name == tag_name) { isExist = true; } });
            return isExist;

        }


        public static List<int> GetAllTags(int tag_id)
        {
            List<int> res = new List<int>();
            res.Add(tag_id);
            return GetAllTags(res);
        }
        public static List<int> GetAllTags(List<int> tag_id)
        {
            List<int> res = new List<int>();
            res.AddRange(tag_id);

            List<int> child_id = GetChildTag(tag_id);
            if (child_id != null && child_id.Count > 0)
            {
                res.AddRange(GetAllTags(child_id));
            }
            return res;
        }


        //public static List<int> GetAllTags(List<int> tag_id)
        //{
        //    List<int> res = new List<int>();
        //    res.AddRange(tag_id);
        //    tag_id.ForEach(t => res.AddRange(GetAllTags(t)));
        //    return res;
        //}
        static List<int> GetChildTag(int tag_id)
        {
            ZZULIEntities ef = GetEntity();
            List<int> data = ef.Navigations.Where(n => n.Ower_ID == tag_id).Select(n => n.Nav_ID).ToList();
            if (data != null && data.Count() > 0)
                return data.ToList();
            return null;
        }

        static List<int> GetChildTag(List<int> tag_id)
        {
            List<int> res = new List<int>();
            tag_id.ForEach(f => { List<int> data = GetChildTag(f); if (data != null) { res.AddRange(data); } });
            //var data = ef.Navigations.Where(n => tag_id.Contains(n.Ower_ID)).Select(n => n.Nav_ID);
            if (res.Count > 0)
                return res;
            else
                return null;
        }

        public static List<Navigations> GetFrom_Question(int q_id)
        {
            ZZULIEntities ef = GetEntity();
            //获取问题的标签
            List<int> tagsid = ef.ObjectTags.Where(o => o.Obj_ID == q_id && o.Obj_Type == 33).Select(o => o.Obj_TagID).ToList();
            ////获取标签的子标签

            //List<int> all = new List<int>();
            //tagsid.ForEach(a => { List<int> ch = GetAllTags(a); if (ch != null && ch.Count > 0) { all.AddRange(GetAllTags(a)); } });
            return ef.Navigations.Where(n => tagsid.Contains(n.Nav_ID)).ToList();
        }


        public static List<Navigations> GetFrom_Course(int couid)
        {
            ZZULIEntities ef = GetEntity();
            //获取课程的标签
            List<int> tagsid = ef.ObjectTags.Where(o => o.Obj_ID == couid && o.Obj_Type == 32).Select(o => o.Obj_TagID).ToList();
            ////获取标签的子标签

            //List<int> all = new List<int>();
            //tagsid.ForEach(a => { List<int> ch = GetAllTags(a); if (ch != null && ch.Count > 0) { all.AddRange(GetAllTags(a)); } });
            return ef.Navigations.Where(n => tagsid.Contains(n.Nav_ID)).ToList();
        }

        public static List<Navigations> GetFrom_Note(int noteid)
        {
            ZZULIEntities ef = GetEntity();
            //获取笔记的标签
            List<int> tagsid = ef.ObjectTags.Where(o => o.Obj_ID == noteid && o.Obj_Type == 34).Select(o => o.Obj_TagID).ToList();
            ////获取标签的子标签

            //List<int> all = new List<int>();
            //tagsid.ForEach(a => { List<int> ch = GetAllTags(a); if (ch != null && ch.Count > 0) { all.AddRange(GetAllTags(a)); } });
            return ef.Navigations.Where(n => tagsid.Contains(n.Nav_ID)).ToList();
        }
    }
}