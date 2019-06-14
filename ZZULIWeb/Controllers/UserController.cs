using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class UserController : Controller
    {
        User user = new User();
        //
        // GET: /User/
        public ActionResult Index(int id)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            //获取个人学习课程记录
            ViewBag.LearnedCourseList = user.GetLearnedCourse_TopN(id, 5);

            return View(info);
        }

        public ActionResult Question(int id, int page = 1)
        {
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ViewBag.Page = page;
            return View(info);
        }

        //[HttpPost]
        //public ActionResult UserQuestion(int id, int page)
        //{
        //    UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
        //    ViewBag.Page = page;
        //    return View(info);
        //}

        public ActionResult q(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            Question ques = new Question();
            int pageNum = Convert.ToInt32(Math.Ceiling(ques.GetList_Where(q => q.User_ID == id).Count / 10.0));
            ViewBag.pageNum = pageNum;

            if (pageNum <= 0)
            {
                ViewBag.QuestionList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Questions> cc_list = ques.GetList_Page(q => q.User_ID == id, q => q.Q_Time, pageNum, 10);
                ViewBag.QuestionList = cc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Questions> cc_list = ques.GetList_Page(q => q.User_ID == id, q => q.Q_Time, 1, 10);
                ViewBag.QuestionList = cc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Questions> cc_list = ques.GetList_Page(q => q.User_ID == id, q => q.Q_Time, pageIndex, 10);
                ViewBag.QuestionList = cc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return View(info);
        }

        public ActionResult an(int id, int pageIndex = 1)
        {
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetAnswerNumByUID(id) / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.AnswerList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Answers> an_list = user.GetAnswerByUID(id, pageNum, 10);
                ViewBag.AnswerList = an_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Answers> an_list = user.GetAnswerByUID(id, 1, 10);
                ViewBag.AnswerList = an_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Answers> an_list = user.GetAnswerByUID(id, pageIndex, 10);
                ViewBag.AnswerList = an_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        public ActionResult qc(int id, int pageIndex = 1)
        {
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();

            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetQuestionCollectByUID(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.QuestionList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Questions> q_list = user.GetQuestionCollectByUID_Page(id, pageNum, 10);
                ViewBag.QuestionList = q_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Questions> q_list = user.GetQuestionCollectByUID_Page(id, 1, 10);
                ViewBag.QuestionList = q_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Questions> q_list = user.GetQuestionCollectByUID_Page(id, pageIndex, 10);
                ViewBag.QuestionList = q_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        public ActionResult PersonInfo(int id)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();

            return View(info);
        }

        #region 课程

        public ActionResult Course(int id, int page = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ViewBag.Page = page;
            return View(info);
        }

        public ActionResult LearnedCourse(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ////获取个人学习课程记录
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);

            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetLearnedCourse(id).Count() / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.LearnedCourseList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<UserLearnCoursesRecord> cc_list = user.GetLearnedCourse_Page(id, pageNum, 10);
                ViewBag.LearnedCourseList = cc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<UserLearnCoursesRecord> cc_list = user.GetLearnedCourse_Page(id, 1, 10);
                ViewBag.LearnedCourseList = cc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<UserLearnCoursesRecord> cc_list = user.GetLearnedCourse_Page(id, pageIndex, 10);
                ViewBag.LearnedCourseList = cc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return View(info);
        }

        //用户学习课程时所提问的问题
        /// <summary>
        /// 用户学习课程时所提问的问题
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult cq(int id, int pageIndex = 1)
        {

            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();

            ////获取课程问题
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetCourseQuestion(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CourseQuestionList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<CoursesQuestions> cq_list = user.GetCourseQuestion_Page(id, pageNum, 10);
                ViewBag.CourseQuestionList = cq_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<CoursesQuestions> cq_list = user.GetCourseQuestion_Page(id, 1, 10);
                ViewBag.CourseQuestionList = cq_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<CoursesQuestions> cq_list = user.GetCourseQuestion_Page(id, pageIndex, 10);
                ViewBag.CourseQuestionList = cq_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        public ActionResult cn(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();

            ////获取课程问题
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetCourseNote(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CourseNoteList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<CoursesNotes> cn_list = user.GetCourseNote_Page(id, pageNum, 10);
                ViewBag.CourseNoteList = cn_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<CoursesNotes> cn_list = user.GetCourseNote_Page(id, 1, 10);
                ViewBag.CourseNoteList = cn_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<CoursesNotes> cn_list = user.GetCourseNote_Page(id, pageIndex, 10);
                ViewBag.CourseNoteList = cn_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        public ActionResult collect(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();

            ////获取课程问题
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetCourseCollectionByUID(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CollectList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Courses> c_list = user.GetCourseCollection_Page(id, pageNum, 10);
                ViewBag.CollectList = c_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Courses> c_list = user.GetCourseCollection_Page(id, 1, 10);
                ViewBag.CollectList = c_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Courses> c_list = user.GetCourseCollection_Page(id, pageIndex, 10);
                ViewBag.CollectList = c_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        #endregion

        #region 笔记

        public ActionResult Note(int id, int page = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ViewBag.Page = page;
            return View(info);
        }

        public ActionResult n(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ////获取个人学习课程记录
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);
            Note note = new Note();
            int pageNum = Convert.ToInt32(Math.Ceiling(note.GetList_Where(n => n.User_ID == id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.NoteList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Notes> cc_list = note.GetList_Page(n => n.User_ID == id, n => n.N_Time, pageNum, 10);
                ViewBag.NoteList = cc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Notes> cc_list = note.GetList_Page(n => n.User_ID == id, n => n.N_Time, 1, 10);
                ViewBag.NoteList = cc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Notes> cc_list = note.GetList_Page(n => n.User_ID == id, n => n.N_Time, pageIndex, 10);
                ViewBag.NoteList = cc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        public ActionResult nc(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ////获取个人学习课程记录
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetNoteCommentsByUID(id).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.NoteCommentList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<NotesComments> nc_list = user.GetNoteCommentsByUID_Page(id, pageNum, 10);
                ViewBag.NoteCommentList = nc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<NotesComments> nc_list = user.GetNoteCommentsByUID_Page(id, 1, 10);
                ViewBag.NoteCommentList = nc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<NotesComments> nc_list = user.GetNoteCommentsByUID_Page(id, pageIndex, 10);
                ViewBag.NoteCommentList = nc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        public ActionResult nr(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ////获取个人学习课程记录
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetNoteLikeNumByUID(id) / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.NoteList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Notes> n_list = user.GetNoteLike_Page(id, pageNum, 10);
                ViewBag.NoteList = n_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Notes> n_list = user.GetNoteLike_Page(id, 1, 10);
                ViewBag.NoteList = n_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Notes> n_list = user.GetNoteLike_Page(id, pageIndex, 10);
                ViewBag.NoteList = n_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        public ActionResult notecollect(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            ////获取个人学习课程记录
            //ViewBag.LearnedCourseList = user.GetLearnedCourse_Page(id, pageIndex);
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetNoteCollectionNumByUID(id) / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.NoteList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Notes> nc_list = user.GetNoteCollect_Page(id, pageNum, 10);
                ViewBag.NoteList = nc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Notes> nc_list = user.GetNoteCollect_Page(id, 1, 10);
                ViewBag.NoteList = nc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Notes> nc_list = user.GetNoteCollect_Page(id, pageIndex, 10);
                ViewBag.NoteList = nc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return PartialView(info);
        }

        #endregion

        public ActionResult Follow(int id, int pageIndex = 1)
        {
            //获取个人信息
            UserInfo info = user.GetList_Where(u => u.User_ID == id).FirstOrDefault();
            int pageNum = Convert.ToInt32(Math.Ceiling(user.GetMyFollowByUID(id).Count / 10.0));
            ViewBag.pageNum = pageNum;

            if (pageNum <= 0)
            {
                ViewBag.FollowList = null;
            }
            else if (pageIndex > pageNum)
            {
                List<Follow> follow_list = user.GetMyFollowByUID_Page(id, pageNum, 10);
                ViewBag.FollowList = follow_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                List<Follow> follow_list = user.GetMyFollowByUID_Page(id, 1, 10);
                ViewBag.FollowList = follow_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                List<Follow> follow_list = user.GetMyFollowByUID_Page(id, pageIndex, 10);
                ViewBag.FollowList = follow_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }
            return View(info);
        }
    }
}