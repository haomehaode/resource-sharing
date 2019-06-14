using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public class ObjectTag : DataBase<ObjectTags>
    {
        public List<Questions> GetQuestionByTags_TopN(int qid, List<int> tagsid, int topN)
        {
            List<int> q_id = base.EF.ObjectTags.Where(o => o.Obj_Type == 33 && tagsid.Contains(o.Obj_TagID)).Select(o => o.Obj_ID).ToList();
            if (q_id.Contains(qid))
            {
                //q_id.Remove(qid);
                q_id.RemoveAll(q => q == qid);
            }
            return base.EF.Questions.Where(q => q_id.Contains(q.Q_ID)).OrderByDescending(q => q.Q_Hot).Skip(0).Take(topN).ToList();
        }
    }
}