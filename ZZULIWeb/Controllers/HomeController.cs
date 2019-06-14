using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class HomeController : Controller
    {
        Navigation nav = new Navigation();

        [HttpGet]
        public ActionResult Index()
        {
            //功能分区
            List<Partition> partList = nav.GetPartition();
            ViewBag.PartList = partList;

            //导航栏
            List<Navigations> navList0 = nav.GetNavigation0();
            ViewBag.NavList0 = navList0;
            List<Navigations> navList1 = nav.GetNavigation1(0);
            ViewBag.NavList1 = navList1;

            //好课推荐
            List<Courses> courseList = new Course().GetList_TopN(n => n.Cou_ID > 0, n => n.Cou_StudyNum, 10);
            ViewBag.CourseList = courseList;

            ViewBag.Title = "资源共享平台";

            return View();
        }


    }
}