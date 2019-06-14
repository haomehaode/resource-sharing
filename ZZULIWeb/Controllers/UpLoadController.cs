using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using ZZULIWeb.Models;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ZZULIWeb.Controllers
{
    public class UpLoadController : Controller
    {
        ZZULIEntities db = new ZZULIEntities();
        Resource re = new Resource();
        //
        // GET: /UpLoad/
        public ActionResult Index()
        {
            ViewBag.TagsList = new Navigation().GetAllTags();
            return View();
        }

        public ActionResult upload()
        {
            //string name = Request.QueryString["Cou_Name"];
            //string title = Request.QueryString["Cou_Title"];
            //Stream stream = Request.InputStream;
            //long length = stream.Length;
            //byte[] by = new byte[1024 * 1024];
            //stream.Read(by, 0, by.Length);
            ////fs.Write(by, 0, by.Length);

            //int leng;
            //using (FileStream fs = new FileStream("E:\\tttt.txt", FileMode.Create, FileAccess.ReadWrite))
            //{
            //    while ((leng = stream.Read(by, 0, by.Length)) != 0)
            //    {
            //        fs.Write(by, 0, by.Length);
            //        fs.Flush();
            //    }
            //}
            //string[] keys = System.Web.HttpContext.Current.Request.Files.AllKeys;
            //string[] ke = System.Web.HttpContext.Current.Request.Form.AllKeys;
            //foreach (string s in ke)
            //{
            //    var value = System.Web.HttpContext.Current.Request.Files[s];
            //}
            //string s = Request.Form["Cou_Describe"].ToString();
            //string des = Request["Cou_Describe"].ToString();
            //var image = Request["Cou_Img"];
            //var tags = Request["tags"];
            //new Courses() { Cou_Describe = Request["Cou_Describe"].ToString(), Cou_Name = Request["Cou_Name"].ToString(), Cou_Time = DateTime.Now, User_ID = Common.UserInfo.User_ID };
            //bool res = ModelState.IsValid;
            //Course c = new Course();
            //cou.Cou_Time = DateTime.Now;
            //cou.User_ID = Common.UserInfo.User_ID;
            //int a = c.Add(cou);
            //string tags = Request.Form["tags"];

            //HttpPostedFileBase img = Request.Files["Cou_Img"];
            //int imgLength = img.ContentLength;


            //HttpPostedFileBase cou_name = Request.Files["Cou_Name"];
            //int cou_nameLength = cou_name.ContentLength;


            //Image ig = Image.FromStream(img.InputStream);

            //Bitmap bitmap = new Bitmap(228, 128);
            //Graphics g = Graphics.FromImage(bitmap);
            //g.DrawImage(ig, 0, 0, bitmap.Width, bitmap.Height);
            //bitmap.Save(Server.MapPath("/images/move/") + a + ".png");

            //HttpPostedFileBase file = Request["Moves"];
            //int fileLength = file.ContentLength;
            //Stream uploadStream = file.InputStream;
            //string fileName = Path.GetFileName(file.FileName);
            //string baseurl = Server.MapPath("/upload/course/");


            return Json("{'status':'Success'}");
            //int bufferLength = 1024;
            //byte[] buffer = new byte[bufferLength];
            //int contentLength = 0;

            //FileStream fs = new FileStream(Server.MapPath("/upload/move/") + a + ".zip", FileMode.Create, FileAccess.ReadWrite);
            //while ((contentLength = uploadStream.Read(buffer, 0, bufferLength)) != 0)
            //{
            //    fs.Write(buffer, 0, buffer.Length);
            //    fs.Flush();
            //}




            //Upload up = new Upload(cou, img, file, tags);
            //EnumUploadResult res = up.StartUpload();
            //return Json("{'status':'" + res.ToString() + "'}");


            //return Content("fafjafj;");

            //Stream stream = Request.InputStream;
            //byte[] temp = new byte[stream.Length];
            //string content = temp.ToString();
            //stream.Read(temp, 0, temp.Length);
            //string filename = Request.Form["FileName"];
            //FileStream fs = new FileStream("E:\\" + filename, FileMode.Create, FileAccess.Write);
            //fs.Write(temp, 0, temp.Length);
            //string path = Request.FilePath;
            //string url = Request.MapPath(filename);
            //int count = Request.Files.Count;
            //string[] filenames = Request.Files.AllKeys;
            //return Content("fjakfjl");
        }

        //[Route("upload")]
        public ActionResult uploadbigfile()
        {

            //前端传输是否为切割文件最后一个小文件
            var isLast = System.Web.HttpContext.Current.Request["isLast"];
            //前端传输当前为第几次切割小文件
            var count = System.Web.HttpContext.Current.Request["count"];
            //获取前端处理过的传输文件名
            string fileName = System.Web.HttpContext.Current.Request["name"];
            //存储接受到的切割文件
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
            //获取重传次数
            var retransNum_Part = System.Web.HttpContext.Current.Request["retransNum_Part"];




            //处理文件名称(去除.part*，还原真实文件名称)
            string newFileName = fileName.Substring(0, fileName.LastIndexOf('.'));

            //获取临时文件夹、压缩包文件、解压文件夹的路径
            string tempUrl = Server.MapPath(@"\upload\temp\");
            string videoUrl = Server.MapPath(@"\video\");

            //判断指定目录是否存在临时存储文件夹，没有就创建
            if (!System.IO.Directory.Exists(tempUrl + "\\" + newFileName))
            {
                //不存在就创建目录 
                System.IO.Directory.CreateDirectory(tempUrl + "\\" + newFileName);
            }
            if (file == null)
            {
                return Json("{'status':'2','statusText':'" + "分片文件保存失败!" + "','partIndex':'" + count + "','retransNum_Part':'" + retransNum_Part + "'}");
            }

            try
            {
                //存储文件
                file.SaveAs(tempUrl + "\\" + newFileName + "\\" + fileName);
            }
            catch
            {
                if (Convert.ToInt32(retransNum_Part) >= 3)
                {
                    Upload.ClearErrorFile(0, newFileName, tempUrl, Server.MapPath(@"\upload\zip\"), Server.MapPath(@"\upload\unzip\"), videoUrl);
                }
                return Json("{'status':'2','statusText':'" + "分片文件保存失败!" + "','partIndex':'" + count + "','retransNum_Part':'" + retransNum_Part + "'}");
            }

            //判断是否为最后一次切割文件传输
            int cid = 0;

            int errorNum = 0;

            bool res = false;
            if (isLast == "true")
            {
                //课程名称
                var couName = System.Web.HttpContext.Current.Request["Cou_Name"];
                //课程介绍
                var couDescribe = System.Web.HttpContext.Current.Request["Cou_Describe"];
                //课程标签
                int couTag = Convert.ToInt32(System.Web.HttpContext.Current.Request["Cou_Tag"]);
                int num = System.Web.HttpContext.Current.Request.Files.Count;
                HttpPostedFile image = System.Web.HttpContext.Current.Request.Files[1];

                //获取重传次数
                var retransNum = System.Web.HttpContext.Current.Request["retransNum"];


                string zipUrl = Server.MapPath(@"\upload\zip\");
                string unzipUrl = Server.MapPath(@"\upload\unzip\");
                //将课程的信息保存到数据库中，此时课程的状态为正在上传，用户不可以查看和学习该课程
                cid = Upload.SaveCourseInfo(couName, couDescribe);


                while (cid <= 0 && errorNum < 3)
                {
                    cid = Upload.SaveCourseInfo(couName, couDescribe);
                    errorNum++;
                }
                if (cid <= 0)
                {
                    return Json("{'status':'3','statusText':'" + "保存课程信息失败!" + "'}");
                }
                errorNum = 0;

                //将分片文件，组合成上传的压缩包源文件
                res = Upload.ComposeFile(cid, newFileName, zipUrl, tempUrl);

                while (!res && errorNum < 3)
                {
                    res = Upload.ComposeFile(cid, newFileName, zipUrl, tempUrl);
                    errorNum++;
                }
                if (!res)
                {
                    Upload.ClearErrorFile(cid, newFileName, tempUrl, zipUrl, unzipUrl, videoUrl);
                    return Json("{'status':'4','statusText':'" + "文件合成失败!" + "'}");
                }
                errorNum = 0;
                res = false;

                //解压课程文件
                res = Upload.UnZip(cid, newFileName, zipUrl + cid + ".zip", unzipUrl);

                while (!res && errorNum < 3)
                {
                    res = Upload.ComposeFile(cid, newFileName, zipUrl + cid + ".zip", unzipUrl);
                    errorNum++;
                }
                if (!res)
                {
                    return Json("{'status':'5','statusText':'" + "文件解压失败!" + "'}");
                }
                errorNum = 0;
                res = false;



                //检查文件是否是有效的文件
                res = Upload.CheckCourseFile(unzipUrl + cid);
                if (!res)
                {
                    Upload.ClearErrorFile(cid, newFileName, tempUrl, zipUrl, unzipUrl, videoUrl);
                    return Json("{'status':'6','statusText':'" + "该文件不符合上传要求!" + "'}");
                }
                errorNum = 0;
                res = false;
                //将课程的章节放到video对应目录下，并将章节信息写入数据库，并将数据库中课程对应的状态为已上传，允许用户访问
                res = Upload.SaveChapterSectionInfo(cid, unzipUrl + cid, Server.MapPath(@"\video\"));

                while (!res && errorNum < 3)
                {
                    res = Upload.SaveChapterSectionInfo(cid, unzipUrl + cid, videoUrl);
                    errorNum++;
                }
                if (!res)
                {
                    return Json("{'status':'7','statusText':'" + "课程章节信息保存失败!" + "'}");
                }
                errorNum = 0;
                res = false;

                var course = db.Courses.Where(c => c.Cou_ID == cid).FirstOrDefault();
                course.Is_UpLoading = false;
                db.SaveChanges();

                //将课程的标签保存到数据库中
                res = Upload.SaveCourseTags(cid, couTag);
                while (!res && errorNum < 3)
                {
                    res = Upload.SaveCourseTags(cid, couTag);
                    errorNum++;
                }
                if (!res)
                {
                    return Json("{'status':'8','statusText':'" + "课程标签保存失败!" + "'}");
                }
                errorNum = 0;
                res = false;

                //保存课程图片
                res = Upload.SaveCourseImage(cid, image, Server.MapPath(@"\images\course\") + cid + ".png");
                if (res == null)
                {
                    return Json("{'status':'9','statusText':'" + "课程图片保存失败!" + "','retransNum':'" + retransNum + "'}");
                }
                while (!res && errorNum < 3)
                {
                    res = Upload.SaveCourseImage(cid, image, Server.MapPath(@"\images\course\") + cid + ".png");
                    errorNum++;
                }
                if (!res)
                {
                    Upload.ClearErrorFile(cid, newFileName, tempUrl, zipUrl, unzipUrl, videoUrl);
                    return Json("{'status':'9','statusText':'" + "课程图片保存失败!" + "'}");
                }
            }
            return Json("{'status':'1','statusText':'" + cid + "'}");
            //return int.Parse(count) + 1;
        }

        public ActionResult uploadcourseimage()
        {
            //var cid = System.Web.HttpContext.Current.Request["cid"];
            //HttpPostedFile image = System.Web.HttpContext.Current.Request.Files[0];
            //try
            //{
            //    image.SaveAs(Server.MapPath(@"\images\course\") + cid + ".png");
            //    //Bitmap bitmap = new Bitmap(228, 128);
            //    //Graphics g = Graphics.FromImage(bitmap);
            //    //g.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
            //    //bitmap.Save(@"\images\move" + this.cid + ".png");
            //}
            //catch
            //{
            //    if (System.IO.File.Exists(@"\images\move\" + cid + ".png"))
            //    {
            //        System.IO.File.Delete(@"\images\move\" + cid + ".png");
            //        return Json("{'status':'0','statusText':'课程图片上传失败,请重试!'");
            //    }
            //}
            return Json("{'status':'1','statusText':'课程图片上传成功!'");
        }
    }
}