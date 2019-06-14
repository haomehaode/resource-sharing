using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class QuestionController : Controller
    {
        Question ques = new Question();
        //
        // GET: /Question/
        public ActionResult List(int pageIndex = 1)
        {
            //List<Questions> questionlist = ques.GetList_TopN(q => q.Q_ID > 0, q => q.Q_Hot, 20);
            //ViewBag.QuestionList = questionlist;


            int pageNum = Convert.ToInt32(Math.Ceiling(ques.GetList_Where(q => q.Q_ID > 0).Count / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<Questions> questionlist = ques.GetList_Page(q => q.Q_ID > 0, q => q.Q_Hot, pageNum, 10);
                ViewBag.QuestionList = questionlist;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<Questions> questionlist = ques.GetList_Page(q => q.Q_ID > 0, q => q.Q_Hot, 1, 10);
                ViewBag.QuestionList = questionlist;
                ViewBag.CurrentPageIndex = 1;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<Questions> questionlist = ques.GetList_Page(q => q.Q_ID > 0, q => q.Q_Hot, pageIndex, 10);
                ViewBag.QuestionList = questionlist;
                ViewBag.CurrentPageIndex = pageIndex;
            }

            return View();
        }

        public ActionResult Index()
        {
            if (Common.UserInfo == null)
                return RedirectToAction("Index", "Login");
            ViewBag.TagsList = new Navigation().GetAllTags();
            return View();
        }

        public ActionResult des(int id, int pageIndex = 1)
        {
            if (Common.UserInfo == null)
                return RedirectToAction("Index", "Login");
            //记录用户的浏览行为
            Common.PageView(id, 2, Common.UserInfo.User_ID);
            Common.ModifyPageView(id, 2);


            int pageNum = Convert.ToInt32(Math.Ceiling(ques.GetAnswerNum(id) / 10.0));
            ViewBag.pageNum = pageNum;
            if (pageNum <= 0)
            {
                ViewBag.AnswerList = null;
            }
            else if (pageIndex < 1)
            {
                //3、根据课程ID获取课程评论信息
                List<Answers> an_list = ques.GetAnswer_Page(id, 1, 10, 5);
                ViewBag.AnswerList = an_list;
                ViewBag.CurrentPageIndex = 1;
            }
            else if (pageIndex > pageNum)
            {
                //3、根据课程ID获取课程评论信息
                List<Answers> an_list = ques.GetAnswer_Page(id, pageNum, 10, 5);
                ViewBag.AnswerList = an_list;
                ViewBag.CurrentPageIndex = pageNum;
            }
            else
            {
                //3、根据课程ID获取课程评论信息
                List<Answers> an_list = ques.GetAnswer_Page(id, pageIndex, 10, 5);
                ViewBag.AnswerList = an_list;
                ViewBag.CurrentPageIndex = pageIndex;
            }

            //获取问题的回答
            //List<Answers> answerList = ques.GetAnswer_Page(id);
            //ViewBag.AnswerList = answerList;

            //获取相关问题，先找到当前问题的标签，也就是问题来自哪儿，获取标签中含有这些标签的问题，根据热度排序，获取TopN5
            List<int> tagids = Common.GetFrom_Question(id).Select(n => n.Nav_ID).ToList();
            List<Questions> list = new ObjectTag().GetQuestionByTags_TopN(id, tagids, 5);

            Questions question = ques.GetList_Where(q => q.Q_ID == id).FirstOrDefault();
            ViewBag.RelevantQuestion = list;
            ViewBag.TagList = Common.GetFrom_Question(id);
            return View(question);
        }
    }
}