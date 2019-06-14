using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class RecommendController : Controller
    {
        User user = new User();

        //
        // GET: /Recommend/
        public ActionResult Course()
        {
            //Matrix ma = new Matrix();
            //ma.Test();
            if (Common.UserInfo == null)
                return RedirectToAction("Index", "Login");
            ViewBag.RecommendCourseList = user.GetRecommendCourse(Common.UserInfo.User_ID);
            return View();
        }
        public ActionResult Question()
        {
            //Matrix ma = new Matrix();
            //ma.Test();
            if (Common.UserInfo == null)
                return RedirectToAction("Index", "Login");
            ViewBag.RecommendQuestionList = user.GetRecommendQuestion(Common.UserInfo.User_ID);
            return View();
        }
        public ActionResult Note()
        {
            //Matrix ma = new Matrix();
            //ma.Test();
            if (Common.UserInfo == null)
                return RedirectToAction("Index", "Login");
            ViewBag.RecommendNoteList = user.GetRecommendNote(Common.UserInfo.User_ID);
            return View();
        }
    }
}