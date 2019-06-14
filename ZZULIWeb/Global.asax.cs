using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ZZULIWeb.Models;

namespace ZZULIWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //自动登录
            //if (Common.UserInfo == null)
            //{
            //HttpApplication app = sender as HttpApplication;
            //if (app.Request.Cookies["UserInfo"] != null)
            //{
            //    int id = Convert.ToInt32(app.Request.Cookies["UserInfo"].Values["User_ID"]);
            //    Common.UserInfo = new ZZULIEntities().UserInfo.Where(u => u.User_ID == id).FirstOrDefault();
            //}
            //}
            //List<string> valiList = new List<string>() { "/User/Index", "/User/LearnedCourse" };

            //string url = app.Request.RawUrl;
            //string[] str = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            //string s = "";
            //if (str.Length >= 2)
            //    s = "/" + str[0] + "/" + str[1];
            //if (valiList.Contains(s))
            //{
            //    if (app.Request.Cookies["UserInfo"] == null)
            //    {
            //        app.Response.ContentType = "text/html";
            //        app.Response.Write("<script>alert('请登录！');window.location='/Login/Index'</script>");
            //    }
            //}
            //用户进入默写页面时，先判断是否已经登录，如果没有登录，则跳转到登录页面
            //string str = app.Request.RawUrl;
            //if (str == "/" && Common.UserInfo == null)
            //{
            //    app.Response.Redirect("~/Login/Index");
            //}
        }
    }
}
