using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;
using System.Xml;

namespace ZZULIWeb.Controllers
{
    public class CourseController : Controller
    {
        Course course = new Course();
        Courses courses = new Courses();
        Navigation nav = new Navigation();
        ObjectTag ot = new ObjectTag();


        //点击课程，显示所有课程
        // GET: /Course/
        public ActionResult List(int nav0 = 0, int nav1 = 0, int param = 1)
        {
            //导航栏
            List<Navigations> navList0 = nav.GetNavigation0();
            ViewBag.NavList0 = navList0;

            List<Navigations> navList1 = nav.GetNavigation1(nav0);
            ViewBag.NavList1 = navList1;

            ViewBag.TiTle = "课程";
            ViewBag.Nav0 = nav0;
            ViewBag.Nav1 = nav1;


            int currentPage = 1;
            int pageNum = 0;


            if (nav0 == 0 && nav1 == 0)//未选择
            {
                pageNum = Convert.ToInt32(Math.Ceiling(course.GetList_Where(c => c.Cou_ID > 0).Count / 20.0));
                if (pageNum < 1)
                {

                    ViewBag.CourseList = null;
                }
                else
                {
                    if (param > pageNum)
                    {
                        currentPage = pageNum;
                    }
                    else
                    {
                        currentPage = param;
                    }
                    ViewBag.CourseList = course.GetList_Page(c => c.Cou_ID > 0, c => c.Cou_Hot, currentPage, 20);
                }
            }
            else if (nav0 != 0 && nav1 == 0)//0级导航选中
            {
                //pageNum = Convert.ToInt32(Math.Ceiling(course.GetList_Where(c => c.Navigations.Ower_ID == nav0).Count / 20.0));
                List<Courses> cl = course.GetCourseByTags(nav0);
                if (cl != null)
                {
                    pageNum = Convert.ToInt32(Math.Ceiling(cl.Count / 20.0));
                    currentPage = param < 1 ? 1 : (param > pageNum ? pageNum : param);
                    //ViewBag.CourseList = course.GetList_Page(c => c.Navigations.Ower_ID == nav0, c => c.Cou_Hot, currentPage, 20);
                    ViewBag.CourseList = course.GetCourseByTags_Page(nav0, c => c.Cou_Hot, currentPage, 20);
                }
            }
            else//1级导航都选中
            {
                //pageNum = Convert.ToInt32(Math.Ceiling(course.GetList_Where(c => c.Cou_Tags == nav1).Count / 20.0));
                List<Courses> cl = course.GetCourseByTags(nav1);
                if (cl != null)
                {
                    pageNum = Convert.ToInt32(Math.Ceiling(cl.Count / 20.0));
                    currentPage = param < 1 ? 1 : (param > pageNum ? pageNum : param);
                    ViewBag.CourseList = course.GetCourseByTags_Page(nav1, c => c.Cou_Hot, currentPage, 20);
                }
            }

            //if (pageIndex > pageNum)
            //{
            //    currentPage = pageNum;
            //}
            //else if (pageIndex < 1)
            //{
            //    currentPage = 1;
            //}
            //else
            //{
            //    currentPage = pageIndex;
            //}


            ViewBag.pageNum = pageNum;
            ViewBag.CurrentPageIndex = param;
            ////1、功能分区
            //List<Partition> partList = MovieHelper.GetPartition();
            //ViewBag.PartList = partList;

            //2、标签
            //3、加载课程
            return View();
        }

        //public ActionResult Learn(int id)
        //{
        //    //6、根据课程标签获取推荐课程
        //    List<Courses> rc_list = course.GetRecommendCourse(id);
        //    ViewBag.RCList = rc_list;
        //    //2、根据课程ID获取课程信息
        //    courses = course.GetCourseDetail(id);
        //    return View(courses);
        //}

        //显示课程目录
        //

        public ActionResult Section(int id)
        {
            //资源下载
            ViewBag.ResourceList = new Resource().GetList_TopN(r => r.R_ID > 0, r => r.R_Hot, 5);
            if (Common.UserInfo != null)
                ViewBag.LearnedMoveList = new User().GetLearnMoveIDByCouID(Common.UserInfo.User_ID, id);
          


            //if (Common.UserInfo == null)
            //    return RedirectToAction("Index", "Login");
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"C:\Users\MC_S\Desktop\前台文件\test.xml");
            //course.Modify(new Courses() { Cou_Catalog = doc.OuterXml }, c => c.Cou_ID == 1005, "Cou_Catalog");
            //记录用户的浏览行为
            //Common.PageView(id, 1, Common.UserInfo == null ? 0 : Common.UserInfo.User_ID);

            //6、根据课程标签获取推荐课程
            List<Courses> rc_list = course.GetRecommendCourseByCID(id);
            ViewBag.RCList = rc_list;

            //2、根据课程ID获取课程信息
            courses = course.GetCourseDetail(id);


            return View(courses);
        }

        public ActionResult Comment(int id, int pageIndex = 1)
        {
            //资源下载
            ViewBag.ResourceList = new Resource().GetList_TopN(r => r.R_ID > 0, r => r.R_Hot, 5);
            //6、根据课程标签获取推荐课程
            List<Courses> rc_list = course.GetRecommendCourseByCID(id);
            ViewBag.RCList = rc_list;

            //2、根据课程ID获取课程信息
            courses = course.GetCourseDetail(id);

            int pageNum = Convert.ToInt32(Math.Ceiling(course.GetCourseComment(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CCList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesComment> cc_list = course.GetCourseComment_Page(id, 1, 10, 5);
                ViewBag.CCList = cc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesComment> cc_list = course.GetCourseComment_Page(id, pageNum, 10, 5);
                ViewBag.CCList = cc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesComment> cc_list = course.GetCourseComment_Page(id, pageIndex, 10, 5);
                ViewBag.CCList = cc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return View(courses);
        }

        public ActionResult Question(int id, int pageIndex = 1)
        {
            //资源下载
            ViewBag.ResourceList = new Resource().GetList_TopN(r => r.R_ID > 0, r => r.R_Hot, 5);
            //6、根据课程标签获取推荐课程
            List<Courses> rc_list = course.GetRecommendCourseByCID(id);
            ViewBag.RCList = rc_list;
            //4、根据课程ID获取课程问答列表信息
            //Dictionary<int, CoursesAnswers> qaList = course.GetCourseQuestionAndAnswer(id);
            //List<CoursesQuestions> cq_list = course.GetCourseQuestion_Page(id, pageIndex, 10, 5);
            //List<CoursesAnswers> ca_list = course.GetCourseAnswer(cq_list);
            //ViewBag.CQList = cq_list;
            //ViewBag.CAList = ca_list;


            //2、根据课程ID获取课程信息
            courses = course.GetCourseDetail(id);


            int pageNum = Convert.ToInt32(Math.Ceiling(course.GetCourseQuestion(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CQList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesQuestions> cc_list = course.GetCourseQuestion_Page(id, 1, 10, 5);
                ViewBag.CQList = cc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesQuestions> cc_list = course.GetCourseQuestion_Page(id, pageNum, 10, 5);
                ViewBag.CQList = cc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesQuestions> cc_list = course.GetCourseQuestion_Page(id, pageIndex, 10, 5);
                ViewBag.CQList = cc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return View(courses);
        }

        public ActionResult Note(int id, int pageIndex = 1)
        {
            //资源下载
            ViewBag.ResourceList = new Resource().GetList_TopN(r => r.R_ID > 0, r => r.R_Hot, 5);
            //6、根据课程标签获取推荐课程
            List<Courses> rc_list = course.GetRecommendCourseByCID(id, 10);
            ViewBag.RCList = rc_list;

            //5、根据课程ID获取课程笔记列表信息
            //List<CoursesNotes> note_list = course.GetCourseNote_Page(id, pageIndex, 10, 5);
            //ViewBag.CNList = note_list;

            //2、根据课程ID获取课程信息
            courses = course.GetCourseDetail(id);


            int pageNum = Convert.ToInt32(Math.Ceiling(course.GetCourseNote(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CNList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesNotes> cc_list = course.GetCourseNote_Page(id, 1, 10, 5);
                ViewBag.CNList = cc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesNotes> cc_list = course.GetCourseNote_Page(id, pageNum, 10, 5);
                ViewBag.CNList = cc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesNotes> cc_list = course.GetCourseNote_Page(id, pageIndex, 10, 5);
                ViewBag.CNList = cc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return View(courses);
        }

        public ActionResult quesdetail(int cq_id, int pageIndex = 1)
        {
            return View();
        }


    }
}