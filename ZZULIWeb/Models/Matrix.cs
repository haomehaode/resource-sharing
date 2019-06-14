using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZULIWeb;

namespace ZZULIWeb.Models
{
    class Matrix
    {
        public void Test()
        {
            ZZULIEntities ZZUL = new ZZULIEntities();
            #region 重置数据库
            using (ZZULIEntities ZZU = new ZZULIEntities())
            {
                var na = ZZU.Database.ExecuteSqlCommand("truncate table UserAry");
                var nl = ZZU.Database.ExecuteSqlCommand("truncate table NewAry");
                ZZU.SaveChanges();
            }
            #endregion
            var UID = ZZUL.PR_UserID().ToArray();//获取所有用户ID
            var AID = ZZUL.PR_AllID().ToArray(); //获取浏览、推荐、收藏用户ID
            var LID = ZZUL.PR_learnID().ToArray();//获取学习课程用户ID
            var CID = ZZUL.PR_CouID().ToArray();//获取所有课程所属标签ID
            #region 原始矩阵
            #region 学习
            //查找用户的学习记录，如果有 5分
            if (LID.Length > 0)
            {
                for (int i = 0; i < LID.Length; i++)//控制用户
                {
                    for (int j = 0; j < CID.Length; j++)//控制课程
                    {
                        var num = ZZUL.PR_Learn(LID[i], CID[j]).ToArray(); //判断用户是否学习过该课程
                        if (num[0] > 0)
                        {
                            ZZUL.PR_UserAry(LID[i], CID[j], 5);    //将用户ID，课程ID,分数增加到UserAry中
                            continue;
                        }
                    }

                }
                ZZUL.SaveChanges();
            }
        
            #endregion
            #region 收藏、推荐、浏览

            //先查找用户在改标签是否学习过，如果有结束，如果没在进行判断收藏4，，推荐3,浏览1
            int sum;
            //操作类型
            if (AID.Length != 0)  //判断是否有数据
            {
                for (int i = 0; i < AID.Length; i++)//控制用户
                {
                    for (int j = 0; j < CID.Length; j++)
                    {
                        sum = 0;
                        var num = ZZUL.PR_Learn(AID[i], CID[j]).ToArray();
                        if (num[0] > 0) //判断用户是否学习过该课程
                        {
                            continue;
                        }
                        else
                        {
                            var n = ZZUL.PR_ALL(AID[i], CID[j]).ToArray();//如果用户进行过收藏、浏览、推荐，返回推荐类型
                            if (n.Length > 0)//判断用户是否进行过推荐收藏、浏览、推荐 
                            {
                                for (int p = 0; p < n.Length; p++)
                                {
                                    sum += Convert.ToInt32(n[p]);
                                }
                                

                                switch (sum)
                                {
                                    case 20: //该用户对该课程都进行过浏览、推荐、收藏，取最高分收藏
                                    case 19: //该用户对该课程进行过推荐、收藏，取最高分收藏

                                    case 9: //该用户对该课程进行过浏览、收藏，取最高分收藏
                                    case 8: ZZUL.PR_UserAry(AID[i], CID[j], 4); break;//8代表收藏 4分
                                    case 12://该用户对该课程进行过浏览、推荐、取最高分推荐
                                    case 11: ZZUL.PR_UserAry(AID[i], CID[j], 3); break;  //推荐 3分
                                    case 1: ZZUL.PR_UserAry(AID[i], CID[j], 1); break;   //浏览一分

                                }
                            }
                            else
                            {
                                ZZUL.PR_UserAry(AID[i], CID[j], 0); //用户未对该课程进行过任何操作，0
                            }
                        }
                    }
                }

            }
            ZZUL.SaveChanges();
            //有些用户只进行过学习操作，则需要下面循环来给0分
            for (int i = 0; i < LID.Length; i++)//控制用户
            {
                for (int j = 0; j < CID.Length; j++)//控制课程
                {
                    var num = ZZUL.PR_SelAll(LID[i], CID[j]).ToArray();
                    if (num.Length == 0)
                    {
                        ZZUL.PR_UserAry(LID[i], CID[j], 0);
                    }
                }

            }
            ZZUL.SaveChanges();
            #endregion
            #endregion
            #region 矩阵分解
            var U = ZZUL.PR_SelectAll().GroupBy(G => G.User_ID).Select(G => G.Key).ToArray();//查找原始矩阵中的所有用户ID
            var list = ZZUL.PR_SelectAll().ToList();
            List<List<double>> shu = new List<List<double>>();
            for (int p = 0; p < U.Length; p++)
            {
                List<double> a = new List<double>();
                foreach (var i in list)
                {

                    if (i.User_ID == U[p])
                        a.Add(Convert.ToDouble(i.Ary_Sco));

                }
                shu.Add(a);
            }
            Matix_Decompose matrix = new Matix_Decompose(UID.Length, CID.Length);
            matrix.get_Matrix(shu);
            #endregion
            #region 将分解后的矩阵写会数据库
            double[,] NewArr = new double[U.Length, CID.Length];
            NewArr = matrix.GetR(); //得到分解后的矩阵
            var ArrID = ZZUL.PR_SelectAll().GroupBy(G => G.User_ID).Select(G => G.Key).ToArray();

            for (int i = 0; i < U.Length; i++)
            {
                for (int j = 0; j < CID.Length; j++)
                {
                    ZZUL.PR_NewAry(U[i], CID[j], NewArr[i, j]);
                }
                
            }
            ZZUL.SaveChanges();
            #endregion
        
        }
    }
}
