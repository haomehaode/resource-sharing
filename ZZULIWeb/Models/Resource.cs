using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public class Resource : DataBase<ResourceInfo>
    {

        public List<ResourceInfo> GetResourceByTag(int tagid)
        {
            List<int> tagids = Common.GetAllTags(tagid);
            var ids = base.EF.ObjectTags.Where(o => o.Obj_Type == 39 && tagids.Contains(o.Obj_TagID)).Select(o => o.Obj_ID);
            return base.EF.ResourceInfo.Where(r => ids.Contains(r.R_ID)).ToList();
        }

        public List<ResourceInfo> GetResource_Page(int nav0, int nav1, int pageIndex = 1, int pageSize = 10)
        {
            List<ResourceInfo> res = new List<ResourceInfo>();
            if (nav0 == -1 && nav1 == -1)
            {
                res = base.GetList_Page(r => r.R_ID > 0, r => r.R_Hot, pageIndex, pageSize);
            }
            else if (nav0 != -1 && nav1 == -1)
            {
                List<int> alltag = Common.GetAllTags(nav0);
                var tagids = base.EF.ObjectTags.Where(o => o.Obj_Type == 39 && alltag.Contains(o.Obj_TagID)).Select(o => o.Obj_ID);
                res = base.GetList_Page(r => tagids.Contains(r.R_ID), r => r.R_Hot, pageIndex, pageSize);
            }
            else
            {
                var tagids = base.EF.ObjectTags.Where(o => o.Obj_Type == 39 && o.Obj_TagID == nav1).Select(o => o.Obj_ID);
                res = base.GetList_Page(r => tagids.Contains(r.R_ID), r => r.R_Hot, pageIndex, pageSize);
            }
            return res;
        }

        public List<Navigations> GetResourceTags(int rid)
        {
            var ids = base.EF.ObjectTags.Where(o => o.Obj_Type == 39 && o.Obj_ID == rid).Select(o => o.Obj_TagID);
            return base.EF.Navigations.Where(n => ids.Contains(n.Nav_ID)).ToList();
        }

        public int GetResourceCommentNum(int rid)
        {
            return base.EF.ResourceComment.Where(r => r.RC_Target == rid).Count();
        }

    }
}