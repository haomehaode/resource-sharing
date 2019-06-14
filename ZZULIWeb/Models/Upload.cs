using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ZZULIWeb.Models;
using ZZULIWeb;
using System.Text;

namespace ZZULIWeb.Models
{
    public static class Upload
    {
        //BackgroundWorker worker = null;
        //Courses cou = new Courses();
        //List<string> tags = new List<string>();
        //EnumUploadResult res = EnumUploadResult.Success;
        static ZZULIEntities db = new ZZULIEntities();

        public static bool SaveCourseImage(int cid, HttpPostedFile image, string savePath)
        {
            try
            {
                Image img = Image.FromStream(image.InputStream);
                Bitmap bitmap = new Bitmap(228, 128);
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(img, 0, 0, bitmap.Width, bitmap.Height);
                bitmap.Save(savePath);
            }
            catch
            {
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                return false;
            }
            return true;
        }


        public static bool UnZip(int cid, string newFileName, string couFilePath, string targetPath)
        {
            using (ZipFile zip = ZipFile.Read(couFilePath, new ReadOptions() { Encoding = Encoding.Default }))
            {
                try
                {
                    zip.ExtractAll(targetPath, ExtractExistingFileAction.Throw);
                    FileInfo f = new FileInfo(targetPath + Path.GetFileNameWithoutExtension(newFileName));
                    f.MoveTo(targetPath + cid);
                }
                catch
                {
                    if (Directory.Exists(targetPath + Path.GetFileNameWithoutExtension(newFileName)))
                    {
                        Directory.Delete(targetPath + Path.GetFileNameWithoutExtension(newFileName), true);
                    }
                    if (Directory.Exists(targetPath + cid))
                    {
                        Directory.Delete(targetPath + cid, true);
                    }
                    return false;
                }
            }
            File.Delete(couFilePath);
            return true;
            //return SaveChapterSectionInfo(cid, targetPath);
        }


        public static int SaveCourseInfo(string cname, string cdes)
        {
            if (cname == null || cname.Length <= 0)
                return -1;
            return new Course().Add(new Courses() { Cou_Describe = cdes, Cou_Name = cname, Cou_Time = DateTime.Now, Is_UpLoading = true, User_ID = Common.UserInfo.User_ID });
        }


        public static bool SaveCourseTags(int cid, int tagid)
        {
            db.ObjectTags.Add(new ObjectTags() { Obj_TagID = tagid, Obj_Type = 32, Obj_ID = cid });
            return db.SaveChanges() > 0 ? true : false;
        }


        public static bool SaveChapterSectionInfo(int cid, string dataPath, string savePath)
        {
            List<string> chapters = new List<string>();
            List<string> dirs = Directory.GetDirectories(dataPath).ToList();
            if (dirs.Count <= 0)
            {
                return false;
            }
            int chapterIndex = 1;
            int sectionIndex = 1;
            bool hasSection = false;
            for (int i = 0; i < dirs.Count; i++)
            {
                List<string> sec = GetSectionInfo(dirs[i]).ToList();
                if (sec != null && sec.Count > 0)
                {
                    hasSection = true;

                    //如果文件夹中有视频文件，认为是一章节，就在video文件夹下新建一个同名文件夹
                    //if (!Directory.Exists(savePath))
                    //    Directory.CreateDirectory(savePath);

                    //章信息
                    db.Move.Add(new ZZULIWeb.Move() { Is_Chapter = true, Chap_Name = Path.GetFileNameWithoutExtension(dirs[i]), Cou_ID = cid, Chap_ID = chapterIndex });

                    ////在课程目录下生成章节目录
                    //if (!Directory.Exists(savePath + "\\" + chapterIndex))
                    //{
                    //    Directory.CreateDirectory(savePath + "\\" + chapterIndex);
                    //}

                    //节信息
                    for (int j = 0; j < sec.Count; j++)
                    {
                        int sectionId = new ZZULIWeb.Models.Move().Insert(new ZZULIWeb.Move() { Chap_ID = chapterIndex, Chap_Name = Path.GetFileNameWithoutExtension(dirs[i]), Is_Chapter = false, Section_ID = sectionIndex, Section_Name = Path.GetFileNameWithoutExtension(sec[j]), Cou_ID = cid });


                        //把当前文件夹下的所有视频文件移动到video对应的文件夹下
                        FileInfo f = new FileInfo(sec[j]);
                        //f.MoveTo(savePath + "\\" + chapterIndex + "\\" + sectionId + ".mp4");
                        f.MoveTo(savePath + sectionId + ".mp4");

                        sectionIndex++;
                    }
                    sectionIndex = 1;
                    chapterIndex++;
                }
            }
            if (!hasSection)
            {
                return false;
            }
            if (db.SaveChanges() > 0)
            {
                Directory.Delete(dataPath, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> GetSectionInfo(string url)
        {
            List<string> filepath = Directory.GetFiles(url).ToList();
            List<string> sec = new List<string>();
            filepath.ForEach(f =>
            {
                if (f.EndsWith(".mp4"))
                {
                    sec.Add(f);
                }
            });
            return sec;
        }


        public static bool ComposeFile(int cid, string newFileName, string zipUrl, string tempUrl)
        {
            string exten = Path.GetExtension(newFileName);
            try
            {
                //判断组合的文件是否存在
                if (System.IO.File.Exists(zipUrl + cid + exten))//如果文件存在
                {
                    System.IO.File.Delete(zipUrl + cid + exten);//先删除,否则新文件就不能创建
                }
                //创建空的文件流
                FileStream FileOut = new FileStream(zipUrl + cid + exten, FileMode.CreateNew, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(FileOut);
                //获取临时存储目录下的所有切割文件
                string[] allFile = Directory.GetFiles(tempUrl + newFileName);
                //将文件进行排序拼接
                allFile = allFile.OrderBy(s => int.Parse(Regex.Match(s, @"\d+$").Value)).ToArray();
                //allFile.OrderBy();
                for (int i = 0; i < allFile.Length; i++)
                {
                    FileStream FileIn = new FileStream(allFile[i], FileMode.Open);
                    BinaryReader br = new BinaryReader(FileIn);
                    byte[] data = new byte[1024 * 1024];   //流读取,缓存空间
                    int readLen = 0;                //每次实际读取的字节大小
                    readLen = br.Read(data, 0, data.Length);
                    bw.Write(data, 0, readLen);
                    //关闭输入流
                    FileIn.Close();
                };
                //关闭二进制写入
                bw.Close();
                FileOut.Close();

                //最后删除临时存储目录
                System.IO.Directory.Delete(tempUrl + newFileName, true);

            }
            catch
            {
                if (File.Exists(zipUrl + cid + ".zip"))
                {
                    File.Delete(zipUrl + cid + ".zip");
                }
                return false;
            }
            return true;
        }

        public static bool CheckCourseFile(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            if (dirs.Length <= 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    if (GetSectionInfo(dirs[i]).Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void ClearErrorFile(int cid, string newFileName, string tempPath, string zipPath, string unzipPath,string videoPath)
        {
            if (Directory.Exists(tempPath + newFileName))
            {
                Directory.Delete(tempPath + newFileName, true);
            }
            if (File.Exists(zipPath + cid + ".zip"))
            {
                File.Delete(zipPath + cid + ".zip");
            }
            if (Directory.Exists(unzipPath + Path.GetFileNameWithoutExtension(newFileName)))
            {
                Directory.Delete(unzipPath + Path.GetFileNameWithoutExtension(newFileName),true);
            }
            if (Directory.Exists(unzipPath + cid))
            {
                Directory.Delete(unzipPath + cid,true);
            }
            if (File.Exists(videoPath + cid + ".mp4"))
            {
                File.Delete(videoPath + cid + ".mp4");
            }
        }
    }
}