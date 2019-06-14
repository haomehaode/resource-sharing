using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class LoginController : Controller
    {
        User user = new User();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetHeader()
        {
            string account = Request.Form["account"].ToString();
            UserInfo userInfo = user.GetList_Where(u => u.User_Account == account).FirstOrDefault();
            if (userInfo != null)
                return Json("{'status':'1','u_id':'" + userInfo.User_ID + "'}");
            else
                return Json("{'status':'0'}");
        }

        public JsonResult ValidateLogin()
        {
            string account = Request.Form["account"].ToString();
            string pwd = Request.Form["pwd"].ToString();
            bool repwd = Convert.ToBoolean(Request.Form["repwd"]);
            UserInfo userInfo = user.GetList_Where(u => u.User_Account == account && u.User_Password == pwd).FirstOrDefault();
            if (userInfo != null)
            {
                Session.Timeout = 30;
                Session.Add("UserInfo", userInfo.User_ID);
                user.ModifyLastLoginTime(userInfo.User_ID);
                //Common.UserInfo = userInfo;
                if (repwd)//下次自动登录
                {
                    HttpCookie cook = new HttpCookie("UserInfo");
                    cook.Expires = DateTime.Now.AddDays(30);
                    cook.Value = userInfo.User_ID.ToString();
                    //cook["User_ID"] = userInfo.User_ID.ToString();
                    //cook.Values.Add("User_ID", userInfo.User_ID.ToString());
                    //cook.Values.Add("User_Account", userInfo.User_Account);
                    //cook.Values.Add("User_Password", userInfo.User_Password);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cook);
                    //Response.Cookies.Add(cook);
                }
                else//禁止下次自动登录
                {
                    //Request.Cookies.Remove("UserInfo");
                    //Response.Cookies.Remove("UserInfo");
                    HttpCookie cook = new HttpCookie("UserInfo");
                    cook.Expires = DateTime.Now.AddSeconds(1);
                    cook.Value = userInfo.User_ID.ToString();
                    //cook.Values.Add("User_Account", userInfo.User_Account);
                    //cook.Values.Add("User_Password", userInfo.User_Password);
                    Response.Cookies.Add(cook);
                }
                return Json("{'status':'1'}");
            }
            else
                return Json("{'status':'0'}");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult GetValidationCode()
        { //设置图片上显示的文本
            string checkCode = "";
            int len = 6;
            string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random ran = new Random();
            for (int i = 0; i < len; i++)
            {
                checkCode += str.Substring(GetRandom(0, 62, ran), 1);
            }
            //根据文本长度确定图片的长度
            Bitmap image = new Bitmap((int)Math.Ceiling(checkCode.Length * 25.0), 35);
            //创建画板
            Graphics g = Graphics.FromImage(image);

            Random random = new Random();
            g.Clear(Color.White);

            #region 画图片的背景噪音线
            for (int i = 0; i < 10; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
            }
            #endregion
            //设置文本的格式
            Font font = new Font("Arial", 20, FontStyle.Bold);
            //设置渐变画刷
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(checkCode, font, brush, 2, 2);

            #region 画图片的前景噪音点

            for (int i = 0; i < 500; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                //设置图片中指定像素点的颜色
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            #endregion

            #region 画图片的边框线

            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            #endregion

            MemoryStream ms = new MemoryStream();
            //指定图片的格式，并将其保存到流中
            image.Save(ms, ImageFormat.Gif);
            ////清空缓冲区所有内容
            //Response.ClearContent();
            ////设置输出类型
            //Response.ContentType = "image/Gif";
            //Response.BinaryWrite(ms.ToArray());

            Session.Timeout = 1;
            string val = Session.SessionID;
            Session.Add("ValidationCode", checkCode.ToLower());

            return File(ms.ToArray(), "image/Gif");
        }

        public JsonResult AccountIsExist()
        {
            string account = Request.Form["account"].ToString();
            UserInfo userinfo = user.GetList_Where(u => u.User_Account == account).FirstOrDefault();
            if (userinfo != null)
            {
                return Json("{'status':'1'}");
            }
            return Json("{'status':'0'}");
        }

        public int GetRandom(int min, int max, Random ran = null)
        {
            if (ran == null)
                ran = new Random();
            return ran.Next(min, max);
        }

        public ActionResult ValidValidationCode()
        {
            string code = Request.Form["ValidationCode"].ToString().ToLower();
            if (code == Session["ValidationCode"].ToString())
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        public ActionResult DoRegister()
        {
            string account = Request.Form["account"].ToString();
            string pwd = Request.Form["pwd"].ToString();
            int res = user.Add(new UserInfo() { User_Account = account, User_Password = pwd, User_Priority = 3, User_Priority_Now = 3, User_LastLoginTime = DateTime.Now, User_NickName = "用户_" + account, });
            if (res > 0)
            {
                return Json("{'status':'1'}");
            }
            return Json("{'status':'0'}");
        }
    }
}