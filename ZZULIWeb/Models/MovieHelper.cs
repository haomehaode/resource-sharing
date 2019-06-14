using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Xml;
using System.Reflection;

namespace ZZULIWeb.Models
{
    public class MovieHelper
    {
        //static ZZULIEntities db = new ZZULIEntities();

        //#region 01.获取功能分组 + public static List<Partition> GetPartition()

        ///// <summary>
        ///// 获取功能分组
        ///// </summary>
        ///// <returns>返回值  功能分组对象集合</returns>
        //public static List<Partition> GetPartition()
        //{
        //    //return db.Partition.Where(n => n.Part_ID > 0).ToList();
        //    return db.Partition.Where(n => n.Part_ID > 0).ToList();
        //}

        //#endregion

        //#region 02.获取0级导航和1级导航

        ///// <summary>
        ///// 获取0级导航栏
        ///// </summary>
        ///// <returns>返回值  0级导航节点对象集合</returns>
        //public static List<Navigation> GetNavigation0()
        //{
        //    return db.Navigation.Where(n => n.Nav_ID > 0).ToList();
        //}

        ///// <summary>
        ///// 获取1级导航栏
        ///// </summary>
        ///// <param name="selectLambda">查询条件</param>
        ///// <returns>返回值  1级导航节点对象集合</returns>
        //public static List<Tags> GetNavigation1(Expression<Func<Tags, bool>> selectLambda)
        //{
        //    return db.Tags.Where(selectLambda).ToList();
        //}

        //#endregion

        //#region 03.获取课程列表  根据类型  根据标签

        ///// <summary>
        ///// 根据不同的排序条件，获取课程TopN
        ///// </summary>
        ///// <typeparam name="TKey">返回值类型</typeparam>
        ///// <param name="orderByLambda">排序条件</param>
        ///// <param name="topN">获取的课程数量</param>
        ///// <param name="isDesc">是否倒叙排列，默认值为Ture</param>
        ///// <returns></returns>
        //public static List<Courses> GetCourseTopN<TKey>(Expression<Func<Courses, TKey>> orderByLambda, int topN)
        //{
        //    return db.Courses.OrderByDescending(orderByLambda).Skip(0).Take(topN).ToList();
        //}

        ///// <summary>
        ///// 根据不同课程类型以及不同的排序条件，获取课程TopN
        ///// </summary>
        ///// <typeparam name="TKey">返回值类型</typeparam>
        ///// <typeparam name="navID">课程类型ID，即0级导航ID</typeparam>
        ///// <param name="selectLambda">查询条件</param>
        ///// <param name="orderByLambda">排序条件</param>
        ///// <param name="topN">获取的课程数量</param>
        ///// <param name="isDesc">是否倒叙排列，默认值为Ture</param>
        ///// <returns></returns>
        //public static List<Courses> GetCourseTopNByTags<TKey>(int navID, Expression<Func<Courses, bool>> selectLambda, Func<Courses, TKey> orderByLambda, int topN)
        //{
        //    //1、获取该类型下的所有标签
        //    List<int> numList = db.Tags.Where(t => t.Tag_Type == navID).Select(t => t.Tag_ID).ToList();
        //    //2、从课程表中获取所有贴有这些标签的课程
        //    List<Courses> courseList = db.Courses.Where(selectLambda).Where(c => numList.Contains(c.cou_)).ToList();
        //    //3、排序，获取topN
        //    return courseList.OrderByDescending(orderByLambda).Skip(0).Take(topN).ToList();
        //}

        //#endregion

        //#region 04.获取课程详情

        ///// <summary>
        ///// 根据条件查询课程详细信息
        ///// </summary>
        ///// <param name="selectLambda">查询条件</param>
        ///// <returns>返回一个课程详情对象</returns>
        //public static Course GetCourseDetail(int id)
        //{
        //    var lis = from c in db.Courses join u in db.UserInfo on c.User_ID equals u.User_ID join t in db.Tags on c.Cou_Tags equals t.Tag_ID join n in db.Navigation on t.Tag_Type equals n.Nav_ID where c.Cou_ID == id select new { Cou_ID = c.Cou_ID, Cou_Name = c.Cou_Name, Cou_Type = n.Nav_Content, Cou_Describe = c.Cou_Describe, Cou_Tags = t.Tag_Name, Cou_Hot = c.Cou_Hot, Cou_Catalog = c.Cou_Catalog, Cou_Likes = c.Cou_Likes, Cou_NotLikes = c.Cou_NotLikes, Cou_StudyNum = c.Cou_StudyNum, Cou_Time = c.Cou_Time, User_ID = u.User_ID, User_Name = u.User_NickName };

        //    Course movieDetail = new Course();
        //    //var m = lis.FirstOrDefault();
        //    //movieDetail.Cou_ID = m.Cou_ID;
        //    //movieDetail.Cou_Name = m.Cou_Name;
        //    //movieDetail.Cou_Type = m.Cou_Type;
        //    //movieDetail.Cou_Describe = m.Cou_Describe;
        //    //movieDetail.Cou_Tags = m.Cou_Tags;
        //    //movieDetail.Cou_Hot = m.Cou_Hot;
        //    //movieDetail.Cou_Catalog = m.Cou_Catalog;
        //    //movieDetail.Cou_Likes = m.Cou_Likes;
        //    //movieDetail.Cou_NotLikes = m.Cou_NotLikes;
        //    //movieDetail.Cou_StudyNum = m.Cou_StudyNum;
        //    //movieDetail.Cou_Time = m.Cou_Time;
        //    //movieDetail.User_ID = m.User_ID;
        //    //movieDetail.User_Name = m.User_Name;
        //    return movieDetail;
        //}

        //#endregion

        //#region 05.课程评论

        ///// <summary>
        ///// 获取课程评论
        ///// </summary>
        ///// <param name="id">课程ID</param>
        ///// <param name="topN">topN</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页容量</param>
        ///// <returns></returns>
        //public static List<CourseComment> GetCourseComment(int cou_id, int topN = 5, int pageIndex = 1, int pageSize = 10)
        //{
        //    List<CourseComment> list = new List<CourseComment>();
        //    CourseComment c;
        //    //1、如果是第一页，先获取赞topN
        //    if (pageIndex == 1)
        //    {
        //        List<string> top_id = new List<string>();
        //        var var_top = (from cc in db.CoursesComment join u in db.UserInfo on cc.User_ID equals u.User_ID where cc.Cou_ID == cou_id orderby cc.CCom_Likes descending select new { CCom_ID = cc.CCom_ID, Cou_ID = cc.Cou_ID, User_ID = u.User_ID, CCom_Content = cc.CCom_Content, CCom_Time = cc.CCom_Time, CCom_Likes = cc.CCom_Likes, User_Name = u.User_NickName }).Take(topN);
        //        foreach (var v in var_top)
        //        {
        //            c = new CourseComment() { CCom_ID = v.CCom_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CCom_Content = v.CCom_Content, CCom_Time = v.CCom_Time, CCom_Likes = v.CCom_Likes, User_Name = v.User_Name };
        //            list.Add(c);
        //            top_id.Add(c.CCom_ID);
        //        }
        //        //2、如果是第一页再获取pagesize-topN条,且不包含topN中的数据，并按照时间先后顺序排列
        //        var var_list = (from cc in db.CoursesComment join u in db.UserInfo on cc.User_ID equals u.User_ID where cc.Cou_ID == cou_id && !top_id.Contains(cc.CCom_ID) orderby cc.CCom_Time descending select new { CCom_ID = cc.CCom_ID, Cou_ID = cc.Cou_ID, User_ID = u.User_ID, CCom_Content = cc.CCom_Content, CCom_Time = cc.CCom_Time, CCom_Likes = cc.CCom_Likes, User_Name = u.User_NickName }).Take(pageSize - topN);
        //        foreach (var v in var_list)
        //        {
        //            c = new CourseComment() { CCom_ID = v.CCom_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CCom_Content = v.CCom_Content, CCom_Time = v.CCom_Time, CCom_Likes = v.CCom_Likes, User_Name = v.User_Name };
        //            list.Add(c);
        //        }
        //    }
        //    else//否则获取pagesize条数据，并按照时间先后顺序排列
        //    {
        //        var var_list = (from cc in db.CoursesComment join u in db.UserInfo on cc.User_ID equals u.User_ID where cc.Cou_ID == cou_id orderby cc.CCom_Time descending select new { CCom_ID = cc.CCom_ID, Cou_ID = cc.Cou_ID, User_ID = u.User_ID, CCom_Content = cc.CCom_Content, CCom_Time = cc.CCom_Time, CCom_Likes = cc.CCom_Likes, User_Name = u.User_NickName }).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        foreach (var v in var_list)
        //        {
        //            c = new CourseComment() { CCom_ID = v.CCom_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CCom_Content = v.CCom_Content, CCom_Time = v.CCom_Time, CCom_Likes = v.CCom_Likes, User_Name = v.User_Name };
        //            list.Add(c);
        //        }
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 获取课程评论的总页数
        ///// </summary>
        ///// <param name="cou_id">课程ID</param>
        ///// <param name="pagesize">页容量</param>
        ///// <returns></returns>
        //static int GetCourseCommentPageNum(int cou_id, int pagesize = 10)
        //{
        //    var var_top = from cc in db.CoursesComment join u in db.UserInfo on cc.User_ID equals u.User_ID where cc.Cou_ID == cou_id select cc;
        //    return Convert.ToInt32(Math.Ceiling(var_top.Count() / Convert.ToDouble(pagesize)));
        //}

        //#endregion

        //#region 06.课程问题

        ///// <summary>
        ///// 获取课程问题集合
        ///// </summary>
        ///// <param name="cou_id">课程ID</param>
        ///// <param name="topN">topN</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页容量</param>
        ///// <returns></returns>
        //public static List<CourseQ> GetCourseQList(int cou_id, int topN = 5, int pageIndex = 1, int pageSize = 10)
        //{
        //    List<CourseQ> list = new List<CourseQ>();
        //    CourseQ c;
        //    if (pageIndex == 1)
        //    {
        //        List<int> top_id = new List<int>();
        //        //1、获取topN
        //        var top_list = (from cq in db.CoursesQuestions join u in db.UserInfo on cq.User_ID equals u.User_ID where cq.Cou_ID == cou_id orderby cq.CQ_Hot descending select new { CQ_ID = cq.CQ_ID, Cou_ID = cq.Cou_ID, User_ID = cq.User_ID, CQ_Title = cq.CQ_Title, CQ_Content = cq.CQ_Content, CQ_Time = cq.CQ_Time, CQ_Collect = cq.CQ_Collect, CQ_Hot = cq.CQ_Hot, CQ_PageView = cq.CQ_PageView, User_Name = u.User_NickName }).Take(topN);
        //        foreach (var v in top_list)
        //        {
        //            c = new CourseQ() { CQ_ID = v.CQ_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CQ_Title = v.CQ_Title, CQ_Content = v.CQ_Content, CQ_Time = v.CQ_Time, CQ_Collect = v.CQ_Collect, CQ_Hot = v.CQ_Hot, CQ_PageView = v.CQ_PageView };
        //            list.Add(c);
        //            top_id.Add(c.CQ_ID);
        //        }
        //        //2、获取pageSize-topN个数据，不包含topn中的数据，并按时间排序
        //        var var_list = (from cq in db.CoursesQuestions join u in db.UserInfo on cq.User_ID equals u.User_ID where cq.Cou_ID == cou_id && !top_id.Contains(cq.CQ_ID) orderby cq.CQ_Time descending select new { CQ_ID = cq.CQ_ID, Cou_ID = cq.Cou_ID, User_ID = cq.User_ID, CQ_Title = cq.CQ_Title, CQ_Content = cq.CQ_Content, CQ_Time = cq.CQ_Time, CQ_Collect = cq.CQ_Collect, CQ_Hot = cq.CQ_Hot, CQ_PageView = cq.CQ_PageView, User_Name = u.User_NickName }).Take(pageSize - topN);
        //        foreach (var v in var_list)
        //        {
        //            c = new CourseQ() { CQ_ID = v.CQ_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CQ_Title = v.CQ_Title, CQ_Content = v.CQ_Content, CQ_Time = v.CQ_Time, CQ_Collect = v.CQ_Collect, CQ_Hot = v.CQ_Hot, CQ_PageView = v.CQ_PageView };
        //            list.Add(c);
        //        }
        //    }
        //    else
        //    {
        //        var var_list = (from cq in db.CoursesQuestions join u in db.UserInfo on cq.User_ID equals u.User_ID where cq.Cou_ID == cou_id orderby cq.CQ_Time descending select new { CQ_ID = cq.CQ_ID, Cou_ID = cq.Cou_ID, User_ID = cq.User_ID, CQ_Title = cq.CQ_Title, CQ_Content = cq.CQ_Content, CQ_Time = cq.CQ_Time, CQ_Collect = cq.CQ_Collect, CQ_Hot = cq.CQ_Hot, CQ_PageView = cq.CQ_PageView, User_Name = u.User_NickName }).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        foreach (var v in var_list)
        //        {
        //            c = new CourseQ() { CQ_ID = v.CQ_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CQ_Title = v.CQ_Title, CQ_Content = v.CQ_Content, CQ_Time = v.CQ_Time, CQ_Collect = v.CQ_Collect, CQ_Hot = v.CQ_Hot, CQ_PageView = v.CQ_PageView };
        //            list.Add(c);
        //        }
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 获取课程问题的总页数
        ///// </summary>
        ///// <param name="cou_id"></param>
        ///// <param name="pageSize"></param>
        ///// <returns></returns>
        //public static int GetCourseQPageNum(int cou_id, int pageSize = 10)
        //{
        //    return Convert.ToInt32(Math.Ceiling(db.CoursesQuestions.Where(cq => cq.CQ_ID == cou_id).ToList().Count / Convert.ToDouble(pageSize)));
        //}

        //#endregion

        //#region 07.课程问题回答

        ///// <summary>
        ///// 获取课程问题回复
        ///// </summary>
        ///// <param name="cq_id">课程问题ID</param>
        ///// <param name="pageSize">显示数量</param>
        ///// <param name="isAll">是否全部显示</param>
        ///// <returns></returns>
        ////public static List<CourseA> GetCourseA(int cq_id, int pageSize = 10, bool isAll = false)
        ////{
        ////    var var_list = from ca in db.CoursesAnswers join u in db.UserInfo on ca.User_ID equals u.User_ID where ca.CQ_ID == cq_id select new { CA_ID = ca.CA_ID, User_ID = ca.User_ID, CA_Target = ca.CA_Target, CA_Content = ca.CA_Content, CA_Time = ca.CA_Time, CA_Likes = ca.CA_Likes, User_Name = u.User_NickName };
        ////    List<CourseA> list = new List<CourseA>();
        ////    CourseA c;
        ////    foreach (var v in var_list)
        ////    {
        ////        c = new CourseA() { CA_ID = v.CA_ID, User_ID = v.User_ID, CA_Target = v.CA_Target, CA_Content = v.CA_Content, CA_Time = v.CA_Time, CA_Likes = v.CA_Likes, User_Name = v.User_Name };
        ////        list.Add(c);
        ////    }
        ////    return list;
        ////}

        ///// <summary>
        ///// 获取课程问题的回复量
        ///// </summary>
        ///// <param name="cq_id"></param>
        ///// <returns></returns>
        //public static int GetCourseANum(int cq_id)
        //{
        //    return db.CoursesAnswers.Where(c => c.CQ_ID == cq_id).ToList().Count;
        //}

        ///// <summary>
        ///// 获取课程问题回复的总页数
        ///// </summary>
        ///// <param name="cq_id">课程问题ID</param>
        ///// <param name="pageSize">页容量</param>
        ///// <returns></returns>
        //public static int GetCourseAPageNum(int cq_id, int pageSize = 10)
        //{
        //    return Convert.ToInt32(Math.Ceiling(db.CoursesAnswers.Where(ca => ca.CQ_ID == cq_id).ToList().Count / Convert.ToDouble(pageSize)));
        //}

        //#endregion

        //#region 08.课程笔记

        ///// <summary>
        ///// 获取课程笔记列表
        ///// </summary>
        ///// <param name="cou_id">课程ID</param>
        ///// <param name="topN">topN</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页容量</param>
        ///// <returns></returns>
        ////public static List<CourseNote> GetCourseNote(int cou_id, int topN = 5, int pageIndex = 1, int pageSize = 10)
        ////{
        ////    List<CourseNote> list = new List<CourseNote>();
        ////    CourseNote c;
        ////    if (pageIndex == 1)
        ////    {
        ////        List<string> top_id = new List<string>();
        ////        var top_list = (from cn in db.CoursesNotes join u in db.UserInfo on cn.User_ID equals u.User_ID where cn.Cou_ID == cou_id orderby cn.CN_Hot descending select new { CN_ID = cn.CN_ID, Cou_ID = cn.Cou_ID, CN_Content = cn.CN_Content, User_ID = cn.User_ID, CN_Time = cn.CN_Time, CN_Collect = cn.CN_Collect, CN_Likes = cn.CN_Likes, CN_NotLikes = cn.CN_NotLikes, CN_Hot = cn.CN_Hot, User_Name = u.User_NickName }).Take(topN);
        ////        foreach (var v in top_list)
        ////        {
        ////            c = new CourseNote() { CN_ID = v.CN_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CN_Content = v.CN_Content, CN_Time = v.CN_Time, CN_Collect = v.CN_Collect, CN_Likes = v.CN_Likes, CN_NotLikes = v.CN_NotLikes, CN_Hot = v.CN_Hot, User_Name = v.User_Name };
        ////            list.Add(c);
        ////            top_id.Add(c.CN_ID);
        ////        }

        ////        var var_list = (from cn in db.CoursesNotes join u in db.UserInfo on cn.User_ID equals u.User_ID where cn.Cou_ID == cou_id && !top_id.Contains(cn.CN_ID) orderby cn.CN_Time descending select new { CN_ID = cn.CN_ID, Cou_ID = cn.Cou_ID, CN_Content = cn.CN_Content, User_ID = cn.User_ID, CN_Time = cn.CN_Time, CN_Collect = cn.CN_Collect, CN_Likes = cn.CN_Likes, CN_NotLikes = cn.CN_NotLikes, CN_Hot = cn.CN_Hot, User_Name = u.User_NickName }).Take(pageSize - topN);
        ////        foreach (var v in var_list)
        ////        {
        ////            c = new CourseNote() { CN_ID = v.CN_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CN_Content = v.CN_Content, CN_Time = v.CN_Time, CN_Collect = v.CN_Collect, CN_Likes = v.CN_Likes, CN_NotLikes = v.CN_NotLikes, CN_Hot = v.CN_Hot, User_Name = v.User_Name };
        ////            list.Add(c);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        var var_list = (from cn in db.CoursesNotes join u in db.UserInfo on cn.User_ID equals u.User_ID where cn.Cou_ID == cou_id orderby cn.CN_Time descending select new { CN_ID = cn.CN_ID, Cou_ID = cn.Cou_ID, CN_Content = cn.CN_Content, User_ID = cn.User_ID, CN_Time = cn.CN_Time, CN_Collect = cn.CN_Collect, CN_Likes = cn.CN_Likes, CN_NotLikes = cn.CN_NotLikes, CN_Hot = cn.CN_Hot, User_Name = u.User_NickName }).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        ////        foreach (var v in var_list)
        ////        {
        ////            c = new CourseNote() { CN_ID = v.CN_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CN_Content = v.CN_Content, CN_Time = v.CN_Time, CN_Collect = v.CN_Collect, CN_Likes = v.CN_Likes, CN_NotLikes = v.CN_NotLikes, CN_Hot = v.CN_Hot, User_Name = v.User_Name };
        ////            list.Add(c);
        ////        }
        ////    }
        ////    return list;
        ////}

        ///// <summary>
        ///// 获取课程笔记的总页数
        ///// </summary>
        ///// <param name="cou_id">课程ID</param>
        ///// <param name="pageSize">页容量</param>
        ///// <returns></returns>
        //public static int GetCourseNotePageNum(int cou_id, int pageSize = 10)
        //{
        //    return Convert.ToInt32(Math.Ceiling(db.CoursesNotes.Where(cn => cn.Cou_ID == cou_id).ToList().Count / Convert.ToDouble(pageSize)));
        //}

        //#endregion

        //#region 09.获取推荐课程

        ///// <summary>
        ///// 根据课程ID获取推荐课程
        ///// </summary>
        ///// <param name="cou_id">课程ID</param>
        ///// <param name="topN">topN</param>
        ///// <returns></returns>
        //public static List<RecommendCourse> GetRecommendCourse(int cou_id, int topN = 5)
        //{
        //    //1、根据课程ID获取对应的标签ID
        //    var v_id = from c in db.Courses where c.Cou_ID == cou_id select c;
        //    int tag_id = Convert.ToInt32(v_id.FirstOrDefault().Cou_Tags);
        //    //2、根据标签ID获取topN
        //    var var_list = (from c in db.Courses where c.Cou_Tags == tag_id && c.Cou_ID != cou_id orderby c.Cou_Hot descending select new { Cou_ID = c.Cou_ID, Cou_Name = c.Cou_Name }).Take(topN);
        //    List<RecommendCourse> list = new List<RecommendCourse>();
        //    RecommendCourse rc = new RecommendCourse();
        //    foreach (var v in var_list)
        //    {
        //        rc = new RecommendCourse() { Cou_ID = v.Cou_ID, Cou_Name = v.Cou_Name };
        //        list.Add(rc);
        //    }
        //    return list;
        //}

        //#endregion

        //static List<object> ModelToList(IQueryable varD, string className)
        //{
        //    switch (className)
        //    {
        //        case "CourseComment":
        //            List<CourseComment> list = new List<CourseComment>;
        //            CourseComment c;

        //    foreach (IQueryable v in varD)
        //    {
        //        c = new CourseComment() { CCom_ID = v.CCom_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CCom_Content = v.CCom_Content, CCom_Time = v.CCom_Time, CCom_Likes = v.CCom_Likes, User_Name = v.User_Name };
        //        list.Add(c);
        //    }
        //            break;
        //        case "":
        //            break;
        //        default:
        //            break;
        //    }
        //    return null;
        //}
    }

}