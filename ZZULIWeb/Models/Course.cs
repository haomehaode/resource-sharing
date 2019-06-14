using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ZZULIWeb.Models
{
    public class Course : DataBase<Courses>
    {
        #region 、、、、、
        ///// <summary>
        ///// 课程ID
        ///// </summary>
        //public int Cou_ID { get; set; }
        ///// <summary>
        ///// 课程名称
        ///// </summary>
        //public string Cou_Name { get; set; }
        ///// <summary>
        ///// 课程类型
        ///// </summary>
        //public string Cou_Type { get; set; }
        ///// <summary>
        ///// 课程简介
        ///// </summary>
        //public string Cou_Describe { get; set; }
        ///// <summary>
        ///// 课程标签
        ///// </summary>
        //public string Cou_Tags { get; set; }
        ///// <summary>
        ///// 课程热度
        ///// </summary>
        //public decimal Cou_Hot { get; set; }
        ///// <summary>
        ///// 课程目录
        ///// </summary>
        //public string Cou_Catalog { get; set; }
        ///// <summary>
        ///// 课程点赞量
        ///// </summary>
        //public int Cou_Likes { get; set; }
        ///// <summary>
        ///// 课程不推荐量
        ///// </summary>
        //public int Cou_NotLikes { get; set; }
        ///// <summary>
        ///// 课程学习量
        ///// </summary>
        //public int Cou_StudyNum { get; set; }
        ///// <summary>
        ///// 课程上传时间
        ///// </summary>
        //public DateTime Cou_Time { get; set; }

        ///// <summary>
        ///// 课程上传者ID
        ///// </summary>
        //public int User_ID { get; set; }
        ///// <summary>
        ///// 课程上传者名字
        ///// </summary>
        //public string User_Name { get; set; }

        #endregion

        public int Add(Courses cou)
        {
            base.EF.Courses.Add(cou);
            if (base.EF.SaveChanges() > 0)
            {
                return cou.Cou_ID;
            }
            else
            {
                return -1;
            }
        }

        //根据ID获取课程详情
        /// <summary>
        /// 根据ID获取课程详情
        /// </summary>
        /// <param name="id">课程ID</param>
        /// <returns></returns>
        public Courses GetCourseDetail(int id)
        {
            return base.EF.Courses.Where(c => c.Cou_ID == id).FirstOrDefault();
        }

        public List<CoursesNotes> GetCourseNote(int cou_id)
        {
            return base.EF.CoursesNotes.Where(c => c.Cou_ID == cou_id).ToList();
        }

        //根据课程ID获取课程笔记列表信息
        /// <summary>
        /// 根据课程ID获取课程笔记列表信息
        /// </summary>
        /// <param name="couid">课程ID</param>
        /// <returns></returns>
        public List<CoursesNotes> GetCourseNote_Page(int couid, int pageIndex, int pageSize, int topN)
        {
            List<CoursesNotes> list = new List<CoursesNotes>();
            if (pageIndex == 1)//如果是第一页，先获取点赞topN
            {
                List<int> topid = new List<int>();
                List<CoursesNotes> top_list = base.EF.CoursesNotes.Where(c => c.Cou_ID == couid).OrderByDescending(c => c.CN_Likes).Skip(0).Take(topN).ToList();
                top_list.ForEach(t => { list.Add(t); topid.Add(t.CN_ID); });
                List<CoursesNotes> then_list = base.EF.CoursesNotes.Where(c => c.Cou_ID == couid && !topid.Contains(c.CN_ID)).OrderByDescending(t => t.CN_Time).Skip(0).Take(pageSize - topN).ToList();
                then_list.ForEach(t => list.Add(t));
            }
            else//如果不是第二页
            {
                List<CoursesNotes> var_list = base.EF.CoursesNotes.Where(c => c.Cou_ID == couid).OrderByDescending(c => c.CN_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var_list.ForEach(v => list.Add(v));
            }
            //return base.EF.Courses.Where(c => c.Cou_ID == id).FirstOrDefault().CoursesNotes.ToList();
            return list;
        }

        public List<CoursesQuestions> GetCourseQuestion(int couid)
        {
            return base.EF.CoursesQuestions.Where(c => c.Cou_ID == couid).ToList();
        }


        public List<CoursesQuestions> GetCourseQuestion_Page(int couid, int pageIndex, int pageSize, int topN)
        {
            List<CoursesQuestions> list = new List<CoursesQuestions>();
            if (pageIndex == 1)//如果是第一页，先获取点赞topN
            {
                List<int> topid = new List<int>();
                List<CoursesQuestions> top_list = base.EF.CoursesQuestions.Where(c => c.Cou_ID == couid).OrderByDescending(c => c.CQ_Hot).Skip(0).Take(topN).ToList();
                top_list.ForEach(t => { list.Add(t); topid.Add(t.CQ_ID); });
                List<CoursesQuestions> then_list = base.EF.CoursesQuestions.Where(c => c.Cou_ID == couid && !topid.Contains(c.CQ_ID)).OrderByDescending(t => t.CQ_Time).Skip(0).Take(pageSize - topN).ToList();
                then_list.ForEach(t => list.Add(t));
            }
            else//如果不是第二页
            {
                List<CoursesQuestions> var_list = base.EF.CoursesQuestions.Where(c => c.Cou_ID == couid).OrderByDescending(c => c.CQ_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var_list.ForEach(v => list.Add(v));
            }
            //return base.EF.Courses.Where(c => c.Cou_ID == id).FirstOrDefault().CoursesNotes.ToList();
            return list;
        }

        public List<CoursesComment> GetCourseComment(int couid)
        {
            return base.EF.CoursesComment.Where(c => c.Cou_ID == couid).ToList();
        }

        //获取课程评论
        /// <summary>
        /// 获取课程评论
        /// </summary>
        /// <param name="id">课程ID</param>
        /// <param name="topN">topN</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        public List<CoursesComment> GetCourseComment_Page(int couid, int pageIndex, int pageSize, int topN)
        {
            List<CoursesComment> list = new List<CoursesComment>();

            //CoursesComment c;
            ZZULIEntities db = base.EF;
            //1、如果是第一页，先获取赞topN
            if (pageIndex == 1)
            {
                //List<CoursesComment> top_list = base.EF.CoursesComment.Include("Courses").Where(cc => cc.Cou_ID == cou_id).OrderByDescending(cc => cc.CCom_Likes).Skip(0).Take(topN).ToList();
                List<CoursesComment> top_list = base.EF.CoursesComment.Where(cc => cc.Cou_ID == couid).OrderByDescending(cc => cc.CCom_Likes).Skip(0).Take(topN).ToList();

                List<int> top_id = new List<int>();

                top_list.ForEach(t => { list.Add(t); top_id.Add(t.CCom_ID); });

                //foreach (CoursesComment v in top_list)
                //{
                //    list.Add(v);
                //    top_id.Add(v.CCom_ID);
                //}
                //2、如果是第一页再获取pagesize-topN条,且不包含topN中的数据，并按照时间先后顺序排列
                //List<CoursesComment> then_list = base.EF.CoursesComment.Include("Courses").Where(cc=>cc.Cou_ID == cou_id && (cou_id !top_id.Contains(top_id))).OrderByDescending(cc=>cc.CCom_Time).Skip(0).Take(pageSize-topN);
                //List<CoursesComment> then_list = (from cc in db.CoursesComment join u in db.UserInfo on cc.User_ID equals u.User_ID where cc.Cou_ID == cou_id && !top_id.Contains(cc.CCom_ID) orderby cc.CCom_Time descending select cc).Skip(0).Take(pageSize - topN).ToList();

                List<CoursesComment> then_list = base.EF.CoursesComment.Where(cc => cc.Cou_ID == couid && !top_id.Contains(cc.Cou_ID)).OrderByDescending(cc => cc.CCom_Time).Skip(0).Take(pageSize - topN).ToList();

                //List<CoursesComment> then_list = from cc in 
                then_list.ForEach(t => list.Add(t));
                //foreach (CoursesComment v in var_list)
                //{
                //    //c = new CoursesComment() { CCom_ID = v.CCom_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CCom_Content = v.CCom_Content, CCom_Time = v.CCom_Time, CCom_Likes = v.CCom_Likes, User_Name = v.User_Name };
                //    list.Add(v);
                //}
            }
            else//否则获取pagesize条数据，并按照时间先后顺序排列
            {
                //List<CoursesComment> var_list = (from cc in db.CoursesComment join u in db.UserInfo on cc.User_ID equals u.User_ID where cc.Cou_ID == cou_id orderby cc.CCom_Time descending select cc).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                List<CoursesComment> var_list = base.EF.CoursesComment.Where(cc => cc.Cou_ID == couid).OrderByDescending(cc => cc.CCom_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                //foreach (CoursesComment v in var_list)
                //{
                //    //c = new CourseComment() { CCom_ID = v.CCom_ID, Cou_ID = v.Cou_ID, User_ID = v.User_ID, CCom_Content = v.CCom_Content, CCom_Time = v.CCom_Time, CCom_Likes = v.CCom_Likes, User_Name = v.User_Name };
                //    list.Add(v);
                //}
                var_list.ForEach(v => list.Add(v));
            }
            return list;
        }

        //根据课程ID获取该课程问题和回答
        /// <summary>
        /// 根据课程ID获取该课程问题和回答
        /// </summary>
        /// <param name="couid">课程ID</param>
        /// <param name="dic">字典表，调用方法时该参数不用传入该参数</param>
        /// <returns></returns>
        public Dictionary<int, CoursesAnswers> GetCourseQuestionAndAnswer(int couid, Dictionary<int, CoursesAnswers> dic = null)
        {
            //字典表，用于存放问题对象和回复对象
            if (dic == null)
                dic = new Dictionary<int, CoursesAnswers>();

            List<CoursesQuestions> list = base.EF.CoursesQuestions.Where(c => c.Cou_ID == couid).OrderBy(c => c.CQ_Time).ToList();

            list.ForEach(c =>
            {
                //遍历问题  查看该问题是否有回复 
                List<CoursesAnswers> list_Answers = base.EF.CoursesAnswers.Where(ca => ca.CA_Target == -1).OrderBy(ca => ca.CA_Time).ToList();
                if (list_Answers.Count() > 0)//如果有回复  加入字典表 然后 将回复当作一个问题 查看该回复是否有回复
                {
                    list_Answers.ForEach(a => dic.Add(couid, a));
                    GetCourseQuestionAndAnswer(couid, dic);
                }
            });
            return dic;
        }

        /// 通过节ID获取该章节下的所有问题
        /// <summary>
        /// 通过节ID获取该章节下的所有问题
        /// </summary>
        /// <param name="movid"></param>
        /// <returns></returns>
        public List<CoursesQuestions> GetMoveQuestion(int movid)
        {
            return base.EF.CoursesQuestions.Where(c => c.Mov_ID == movid).ToList();
        }

        public List<CoursesQuestions> GetMoveQuestion_Page(int movid, int pageIndex, int pageSize, int topN)
        {
            List<CoursesQuestions> list = new List<CoursesQuestions>();

            #region 先获取热度topN,然后根据时间排序

            //if (pageIndex == 1)//如果是第一页，先获取点赞topN
            //{
            //    List<int> topid = new List<int>();
            //    List<CoursesQuestions> top_list = base.EF.CoursesQuestions.Where(c => c.Mov_ID == movid).OrderByDescending(c => c.CQ_Hot).Skip(0).Take(topN).ToList();
            //    top_list.ForEach(t => { list.Add(t); topid.Add(t.CQ_ID); });
            //    List<CoursesQuestions> then_list = base.EF.CoursesQuestions.Where(c => c.Mov_ID == movid && !topid.Contains(c.CQ_ID)).OrderByDescending(t => t.CQ_Time).Skip(0).Take(pageSize - topN).ToList();
            //    then_list.ForEach(t => list.Add(t));
            //}
            //else//如果不是第二页
            //{
            //    List<CoursesQuestions> var_list = base.EF.CoursesQuestions.Where(c => c.Mov_ID == movid).OrderByDescending(c => c.CQ_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //    var_list.ForEach(v => list.Add(v));
            //}

            #endregion
            list = base.EF.CoursesQuestions.Where(c => c.Mov_ID == movid).OrderByDescending(c => c.CQ_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            //return base.EF.Courses.Where(c => c.Cou_ID == id).FirstOrDefault().CoursesNotes.ToList();
            return list;
        }

        public List<CoursesComment> GetMoveCommment(int movid)
        {
            return base.EF.CoursesComment.Where(c => c.Mov_ID == movid).ToList();
        }

        public List<CoursesComment> GetMoveCommment_Page(int movid, int pageIndex, int pageSize, int topN)
        {
            List<CoursesComment> list = new List<CoursesComment>();

            ZZULIEntities db = base.EF;
            #region 先获取热度topN，然后再根据时间排序

            //if (pageIndex == 1)
            //{
            //    List<CoursesComment> top_list = base.EF.CoursesComment.Where(cc => cc.Mov_ID == movid).OrderByDescending(cc => cc.CCom_Likes).Skip(0).Take(topN).ToList();

            //    List<int> top_id = new List<int>();

            //    top_list.ForEach(t => { list.Add(t); top_id.Add(t.CCom_ID); });


            //    List<CoursesComment> then_list = base.EF.CoursesComment.Where(cc => cc.Mov_ID == movid && !top_id.Contains(cc.Cou_ID)).OrderByDescending(cc => cc.CCom_Time).Skip(0).Take(pageSize - topN).ToList();

            //    then_list.ForEach(t => list.Add(t));
            //}
            //else//否则获取pagesize条数据，并按照时间先后顺序排列
            //{
            //    List<CoursesComment> var_list = base.EF.CoursesComment.Where(cc => cc.Mov_ID == movid).OrderByDescending(cc => cc.CCom_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //    var_list.ForEach(v => list.Add(v));
            //}

            #endregion

            list = db.CoursesComment.Where(c => c.Mov_ID == movid).OrderByDescending(c => c.CCom_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }

        public List<CoursesNotes> GetMoveNote(int movid)
        {
            return base.EF.CoursesNotes.Where(c => c.Mov_ID == movid).ToList();
        }
        public List<CoursesNotes> GetMoveNote_Page(int movid, int pageIndex, int pageSize, int topN)
        {
            List<CoursesNotes> list = new List<CoursesNotes>();

            #region 先获取热度topN，然后根据时间排序

            //if (pageIndex == 1)//如果是第一页，先获取点赞topN
            //{
            //    List<int> topid = new List<int>();
            //    List<CoursesNotes> top_list = base.EF.CoursesNotes.Where(c => c.Mov_ID == movid).OrderByDescending(c => c.CN_Likes).Skip(0).Take(topN).ToList();
            //    top_list.ForEach(t => { list.Add(t); topid.Add(t.CN_ID); });
            //    List<CoursesNotes> then_list = base.EF.CoursesNotes.Where(c => c.Mov_ID == movid && !topid.Contains(c.CN_ID)).OrderByDescending(t => t.CN_Time).Skip(0).Take(pageSize - topN).ToList();
            //    then_list.ForEach(t => list.Add(t));
            //}
            //else//如果不是第二页
            //{
            //    List<CoursesNotes> var_list = base.EF.CoursesNotes.Where(c => c.Mov_ID == movid).OrderByDescending(c => c.CN_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //    var_list.ForEach(v => list.Add(v));
            //}

            #endregion

            list = base.EF.CoursesNotes.Where(c => c.Mov_ID == movid).OrderByDescending(c => c.CN_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            //return base.EF.Courses.Where(c => c.Cou_ID == id).FirstOrDefault().CoursesNotes.ToList();
            return list;
        }


        public List<CoursesAnswers> GetCourseAnswer(List<CoursesQuestions> list)
        {
            List<int> ids = new List<int>();
            list.ForEach(l => ids.Add(l.CQ_ID));
            //var v = from a in base.EF.CoursesAnswers where ids.Contains(a.CQ_ID) select a;
            return base.EF.CoursesAnswers.Where(c => ids.Contains(c.CQ_ID)).ToList();
        }


        //根据课程ID获取推荐课程
        /// <summary>
        /// 根据课程ID获取推荐课程
        /// </summary>
        /// <param name="id">课程ID</param>
        /// <returns></returns>
        public List<Courses> GetRecommendCourseByCID(int id, int topN = 5)
        {
            var tagsid = Common.GetAllTags(base.EF.ObjectTags.Where(o => o.Obj_ID == id && o.Obj_Type == 32).Select(o => o.Obj_TagID).ToList());
            //int owerid = base.EF.Courses.Where(c => c.Cou_ID == id).FirstOrDefault().Navigations.Ower_ID;
            //return base.EF.Courses.Where(c => c.Navigations.Ower_ID == owerid).OrderByDescending(c => c.Cou_Hot).Skip(0).Take(topN).ToList();
            var cou_id = base.EF.ObjectTags.Where(o => tagsid.Contains(o.Obj_TagID) && o.Obj_Type == 32).Select(o => o.Obj_ID);
            return base.EF.Courses.Where(c => cou_id.Contains(c.Cou_ID)).OrderByDescending(c => c.Cou_Hot).Skip(0).Take(topN).ToList();
        }

        //根据Nav0获取推荐课程
        /// <summary>
        /// 根据Nav0获取推荐课程
        /// </summary>
        /// <param name="id">Nav0</param>
        /// <returns></returns>
        public List<Courses> GetRecommendCourseByNav0(int nav0, int topN = 5)
        {
            var tagsid = Common.GetAllTags(nav0);
            var cou_id = base.EF.ObjectTags.Where(o => tagsid.Contains(o.Obj_TagID) && o.Obj_Type == 32).Select(o => o.Obj_ID);
            //return base.EF.Courses.Where(c => c.Navigations.Ower_ID == nav0).OrderByDescending(c => c.Cou_Hot).Skip(0).Take(topN).ToList();
            return base.EF.Courses.Where(c => cou_id.Contains(c.Cou_ID)).OrderByDescending(c => c.Cou_Hot).Skip(0).Take(topN).ToList();
        }


        //获取课程评论的总页数
        /// <summary>
        /// 获取课程评论的总页数
        /// </summary>
        /// <param name="cou_id">课程ID</param>
        /// <param name="pagesize">页容量</param>
        /// <returns></returns>
        public int GetCourseCommentPageNum(int cou_id, int pagesize = 10)
        {
            int count = base.EF.CoursesComment.Include("Courses").Where(c => c.Cou_ID == cou_id).Count();
            //var var_top = from cc in db.CoursesComment join u in db.UserInfo on cc.User_ID equals u.User_ID where cc.Cou_ID == cou_id select cc;
            return Convert.ToInt32(Math.Ceiling(count / Convert.ToDouble(pagesize)));
        }

        /// <summary>
        /// 获取用户在课程下的笔记数量
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="cou_id"></param>
        /// <returns></returns>
        public int GetCourseNoteByUID(int u_id, int cou_id)
        {
            return base.EF.CoursesNotes.Where(c => c.User_ID == u_id && c.Cou_ID == cou_id).Count();
        }

        /// <summary>
        /// 获取用户在课程下的问题数量
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="cou_id"></param>
        /// <returns></returns>
        public int GetCourseQuestionByUID(int u_id, int cou_id)
        {
            return base.EF.CoursesQuestions.Where(c => c.User_ID == u_id && c.Cou_ID == cou_id).Count();
        }

        //根据条件获取topN
        //
        //public List<Courses> GetCourseTopN<TR>(Expression<Func<Courses, bool>> whereLambda, Expression<Func<Courses, TR>> orderLambda, int topN)
        //{
        //    return base.EF.Courses.Where(whereLambda).OrderByDescending(orderLambda).Skip(0).Take(topN).ToList();
        //}

        public List<Courses> GetCourseByTags(int tag_id)
        {
            var tagsid = Common.GetAllTags(tag_id);

            var cou_id = base.EF.ObjectTags.Where(o => tagsid.Contains(o.Obj_TagID) && o.Obj_Type == 32).Select(o => o.Obj_ID);

            var res = base.EF.Courses.Where(c => cou_id.Contains(c.Cou_ID));
            if (res != null && res.Count() > 0)
            {
                return res.ToList();
            }
            else { return null; }
        }


        /// <summary>
        /// 根据课程标签给topN
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="tag_id"></param>
        /// <param name="orderLambda"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public List<Courses> GetCourseTopNByTags<TR>(int tag_id, Expression<Func<Courses, TR>> orderLambda, int topN)
        {
            var tagsid = Common.GetAllTags(tag_id);
            ////1、获取该类型下的所有标签
            //List<int> numList = base.EF.Tags.Where(t => t.Tag_Type == tag_id).Select(t => t.Tag_ID).ToList();
            ////2、从课程表中获取所有贴有这些标签的课程,排序，获取topN
            //return base.EF.Courses.Where(whereLambda).Where(c => numList.Contains(c.Tags.Tag_ID)).OrderByDescending(orderLambda).Skip(0).Take(topN).ToList();
            //return base.EF.Courses.Where(c => c.Navigations.Ower_ID == owerid).OrderByDescending(orderLambda).Skip(0).Take(topN).ToList();
            //return base.EF.Courses.Where(c => tagsid.Contains(c.Cou_Tags)).OrderByDescending(orderLambda).Skip(0).Take(topN).ToList();
            var cou_id = base.EF.ObjectTags.Where(o => tagsid.Contains(o.Obj_TagID) && o.Obj_Type == 32).Select(o => o.Obj_ID);

            var res = base.EF.Courses.Where(c => cou_id.Contains(c.Cou_ID)).OrderByDescending(orderLambda).Skip(0).Take(topN);
            if (res != null && res.Count() > 0)
            {
                return res.ToList();
            }
            else { return null; }
        }

        public List<Courses> GetCourseByTags_Page<TR>(int tag_id, Expression<Func<Courses, TR>> orderLambda, int pageIndex = 1, int pageSize = 20)
        {
            var tagsid = Common.GetAllTags(tag_id);
            var cou_id = base.EF.ObjectTags.Where(o => tagsid.Contains(o.Obj_TagID) && o.Obj_Type == 32).Select(o => o.Obj_ID);
            var res = base.EF.Courses.Where(c => cou_id.Contains(c.Cou_ID)).OrderByDescending(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            if (res != null && res.Count() > 0)
            {
                return res.ToList();
            }
            else
            {
                return null;
            }
        }

        public string GetNav0NameByCourseID(int cou_id)
        {
            int id = GetNav0IDByCourseID(cou_id);
            return base.EF.Navigations.Where(n => n.Nav_ID == id).FirstOrDefault().Nav_Name;
            //return base.EF.ObjectTags.Where(o => o.Obj_ID == cou_id && o.Obj_Type == 32).FirstOrDefault().Navigations.Nav_Name;
            //var id = base.EF.Courses.Where(c => c.Cou_ID == cou_id).FirstOrDefault().Navigations.Ower_ID;
            //return base.EF.Navigations.Where(n => n.Nav_ID == id).FirstOrDefault().Nav_Name;
        }

        public int GetNav0IDByCourseID(int cou_id)
        {
            //先找到课程的一个标签就行了
            int id = base.EF.ObjectTags.Where(o => o.Obj_ID == cou_id && o.Obj_Type == 32).FirstOrDefault().Obj_TagID;

            return new Navigation().GetRootTagID(id);
        }

        /// <summary>
        /// 获取课程问起的回答量
        /// </summary>
        /// <param name="id">课程问题的ID</param>
        /// <returns></returns>
        public int GetCourseAnswerNum(int id)
        {
            return base.EF.CoursesAnswers.Where(c => c.CQ_ID == id).Count();
        }

        /// <summary>
        /// 用户开始学习的课程
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="movid"></param>
        /// <returns></returns>
        public bool UserStartLearn(int uid, int movid)
        {
            //获取节对应的课程ID
            int courseid = base.EF.Move.Where(m => m.Mov_ID == movid).Select(m => m.Cou_ID).FirstOrDefault();

            UserLearnCoursesRecord rec = base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == uid && u.Cou_ID == courseid).FirstOrDefault();

            //用户学习课程记录
            if (rec == null)//如果该用户第一次学习该课程，就在数据表中添加数据
            {
                base.EF.UserLearnCoursesRecord.Add(new UserLearnCoursesRecord() { Cou_ID = courseid, ULC_Time = DateTime.Now, User_ID = uid });
            }
            else//如果用户已经学习了该课程，并且不过是否已经学完，都要更改时间
            {
                rec.ULC_Time = DateTime.Now;
            }

            //用户学习章节记录
            UserLearnSectionRecord section = base.EF.UserLearnSectionRecord.Where(u => u.Mov_ID == movid && u.User_ID == uid).FirstOrDefault();
            if (section == null)//第一次学习该章节
            {
                base.EF.UserLearnSectionRecord.Add(new UserLearnSectionRecord() { User_ID = uid, Mov_ID = movid, Is_Finish = false, Cou_ID = courseid });
            }
            return base.EF.SaveChanges() > 0 ? true : false;
        }

        /// <summary>
        /// 用户学习完某门课程下的某个视频
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="couid"></param>
        /// <param name="movid"></param>
        /// <returns></returns>
        public bool UserLearnEnd(int uid, int movid)
        {
            int courseid = base.EF.Move.Where(m => m.Mov_ID == movid).FirstOrDefault().Cou_ID;

            UserLearnCoursesRecord course = base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == uid && u.Cou_ID == courseid).FirstOrDefault();


            UserLearnSectionRecord rec = base.EF.UserLearnSectionRecord.Where(u => u.User_ID == uid && u.Mov_ID == movid).FirstOrDefault();
            rec.Is_Finish = true;
            course.ULC_Time = DateTime.Now;
            //var list = base.EF.UserLearnCoursesRecord.Where(u => u.User_ID == uid && u.Cou_ID == rec.Cou_ID);
            //foreach (var item in list)
            //{
            //    item.ULC_Time = DateTime.Now;
            //}
            //if (rec == null)
            //{
            //    int courseid = base.EF.Move.Where(m => m.Mov_ID == movid).Select(m => m.Cou_ID).FirstOrDefault();
            //    base.EF.UserLearnCoursesRecord.Add(new UserLearnCoursesRecord() {Cou_ID = courseid, User_ID = uid, Move_ID = movid, Is_Finish = true, ULC_Time = DateTime.Now });
            //}
            //else
            //{
            //    rec.ULC_Time = DateTime.Now;
            //}
            return base.EF.SaveChanges() > 0 ? true : false;
        }

        // 根据课程ID获取课程的章信息
        /// <summary>
        /// 根据课程ID获取课程的章信息
        /// </summary>
        /// <param name="couid"></param>
        /// <returns></returns>
        public List<ZZULIWeb.Move> GetChapterByCID(int couid)
        {
            return base.EF.Move.Where(m => m.Cou_ID == couid && m.Is_Chapter == true).ToList();
        }

        // 根据课程ID和章ID获取节信息
        /// <summary>
        /// 根据课程ID和章ID获取节信息
        /// </summary>
        /// <param name="couid"></param>
        /// <param name="chapid"></param>
        /// <returns></returns>
        public List<ZZULIWeb.Move> GetSectionByCID(int couid, int chapid)
        {
            return base.EF.Move.Where(m => m.Cou_ID == couid && m.Is_Chapter == false && m.Chap_ID == chapid).ToList();
        }
    }
}