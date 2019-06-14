using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;

namespace ZZULIWeb.Controllers
{
    public class CommonController : Controller
    {
        string error = "";
        ZZULIEntities db = new ZZULIEntities();
        Resource re = new Resource();

        [HttpPost]
        public JsonResult Like(int target_id, int optionType)
        {
            if (Common.UserInfo == null)
                return Json("{'status':'2'}");
            if (Common.Like(target_id, optionType))
            {
                //此处 记录用户行为
                UserActionRecord.Recommend(Common.UserInfo.User_ID, target_id, optionType);

                return Json("{'status':'1'}");
            }
            else
                return Json("{'status':'0';'error':'" + error + "'}");
        }

        [HttpPost]
        public JsonResult AbortLike(int target_id, int optionType)
        {
            if (Common.AbortLike(target_id, optionType))
            {
                //此处 删除用户行为信息
                UserActionRecord.DelRecommend(Common.UserInfo.User_ID, target_id, optionType);

                return Json("{'status':'1'}");
            }
            else
                return Json("{'status':'0';'error':'" + error + "'}");
        }

        [HttpPost]
        public JsonResult NotLike(int target_id, int optionType)
        {
            if (Common.UserInfo == null)
                return Json("{'status':'2'}");
            if (Common.NotLike(target_id, optionType))
            {
                //此处 记录用户行为
                UserActionRecord.NotRecommend(Common.UserInfo.User_ID, target_id, optionType);

                return Json("{'status':'1'}");
            }
            else
                return Json("{'status':'0';'error':'" + error + "'}");
        }

        [HttpPost]
        public JsonResult AbortNotLike(int target_id, int optionType)
        {
            if (Common.AbortNotLike(target_id, optionType))
            {
                //此处 删除用户行为
                UserActionRecord.DelNotRecommend(Common.UserInfo.User_ID, target_id, optionType);

                return Json("{'status':'1'}");
            }
            else
                return Json("{'status':'0';'error':'" + error + "'}");
        }

        [HttpPost]
        public JsonResult Collect(int target_id, int optionType)
        {
            if (Common.UserInfo == null)
                return Json("{'status':'2'}");
            if (Common.Collect(target_id, optionType))
            {
                UserActionRecord.Collect(Common.UserInfo.User_ID, target_id, optionType);
                return Json("{'status':'1'}");
            }
            else
            {
                UserActionRecord.AbortCollect(Common.UserInfo.User_ID, target_id, optionType);
                return Json("{'status':'0'}");
            }
        }
        [HttpPost]
        public JsonResult AbortCollect(int target_id, int optionType)
        {
            if (Common.AbortCollect(target_id, optionType))
                return Json("{'status':'1'}");
            else
                return Json("{'status':'0'}");
        }

        public JsonResult Follow(int target_id)
        {
            if (Common.UserInfo == null)
                return Json("{'status':'2'}");
            if (Common.Follow(Common.UserInfo.User_ID, target_id))
            {
                //用户行为记录
                UserActionRecord.Follow(Common.UserInfo.User_ID, target_id);
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'");
            }
        }
        public JsonResult AbortFollow(int target_id)
        {
            if (Common.AbortFollow(Common.UserInfo.User_ID, target_id))
            {
                UserActionRecord.AbortFollow(Common.UserInfo.User_ID, target_id);
                //删除用户行为记录
                return Json("{'status':'1'");
            }
            else
            {
                return Json("{'status':'0'");
            }
        }

        [ValidateInput(false)]
        public JsonResult Submit_CourseComment(int id, int movid, string content)
        {
            if (Common.UserInfo == null)
            {
                return Json("{'status':'2'}");
            }
            if (Common.Submit_CourseComment(Common.UserInfo.User_ID, id, movid, content))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        //public JsonResult Submit_CourseQuestion(int id, int movid, string content)
        //{
        //}

        [ValidateInput(false)]
        public JsonResult Submit_Question(string title, string content, string content_style, List<string> tags)
        {
            if (Common.UserInfo == null)
            {
                return Json("{'status':'2'}");
            }
            if (Common.Submit_Qeustion(Common.UserInfo.User_ID, title, content, content_style, tags))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        [ValidateInput(false)]
        public JsonResult Submit_CourseNote(int id, int movid, string content)
        {
            if (Common.UserInfo == null)
            {
                return Json("{'status':'2'}");
            }
            else if (Common.Submit_CourseNote(Common.UserInfo.User_ID, id, movid, content))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        [ValidateInput(false)]
        public JsonResult Submit_Note(string title, string content, string content_style, List<string> tags)
        {
            if (Common.UserInfo == null)
            {
                return Json("{'status':'2'}");
            }
            if (Common.Submit_Note(Common.UserInfo.User_ID, title, content, content_style, tags))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        [ValidateInput(false)]
        public JsonResult Submit_NoteComment(int nid, string content)
        {
            if (Common.UserInfo == null)
            {
                return Json("{'status':'2'}");
            }
            if (Common.Submit_NoteComment(Common.UserInfo.User_ID, nid, content))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        [ValidateInput(false)]
        public JsonResult Submit_QuestionAnswer(int qid, string content)
        {
            if (Common.UserInfo == null)
                return Json("{'status':'2'}");
            if (Common.Submit_QuestionAnswer(Common.UserInfo.User_ID, qid, content))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        [ValidateInput(false)]
        public JsonResult Submit_QuestionAnswerOfAnswer(int qid, int aid, string content)
        {
            if (Common.UserInfo == null)
            {
                return Json("{'status':'2'}");
            }
            if (Common.Submit_QuestionAnswerOfAnswer(Common.UserInfo.User_ID, qid, aid, content))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        [ValidateInput(false)]
        public JsonResult Submit_CourseQuestion(int cid, int mid, string title, string content)
        {
            if (Common.UserInfo == null)
            {
                return Json("{'status':'2'}");
            }
            if (Common.Submit_CourseQuestion(Common.UserInfo.User_ID, cid, mid, title, content))
            {
                return Json("{'status':'1'}");
            }
            else
            {
                return Json("{'status':'0'}");
            }
        }

        //长轮询
        public JsonResult Link(string type)
        {
            try
            {
                bool res = Common.Link(type);
                if (res)
                    return Json("{'status':'1'}");
                else
                    return Json("{'status':'0'}");
            }
            catch
            {
                return Json("{'status':'0'}");
            }
        }

        //public JsonResult SubmitData_Comment_NotTarget(int id, int optionType, string content)
        //{

        //    if (Common.UserInfo == null)
        //    {
        //        return Json("{'status':'2'}");
        //    }
        //    if (Common.SubmitData_Comment_NotTarget(Common.UserInfo.User_ID, id, optionType, content))
        //    {
        //        return Json("{'status':'1'}");
        //    }
        //    else
        //    {
        //        return Json("{'status':'0'}");
        //    }
        //}

        //public JsonResult SubmitData_Comment_Target(int id, int optionType, int target_id, string content)
        //{
        //    if (Common.UserInfo == null)
        //    {
        //        return Json("{'status':'2'}");
        //    }
        //    if (Common.SubmitData_Comment_Target(Common.UserInfo.User_ID, id, optionType, target_id, content))
        //    {
        //        return Json("{'status':'1'}");
        //    }
        //    else
        //    {
        //        return Json("{'status':'0'}");
        //    }
        //}

        //public JsonResult SubmitData_Question_NotTags(int id, int optionType, string title, string content)
        //{
        //    if (Common.UserInfo == null)
        //    {
        //        return Json("{'status':'2'}");
        //    }
        //    if (Common.SubmitData_Question_NotTags(Common.UserInfo.User_ID, id, optionType, title, content))
        //    {
        //        return Json("{'status':'1'}");
        //    }
        //    else
        //    {
        //        return Json("{'status':'0'}");
        //    }
        //}

        //public JsonResult SubmitData_Question_Tags(int optionType, string title, string content, List<string> tags)
        //{
        //    if (Common.UserInfo == null)
        //    {
        //        return Json("{'status':'2'}");
        //    }
        //    if (Common.SubmitData_Question_Tags(Common.UserInfo.User_ID, optionType, title, content, tags))
        //    {
        //        return Json("{'status':'1'}");
        //    }
        //    else
        //    {
        //        return Json("{'status':'0'}");
        //    }
        //}

        //public JsonResult SubmitData_Notes(int optionType, string title, string content, List<string> tags)
        //{
        //    if (Common.UserInfo == null)
        //    {
        //        return Json("{'status':'2'}");
        //    }
        //    if (Common.SubmitData_Notes(Common.UserInfo.User_ID, optionType, title, content, tags))
        //    {
        //        return Json("{'status':'1'}");
        //    }
        //    else
        //    {
        //        return Json("{'status':'0'}");
        //    }
        //}


        public ActionResult getnav1(int nav0)
        {
            if (nav0 == -1)
            {
                return Content("<option value='-1'>--未选择--</option>");
            }
            //int nav0 = Convert.ToInt32(Request.QueryString["nav0"]);
            List<Navigations> list = new Navigation().GetNavigation1(nav0);
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value='-1'>--未选择--</option>");
            list.ForEach(l => sb.Append("<option value='" + l.Nav_ID + "'>" + l.Nav_Name + "</option>"));
            return Content(sb.ToString());
        }


        public ActionResult Clear()
        {
            try
            {
                HttpCookie aCookie;
                string cookieName;
                int limit = System.Web.HttpContext.Current.Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    cookieName = System.Web.HttpContext.Current.Request.Cookies[i].Name;
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    System.Web.HttpContext.Current.Response.Cookies.Add(aCookie);
                }
                if (Session["UserInfo"] != null)
                {
                    Session.Remove("UserInfo");
                }
                if (Common.UserInfo != null)
                {
                    Common.UserInfo = null;
                }
                return Json("{'status':'1'}");
            }
            catch
            {
                return Json("{'status':'0'}");
            }
        }

    }
}