using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZULIWeb.Models
{
    class Matix_Decompose
    {
        static int h, l;
        int M;
        public Matix_Decompose(int wh,int wl)
        {
            h = wh;
            l = wl;
        }
        double[,] array = new double[h, l];
        public void get_Matrix(List<List<double>> R)
        {

           
            bool xuanze = true;
            int N = R.Count;
            try
            {
                if (R[0].Count > 0)
                {
                    M = R[0].Count;
                }
            }
            catch
            {
                Console.WriteLine("数据为空");
                Console.ReadKey();
            }
            int A = 2;
            Console.WriteLine("原始矩阵");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write(R[i][j] + ",");
                }
                Console.WriteLine();
            }
            while (xuanze)
            {
                int K = A;
                List<List<double>> P = new List<List<double>>();
                List<List<double>> Q = new List<List<double>>();
                double[,] T = new double[N, M];
                Random ran = new Random(1);
                for (int i = 0; i < N; i++)
                {
                    List<double> temp = new List<double>();
                    for (int j = 0; j < K; j++)
                    {
                        temp.Add(ran.Next(9));
                    }
                    P.Add(temp);
                }
                for (int i = 0; i < K; i++)
                {
                    List<double> temp = new List<double>();
                    for (int j = 0; j < M; j++)
                    {
                        temp.Add(ran.Next(9));
                    }
                    Q.Add(temp);
                }
                mareix_factorization(R, P, Q, K);
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        double temp = 0;
                        for (int k = 0; k < K; k++)
                        {
                            temp += P[i][k] * Q[k][j];
                        }
                        T[i, j] = temp;
                    }
                }
                bool s = false;
                for (int i = 0; i < N; i++)
                {

                    for (int j = 0; j < M; j++)
                    {
                        if (R[i][j] != 0)
                        {
                            double a = Math.Abs(T[i, j] - R[i][j]);
                            if (a > 1)
                            {
                                s = true;
                                break;
                            }
                        }

                    }
                    if (s) { break; }
                }
                if (s == false)
                {
                    xuanze = false;
                    Console.WriteLine("重构矩阵");
                    for (int i = 0; i < N; ++i)
                    {
                        for (int j = 0; j < M; ++j)
                        {
                            Console.Write(T[i, j]);
                            Console.Write("  ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("处理后矩阵");
                 
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < M; j++)
                        {
                            if ((R[i][j] == 0) && (T[i, j] <= 5))
                            {

                                Console.Write(T[i, j]);

                                Console.Write("  ");

                            }

                            else
                            {
                                T[i, j] = R[i][j];
                                Console.WriteLine();
                            }
                        }
                    }


                    array = T;
                    Console.WriteLine("潜在因子数" + A);

                }
                A = A + 1;  
            }        
        }         
        /// <summary>
        /// 返回处理后矩阵
        /// </summary>
        /// <returns></returns>
        public double[,] GetR()
        {
            return array;
        }
        /// <summary>
        /// 矩阵重构
        /// </summary>
        /// <param name="R">原始矩阵R</param>
        /// <param name="P">分解矩阵P</param>
        /// <param name="Q">分解矩阵Q</param>
        /// <param name="K">潜在因子数目K</param>
        /// <param name="steps"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        private void mareix_factorization(List<List<double>> R, List<List<double>> P, List<List<double>> Q, int K, int steps = 5000, double alpha = 0.0002, double beta = 0.02)
        {
            int N = R.Count;
            M = R[0].Count;        
            for (int step = 0; step < steps; ++step)
            {
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < M; ++j)
                    {
                        if (R[i][j] > 0)
                        {
                            //这里面的error 就是公式6里面的e(i,j)
                            double error = R[i][j];
                            for (int k = 0; k < K; ++k)
                            {
                                error -= P[i][k] * Q[k][j];
                            }
                            //更新公式6
                            for (int k = 0; k < K; ++k)
                            {
                                P[i][k] += alpha * (2 * error * Q[k][j] - beta * P[i][k]);
                                Q[k][j] += alpha * (2 * error * P[i][k] - beta * Q[k][j]);
                            }
                        }
                    }
                }
                double loss = 0;
                //计算每一次迭代后的，loss大小，也就是原来R矩阵里面每一个非缺失值跟预测值的平方损失
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < M; ++j)
                    {
                        if (R[i][j] > 0)
                        {
                            double error = 0;
                            for (int k = 0; k < K; ++k)
                            {
                                error += P[i][k] * Q[k][j];
                            }
                            loss += Math.Pow(R[i][j] - error, 2);
                            for (int k = 0; k < K; ++k)
                            {
                                loss += (beta / 2) * (Math.Pow(P[i][k], 2) + Math.Pow(Q[k][j], 2));
                            }
                        }
                    }
                }
                if (loss < 0.001)
                {
                    break;
                }
                //if (step % 1000 == 0)
                //{
                //    Console.WriteLine("loss:" + loss);
                //}
            }
        }
    }
}

