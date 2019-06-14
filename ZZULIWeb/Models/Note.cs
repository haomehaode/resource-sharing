using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ZZULIWeb.Models;

namespace ZZULIWeb.Models
{
    public class Note : DataBase<Notes>
    {

        //public List<Notes> GetNotesTopN<TKey>(Expression<Func<Notes, bool>> selectLambda, Expression<Func<Notes, TKey>> orderByLambda, int topN, bool isDesc = true)
        //{
        //    if (isDesc)
        //        return base.EF.Notes.Where(selectLambda).OrderByDescending(orderByLambda).Skip(0).Take(topN).ToList();
        //    else
        //        return base.EF.Notes.Where(selectLambda).OrderBy(orderByLambda).Skip(0).Take(topN).ToList();
        //}

        /// <summary>
        /// 根据笔记的标签ID获取topN
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="nav0_id"></param>
        /// <param name="selectLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="topN"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public List<Notes> GetNotesTopNByTags<TKey>(int nav0_id, Func<Notes, TKey> orderByLambda, int topN, bool isDesc = true)
        {
            //// 1、获取笔记所属导航下所有的标签的Nav_ID
            List<int> tagsid = Common.GetAllTags(nav0_id);

            ////3、从ObjectTags中遍历获取标记有这些标签的笔记
            List<int> note_id = base.EF.ObjectTags.Where(o => tagsid.Contains(o.Obj_TagID) && o.Obj_Type == 34).Select(o => o.Obj_ID).ToList();

            //4、从Notes表中获取数据并排序
            //var tagsid = base.EF.Navigations.Where(n => n.Ower_ID == nav0_id).Select(n => n.Nav_ID);
            if (isDesc)
                return base.EF.Notes.Where(n => note_id.Contains(n.N_ID)).OrderByDescending(orderByLambda).Skip(0).Take(topN).ToList();
            else
                return base.EF.Notes.Where(n => note_id.Contains(n.N_ID)).OrderBy(orderByLambda).Skip(0).Take(topN).ToList();
        }

        public int GetNoteCommentNum(int note_id)
        {
            return base.EF.NotesComments.Where(n => n.N_ID == note_id).Count();
        }

        public List<NotesComments> GetNoteComment(int note_id)
        {
            return base.EF.NotesComments.Where(n => n.N_ID == note_id).ToList();
        }

        public List<NotesComments> GetNoteComment_Page(int note_id, int pageIndex = 1, int pageSize = 10, int topN = 5)
        {
            List<NotesComments> list = new List<NotesComments>();
            if (pageIndex == 1)//如果是第一页，先获取点赞topN
            {
                List<int> topid = new List<int>();
                List<NotesComments> top_list = base.EF.NotesComments.Where(n => n.N_ID == note_id).OrderByDescending(n => n.NC_Likes).Skip(0).Take(topN).ToList();
                top_list.ForEach(t => { list.Add(t); topid.Add(t.NC_ID); });
                List<NotesComments> then_list = base.EF.NotesComments.Where(n => n.N_ID == note_id && !topid.Contains(n.NC_ID)).OrderByDescending(t => t.NC_Time).Skip(0).Take(pageSize - topN).ToList();
                then_list.ForEach(t => list.Add(t));
            }
            else//如果不是第二页
            {
                List<NotesComments> var_list = base.EF.NotesComments.Where(n => n.N_ID == note_id).OrderByDescending(n => n.NC_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var_list.ForEach(v => list.Add(v));
            }
            //return base.EF.Courses.Where(c => c.Cou_ID == id).FirstOrDefault().CoursesNotes.ToList();
            return list;
        }

        /// <summary>
        /// 根据笔记的ID获取笔记的标签
        /// </summary>
        /// <param name="note_id"></param>
        /// <returns></returns>
        public List<Navigations> GetNoteTags(int note_id)
        {
            List<int> tagsid = base.EF.ObjectTags.Where(o => o.Obj_Type == 34 && o.Obj_ID == note_id).Select(o => o.Obj_TagID).ToList();
            return base.EF.Navigations.Where(n => tagsid.Contains(n.Nav_ID)).ToList();

        }

        public int GetNoteCollectionNum(int nid)
        {
            return base.EF.UserCollections.Where(u => u.UC_Type == 10 && u.UC_Target == nid).Count();
        }

    }
}