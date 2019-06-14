using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public enum EnumActionType
    {
        None = 0,

        #region 浏览
        /// <summary>
        /// 浏览课程
        /// </summary>
        Browse_Course = 1,
        /// <summary>
        /// 浏览问题
        /// </summary>
        Browse_Question,
        /// <summary>
        /// 浏览笔记
        /// </summary>
        Browse_Note,
        #endregion

        #region 赞
        /// <summary>
        /// 点赞  课程
        /// </summary>
        Like_Course,
        /// <summary>
        /// 点赞  课程评论
        /// </summary>
        Like_CourseComment,
        /// <summary>
        /// 点赞  课程问题回复
        /// </summary>
        Like_CourseAnswer,
        /// <summary>
        /// 点赞  课程笔记
        /// </summary>
        Like_CourseNote,
        /// <summary>
        /// 点赞  问题回复
        /// </summary>
        Like_Answer,
        #endregion

        #region 踩
        /// <summary>
        /// 踩   课程
        /// </summary>
        NotLike_Course,
        /// <summary>
        /// 踩   课程评论
        /// </summary>
        NotLike_CourseComment,
        /// <summary>
        /// 踩   课程问题回复
        /// </summary>
        NotLike_CourseAnswer,
        /// <summary>
        /// 踩   课程笔记
        /// </summary>
        NotLike_CourseNote,
        /// <summary>
        /// 踩   问题回复
        /// </summary>
        NotLike_Answer,
        #endregion

        #region 收藏
        /// <summary>
        /// 收藏  课程
        /// </summary>
        Collect_Course,
        /// <summary>
        /// 收藏  课程问题
        /// </summary>
        Collect_CourseQuestion,
        /// <summary>
        /// 收藏  课程笔记
        /// </summary>
        Collect_CourseNote,
        /// <summary>
        /// 收藏  问题
        /// </summary>
        Collect_Question,
        /// <summary>
        /// 收藏  笔记
        /// </summary>
        Collect_Note
        #endregion
    }
}