using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class SearchController : Controller
    {
        Search search = new Search();
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult s()
        {
            string str_search = Request.QueryString["StrSearch"];
            //热门搜索
            List<string> list = search.GetHostestSearch();
            //string str_search = null;

            if (str_search != null)
            {
                str_search = HttpUtility.UrlDecode(str_search.ToString());
                if (str_search.ToString().Trim() == "")
                {
                    ViewBag.CurrentSearch = "";
                    ViewBag.ResultNum = 0;
                }
                else
                {
                    List<Courses> list_course = search.GetSearchResult_Course(str_search, 0, 5);
                    List<Questions> list_question = search.GetSearchResult_Question(str_search, 0);
                    List<Notes> list_note = search.GetSearchResult_Note(str_search, 0);

                    ViewBag.CourseList = list_course;
                    ViewBag.QuestionList = list_question;
                    ViewBag.NoteList = list_note;

                    ViewBag.HostSearch = list;
                    ViewBag.CurrentSearch = str_search;
                    ViewBag.ResultNum = list_course.Count + list_question.Count + list_note.Count;
                }
            }
            else
            {
                ViewBag.CurrentSearch = "";
                ViewBag.ResultNum = 0;
            }

            ViewBag.HostSearch = list;
            return View();
        }

        public ActionResult c(int pageIndex = 1)
        {
            string str_search = Request.QueryString["StrSearch"];
            //热门搜索
            List<string> list = search.GetHostestSearch();

            if (str_search != null)
            {
                str_search = HttpUtility.UrlDecode(str_search.ToString());
                if (str_search.ToString().Trim() == "")
                {
                    ViewBag.CurrentSearch = "";
                    ViewBag.ResultNum = 0;
                }
                else
                {
                    ViewBag.CurrentSearch = str_search;
                    List<Courses> cou_list = new List<Courses>();

                    int resultNum = new Course().GetList_Where(c => c.Cou_Name.Contains(str_search) || c.Cou_Describe.Contains(str_search)).Count;
                    int pageNum = Convert.ToInt32(Math.Ceiling(resultNum / 10.0));
                    ViewBag.pageNum = pageNum;
                    ViewBag.ResultNum = resultNum;

                    #region 分页获取数据

                    if (pageIndex > pageNum)
                    {
                        cou_list = search.GetSearchResult_Course(str_search, pageIndex: pageNum);
                        ViewBag.CourseList = cou_list;
                        ViewBag.CurrentPageIndex = pageNum;
                    }
                    else if (pageIndex < 1)
                    {
                        cou_list = search.GetSearchResult_Course(str_search);
                        ViewBag.CourseList = cou_list;
                        ViewBag.CurrentPageIndex = 1;
                    }
                    else
                    {
                        cou_list = search.GetSearchResult_Course(str_search, pageIndex: pageIndex);
                        ViewBag.CourseList = cou_list;
                        ViewBag.CurrentPageIndex = pageIndex;
                    }
                }
                    #endregion

            }
            else
            {
                ViewBag.CurrentSearch = "";
                ViewBag.ResultNum = 0;
            }

            ViewBag.HostSearch = list;
            return View();
        }

        public ActionResult q(int pageIndex = 1)
        {
            string str_search = Request.QueryString["StrSearch"];
            //热门搜索
            List<string> list = search.GetHostestSearch();

            if (str_search != null)
            {
                str_search = HttpUtility.UrlDecode(str_search.ToString());
                if (str_search.ToString().Trim() == "")
                {
                    ViewBag.CurrentSearch = "";
                    ViewBag.ResultNum = 0;
                }
                else
                {
                    ViewBag.CurrentSearch = str_search;
                    List<Questions> question_list = new List<Questions>();

                    int resultNum = new Question().GetList_Where(q => q.Q_Title.Contains(str_search) || q.Q_Content.Contains(str_search)).Count;
                    int pageNum = Convert.ToInt32(Math.Ceiling(resultNum / 10.0));
                    ViewBag.pageNum = pageNum;
                    ViewBag.ResultNum = resultNum;

                    #region 分页获取数据

                    if (pageIndex > pageNum)
                    {
                        question_list = search.GetSearchResult_Question(str_search, pageIndex: pageNum);
                        ViewBag.QuestionList = question_list;
                        ViewBag.CurrentPageIndex = pageNum;
                    }
                    else if (pageIndex < 1)
                    {
                        question_list = search.GetSearchResult_Question(str_search);
                        ViewBag.QuestionList = question_list;
                        ViewBag.CurrentPageIndex = 1;
                    }
                    else
                    {
                        question_list = search.GetSearchResult_Question(str_search, pageIndex: pageIndex);
                        ViewBag.QuestionList = question_list;
                        ViewBag.CurrentPageIndex = pageIndex;
                    }
                }

                    #endregion

            }
            else
            {
                ViewBag.CurrentSearch = "";
                ViewBag.ResultNum = 0;
            }

            ViewBag.HostSearch = list;
            return View();
        }

        public ActionResult n(int pageIndex = 1)
        {
            string str_search = Request.QueryString["StrSearch"];
            //热门搜索
            List<string> list = search.GetHostestSearch();

            if (str_search != null)
            {
                str_search = HttpUtility.UrlDecode(str_search.ToString());
                if (str_search.ToString().Trim() == "")
                {
                    ViewBag.CurrentSearch = "";
                    ViewBag.ResultNum = 0;
                }
                else
                {
                    ViewBag.CurrentSearch = str_search;
                    List<Notes> note_list = new List<Notes>();

                    int resultNum = new Note().GetList_Where(n => n.N_Title.Contains(str_search) || n.N_Content.Contains(str_search)).Count;
                    int pageNum = Convert.ToInt32(Math.Ceiling(resultNum / 10.0));
                    ViewBag.pageNum = pageNum;
                    ViewBag.ResultNum = resultNum;

                    #region 分页获取数据

                    if (pageIndex > pageNum)
                    {
                        note_list = search.GetSearchResult_Note(str_search, pageIndex: pageNum);
                        ViewBag.NoteList = note_list;
                        ViewBag.CurrentPageIndex = pageNum;
                    }
                    else if (pageIndex < 1)
                    {
                        note_list = search.GetSearchResult_Note(str_search);
                        ViewBag.NoteList = note_list;
                        ViewBag.CurrentPageIndex = 1;
                    }
                    else
                    {
                        note_list = search.GetSearchResult_Note(str_search, pageIndex: pageIndex);
                        ViewBag.NoteList = note_list;
                        ViewBag.CurrentPageIndex = pageIndex;
                    }
                }

                    #endregion

            }
            else
            {
                ViewBag.CurrentSearch = "";
                ViewBag.ResultNum = 0;
            }

            ViewBag.HostSearch = list;
            return View();
        }
    }
}