using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public enum EnumOptionType
    {
        #region 浏览

        PageView_Course = 1,
        PageView_Question = 2,
        PageView_Note = 3,

        #endregion

        #region 搜索

        Search_Course = 4,
        Search_Question = 5,
        Search_Note = 6,
        Search_Resource = 7,
        
        #endregion

        #region 收藏

        Collect_Course = 8,
        Collect_Question = 9,
        Collect_Note = 10,

        #endregion

        #region 推荐

        Recommend_Course = 11,
        Recommend_CourseComment = 12,
        Recommend_CourseAnswer = 13,
        Recommend_CourseNote = 14,
        Recommend_Answer = 15,
        Recommend_Note = 16,
        Recommend_NoteComment = 17,

        #endregion

        #region 不推荐

        NotRecommend_Course = 18,
        NotRecommend_Answer = 19,
        NotRecommend_Note = 20

        #endregion
    }
}