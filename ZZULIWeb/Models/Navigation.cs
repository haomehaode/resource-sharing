using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;


namespace ZZULIWeb.Models
{
    public class Navigation : DataBase<Navigation>
    {
        public List<Partition> GetPartition()
        {
            return base.EF.Partition.Where(p => p.Part_ID > 0).ToList();
        }

        public List<Navigations> GetNavigation0()
        {
            return base.EF.Navigations.Where(n => n.Ower_ID == -1).ToList();
        }

        public List<Navigations> GetNavigation1(int nav0)
        {
            //List<int> list = new List<int>();
            //GetNavigation0().ForEach(n => list.Add(n.Nav_ID));
            if (nav0 == 0)
                return base.EF.Navigations.Where(n => n.Nav_Type == 1 && n.Ower_ID != -1).ToList();
            else
                return base.EF.Navigations.Where(n => n.Nav_Type == 1 && n.Ower_ID == nav0).ToList();
        }

        public int GetRootTagID(int tag_id)
        {
            int current_id = tag_id;
            int father_id = base.EF.Navigations.Where(n => n.Nav_ID == tag_id).FirstOrDefault().Ower_ID;
            while (father_id != -1)
            {
                var data = base.EF.Navigations.Where(n => n.Nav_ID == father_id).FirstOrDefault();
                current_id = data.Nav_ID;
                father_id = data.Ower_ID;
            }
            return current_id;
        }


        public List<Navigations> GetAllTags()
        {
            return base.EF.Navigations.Where(n => n.Nav_ID > 0 && n.Nav_Type < 3).ToList();
        }
    }
}