using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class DownLoadController : Controller
    {
        ZZULIEntities db = new ZZULIEntities();
        Resource re = new Resource();
        //
        // GET: /DownLoad/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getcontent(int nav0 = -1, int nav1 = -1, int pageIndex = 1)
        {
            //"-1,-1"
            //string[] data = Request.Form["Nav"].ToString().Split(new char[] { ',' });

            //nav0 = Convert.ToInt32(data[0]);
            //nav1 = Convert.ToInt32(data[1]);
            int currentPage = 1;
            int pageNum = 0;
            if (nav0 == -1 && nav1 == -1)//没有过滤
            {
                int datanum = db.ResourceInfo.Where(r => r.R_ID > 0).Count();
                pageNum = Convert.ToInt32(Math.Ceiling(datanum / 10.0));
                if (pageNum < 1)
                {
                    ViewBag.ResourceList = null;
                }
                else
                {
                    if (pageIndex > pageNum)
                    {
                        currentPage = pageNum;
                    }
                    else
                    {
                        currentPage = pageIndex;
                    }
                    ViewBag.ResourceList = re.GetResource_Page(Convert.ToInt32(nav0), Convert.ToInt32(nav1), currentPage);
                }
                //ViewBag.ResourceList = 
            }
            else if (nav0 != -1 && nav1 == -1)//nav0
            {
                int datanum = re.GetResourceByTag(Convert.ToInt32(nav0)).Count;
                pageNum = Convert.ToInt32(Math.Ceiling(datanum / 10.0));
                if (pageNum < 1)
                {
                    ViewBag.ResourceList = null;
                }
                else
                {
                    if (pageIndex > pageNum)
                    {
                        currentPage = pageNum;
                    }
                    else
                    {
                        currentPage = pageIndex;
                    }
                    ViewBag.ResourceList = re.GetResource_Page(Convert.ToInt32(nav0), Convert.ToInt32(nav1), currentPage);
                }
            }
            else//nav0 nav1
            {
                int datanum = re.GetResourceByTag(Convert.ToInt32(nav1)).Count;
                pageNum = Convert.ToInt32(Math.Ceiling(datanum / 10.0));
                if (pageNum < 1)
                {
                    ViewBag.ResourceList = null;
                }
                else
                {
                    if (pageIndex > pageNum)
                    {
                        currentPage = pageNum;
                    }
                    else
                    {
                        currentPage = pageIndex;
                    }
                    ViewBag.ResourceList = re.GetResource_Page(Convert.ToInt32(nav0), Convert.ToInt32(nav1), currentPage);
                }
            }
            return PartialView();
        }


	}
}