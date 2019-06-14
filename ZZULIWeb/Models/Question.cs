using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public class Question : DataBase<Questions>
    {
        /// <summary>
        /// 根据问题ID获取其最受欢迎的回答
        /// </summary>
        /// <param name="q_id"></param>
        /// <returns></returns>
        public Answers GetHotestAnswer(int q_id)
        {
            return base.EF.Answers.Where(a => a.Q_ID == q_id && a.A_Target == -1).OrderByDescending(a => a.A_Likes).FirstOrDefault();
        }

        public CoursesAnswers GetHotesCourseAnswer(int cq_id)
        {
            return base.EF.CoursesAnswers.Where(c => c.CQ_ID == cq_id && c.CA_Target == -1).OrderByDescending(c => c.CA_Likes).FirstOrDefault();
        }

        /// <summary>
        /// 获取推荐分类
        /// </summary>
        /// <param name="topN"></param>
        /// <returns></returns>
        public List<Navigations> GetRecommendFollow(int topN = 5, int u_id = -1)
        {
            if (u_id == -1)
            {
                return base.EF.Navigations.Where(n => n.Ower_ID != -1).OrderByDescending(n => n.Nav_FollowNum).Skip(0).Take(topN).ToList();
            }
            else
            {
                var list = base.EF.UserFollows.Where(u => u.User_ID == u_id).Select(u => u.Nav_ID).ToList();
                return base.EF.Navigations.Where(n => n.Ower_ID != -1 && !list.Contains(n.Nav_ID)).OrderByDescending(n => n.Nav_FollowNum).Skip(0).Take(topN).ToList();
            }
        }


        /// <summary>
        /// 根据用户ID获取用户的关注分类
        /// </summary>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public List<UserFollows> GetFollowByUID(int u_id)
        {
            return base.EF.UserFollows.Where(u => u.User_ID == u_id).ToList();
        }

        /// <summary>
        /// 根据问题的ID获取问题的回答量
        /// </summary>
        /// <param name="q_id"></param>
        /// <returns></returns>
        public int GetAnswerNum(int q_id)
        {
            return base.EF.Answers.Where(a => a.Q_ID == q_id && a.A_Target == -1).Count();
        }

        public int GetAnswerNumOfAnswer(int aid)
        {
            return base.EF.Answers.Where(a => a.A_Target == aid).Count();
        }
        /// <summary>
        /// 根据课程问题的ID获取课程问题的回复量
        /// </summary>
        /// <param name="cq_id"></param>
        /// <returns></returns>
        public int GetCourseAnswerNum(int cq_id)
        {
            return base.EF.CoursesAnswers.Where(c => c.CQ_ID == cq_id && c.CA_Target == -1).Count();
        }

        /// <summary>
        /// 根据问题ID和回复ID，获取回复的回复数量
        /// </summary>
        /// <param name="q_id"></param>
        /// <param name="targer_id"></param>
        /// <returns></returns>
        public int GetAnswerOfAnswerNum(int q_id, int targer_id)
        {
            return base.EF.Answers.Where(a => a.Q_ID == q_id && a.A_Target == targer_id).Count();
        }

        /// <summary>
        /// 获取问题回复 的回复
        /// </summary>
        /// <param name="qid"></param>
        /// <param name="target_id"></param>
        /// <returns></returns>
        public List<Answers> GetAnswerOfAnswer(int qid, int target_id)
        {
            return base.EF.Answers.Where(a => a.Q_ID == qid && a.A_Target == target_id).ToList();
        }

        public List<Answers> GetAnswer(int q_id)
        {
            return base.EF.Answers.Where(a => a.Q_ID == q_id).OrderByDescending(a => a.A_Time).ToList();
        }

        public List<Answers> GetAnswer_Page(int q_id, int pageIndex = 1, int pageSize = 10, int topN = 5)
        {
            List<Answers> list = new List<Answers>();
            if (pageIndex == 1)//如果是第一页，先获取点赞topN
            {
                List<int> topid = new List<int>();
                List<Answers> top_list = base.EF.Answers.Where(a => a.Q_ID == q_id).OrderByDescending(a => a.A_Likes).Skip(0).Take(topN).ToList();
                top_list.ForEach(t => { list.Add(t); topid.Add(t.A_ID); });
                List<Answers> then_list = base.EF.Answers.Where(a => a.Q_ID == q_id && !topid.Contains(a.A_ID)).OrderByDescending(a => a.A_Time).Skip(0).Take(pageSize - topN).ToList();
                then_list.ForEach(t => list.Add(t));
            }
            else//如果不是第二页
            {
                List<Answers> var_list = base.EF.Answers.Where(a => a.Q_ID == q_id).OrderByDescending(a => a.A_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var_list.ForEach(v => list.Add(v));
            }
            //return base.EF.Answers.Where(a => a.Q_ID == q_id).OrderByDescending(a => a.A_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }

        public int GetQuestionCollectNum(int qid)
        {
            return base.EF.UserCollections.Where(u => u.UC_Type == 9 && u.UC_Target == qid).Count();
        }
    }
}