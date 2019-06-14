using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public class Search : DataBase<UserSearchRecord>
    {
        public List<string> GetHostestSearch()
        {
            List<string> groupList = base.EF.UserSearchRecord.GroupBy(s => s.USR_Content).OrderByDescending(s => s.Count()).Select(s => s.Key).Skip(0).Take(10).ToList();
            return groupList;
        }

        public List<Courses> GetSearchResult_Course(string str, int pageIndex = 1, int pageSize = 10)
        {
            //当pageindex为0时，表示全站搜索
            if (pageIndex == 0)
                return base.EF.Courses.Where(c => c.Cou_Name.Contains(str) || c.Cou_Describe.Contains(str)).ToList();
            else
                return base.EF.Courses.Where(c => c.Cou_Name.Contains(str) || c.Cou_Describe.Contains(str)).OrderByDescending(c => c.Cou_Hot).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Questions> GetSearchResult_Question(string str, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex == 0)
                return base.EF.Questions.Where(q => q.Q_Title.Contains(str) || q.Q_Content.Contains(str)).ToList();
            else
                return base.EF.Questions.Where(q => q.Q_Title.Contains(str) || q.Q_Content.Contains(str)).OrderByDescending(q => q.Q_Hot).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }


        public List<Notes> GetSearchResult_Note(string str, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex == 0)
                return base.EF.Notes.Where(n => n.N_Title.Contains(str) || n.N_Content.Contains(str)).ToList();
            else
                return base.EF.Notes.Where(n => n.N_Title.Contains(str) || n.N_Content.Contains(str)).OrderByDescending(n => n.N_Hot).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}