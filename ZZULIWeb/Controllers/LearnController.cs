using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class LearnController : Controller
    {
        Course course = new Course();
        Models.Move move = new Models.Move();
        //
        // GET: /Learn/
        public ActionResult v(int id, int page = 1)
        {
            string url = Request.RawUrl;
            if (Common.UserInfo == null)
                return RedirectToAction("Index", "Login");

            //记录用户行为信息
            //判断用户学习表中是否已经存在该用户的记录，如果存在，更改学习进度
            ViewBag.Page = page;
            //ViewBag.VideoName = videoName;
            //Courses course = new Course().GetList_Where(c => c.Cou_ID == id).FirstOrDefault();
            course.UserStartLearn(Common.UserInfo.User_ID, id);
            return View(id);
        }

        public ActionResult comment(int id, int pageIndex = 1)
        {
            #region MyRegion
            ZZULIWeb.Move m = move.GetList_Where(mo => mo.Mov_ID == id).FirstOrDefault();

            //Courses cou = course.GetList_Where(c => c.Cou_ID == id).FirstOrDefault();
            int pageNum = Convert.ToInt32(Math.Ceiling(course.GetMoveCommment(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CCList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesComment> cc_list = course.GetMoveCommment_Page(id, 1, 10, 5);
                ViewBag.CCList = cc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesComment> cc_list = course.GetMoveCommment_Page(id, pageNum, 10, 5);
                ViewBag.CCList = cc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesComment> cc_list = course.GetMoveCommment_Page(id, pageIndex, 10, 5);
                ViewBag.CCList = cc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            #endregion
            return PartialView(m);
        }
        public ActionResult question(int id, int pageIndex = 1)
        {
            ZZULIWeb.Move m = move.GetList_Where(mo => mo.Mov_ID == id).FirstOrDefault();

            //Courses cou = course.GetList_Where(c => c.Cou_ID == id).FirstOrDefault();
            int pageNum = Convert.ToInt32(Math.Ceiling(course.GetMoveQuestion(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CQList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesQuestions> cq_list = course.GetMoveQuestion_Page(id, 1, 10, 5);
                ViewBag.CQList = cq_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesQuestions> cq_list = course.GetMoveQuestion_Page(id, pageNum, 10, 5);
                ViewBag.CQList = cq_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesQuestions> cq_list = course.GetMoveQuestion_Page(id, pageIndex, 10, 5);
                ViewBag.CQList = cq_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(m);
        }
        public ActionResult note(int id, int pageIndex = 1)
        {
            ZZULIWeb.Move m = move.GetList_Where(mo => mo.Mov_ID == id).FirstOrDefault();

            //Courses cou = course.GetList_Where(c => c.Cou_ID == id).FirstOrDefault();
            int pageNum = Convert.ToInt32(Math.Ceiling(course.GetMoveNote(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CNList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesNotes> cn_list = course.GetMoveNote_Page(id, 1, 10, 5);
                ViewBag.CNList = cn_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesNotes> cn_list = course.GetMoveNote_Page(id, pageNum, 10, 5);
                ViewBag.CNList = cn_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<CoursesNotes> cn_list = course.GetMoveNote_Page(id, pageIndex, 10, 5);
                ViewBag.CNList = cn_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(m);
        }
    }
}