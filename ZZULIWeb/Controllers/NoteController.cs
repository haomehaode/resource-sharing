using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class NoteController : Controller
    {
        Note note = new Note();
        //
        // GET: /Note/
        public ActionResult List(int pageIndex = 1)
        {
            //获取笔记列表
            //List<Notes> note_list = note.GetList_Page(n => n.N_ID > 0, n => n.N_Time, pageIndex, 10);
            //ViewBag.NoteList = note_list;


            int pageNum = Convert.ToInt32(Math.Ceiling(note.GetList_Where(n => n.N_ID > 0).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<Notes> note_list = note.GetList_Page(n => n.N_ID > 0, n => n.N_Time, pageNum, 10);
                ViewBag.NoteList = note_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<Notes> note_list = note.GetList_Page(n => n.N_ID > 0, n => n.N_Time, 1, 10);
                ViewBag.NoteList = note_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<Notes> note_list = note.GetList_Page(n => n.N_ID > 0, n => n.N_Time, pageIndex, 10);
                ViewBag.NoteList = note_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }

            return View();
        }

        public ActionResult n(int id, int pageIndex = 1)
        {
            if (Common.UserInfo == null)
                return RedirectToAction("Index", "Login");
            //记录用户行为信息
            Common.PageView(id, 3, Common.UserInfo.User_ID);
            Common.ModifyPageView(id, 3);

            int pageNum = Convert.ToInt32(Math.Ceiling(note.GetNoteCommentNum(id) / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.CommentList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<NotesComments> nc_list = note.GetNoteComment_Page(id, 1, 10, 5);
                ViewBag.CommentList = nc_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<NotesComments> nc_list = note.GetNoteComment_Page(id, pageNum, 10, 5);
                ViewBag.CommentList = nc_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<NotesComments> nc_list = note.GetNoteComment_Page(id, pageIndex, 10, 5);
                ViewBag.CommentList = nc_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }

            //ViewBag.CommentList = note.GetNoteComment(id);
            ViewBag.TagList = note.GetNoteTags(id);
            return View(note.GetList_Where(n => n.N_ID == id).FirstOrDefault());
        }

        public ActionResult Index()
        {
            if (Common.UserInfo == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.TagsList = new Navigation().GetAllTags();
            return View();
        }
    }
}