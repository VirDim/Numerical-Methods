using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            method1();
        }
        static void reg1()
        {
            double[,] matrixA = { { 1.06, 0.994 }, { 0.991, 0.943 } };
            double[] vectorB = { 2.54, 2.44 };

            BadConditionedSLAE.Regularization regul = new BadConditionedSLAE.Regularization(matrixA, vectorB, 10);
            double[] vectorX = regul.GetVectorX();

            for (int i = 0; i < vectorB.Length; i++)
                Console.Write(vectorX[i] + " ");
        }
        static void method1()
        {
            int size = 2;
            double[,] matr = { { 1.07, 0.995 }, { 0.991, 0.944 } }; // вариант 5 
            double[] b = { 2.55, 2.45 };
            double alpha, beta;
            double[,] matr_New = new double[size, size];
            double[] b_New = new double[size];
            double[] x = new double[size];

            
                for (int z = 0; z < size - 1; z++)
                {
                    alpha = matr[0, 0] / (Math.Sqrt(Math.Pow(2, matr[0, 0]) + Math.Pow(2, matr[z + 1, 0])));
                    beta = matr[z + 1, 0] / (Math.Sqrt(Math.Pow(2, matr[0, 0]) + Math.Pow(2, matr[z + 1, 0])));

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if ((1 + i) % 2 != 0)
                                matr_New[i, j] = alpha * matr[0, j] + beta * matr[1, j];
                            else
                                matr_New[i, j] = alpha * matr[1, j] - beta * matr[0, j];
                        }
                        if ((i + 1) % 2 != 0)
                            b_New[i] = alpha * b[0] + beta * b[1];
                        else
                            b_New[i] = alpha * b[1] - beta * b[0];
                    }
                    matr_New[z + 1, z] = 0;

                }
           



            Console.WriteLine(" Рабочая матрица после всех преобразований");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0:0.000000} ", matr_New[i, j]);
                }
                Console.WriteLine("= {0:0.######}", b_New[i]);
            }
            Console.WriteLine();


            // Нахождение Х
            double l = 0;
            for (int i = size - 1; i >= 0; i--)
            {
                for (int k = i; k < size - 1; k++)
                {
                    l += matr_New[i, k + 1] * x[k + 1];
                }
                x[i] = (b_New[i] - l) / matr_New[i, i];
                l = 0;
            }

            // Вывод X
            Console.WriteLine(" X : ");
            for (int i = 0; i < size; i++)
            { Console.WriteLine("{0:0.00####}", x[i]); }
        }
        static void mrthod2()
        {
            int n = 2;
            double alpha = 0.001;
            //double[,] matr = { {  1.03, 0.991 }, { 0.991, 0.943 } }; // из тетрадки пример
            //double[] b = { 2.51, 2.41 };

            //double[,] matr = { { 1.07, 0.995 }, { 0.991, 0.944 } }; // вариант 5 
            // double[] b = { 2.55, 2.45 };
            double[,] matr = { { 1.06, 0.994 }, { 0.991, 0.943 } };
            double[] b = { 2.54, 2.44 };
            double[,] matr_T = new double[n, n];
            double[,] matr_MPY = new double[n, n];
            double[,] E_matr = { { 1, 0 }, { 0, 1 } };
            double[,] E_MPY = new double[n, n];
            double[] b_MPY = new double[n];
            double[] xZero = { 0, 0, 0 };
            //double[] y = new double[3];
            double[] x = new double[n];


            // транспонирование 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matr_T[i, j] = matr[j, i];
                }
            }


            // Умножение матриц
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        matr_MPY[j, i] += matr_T[i, k] * matr[k, j];
                    }
                }
            }

            // Alpha * E
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    E_MPY[i, k] = alpha * E_matr[i, k];
                }
            }

            // Сложение Атр*А + фльфа*Е
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    matr_MPY[i, k] += E_MPY[i, k];
                }
            }

            // Умножение   вектора b  на матрицу Атр
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    b_MPY[i] += b[k] * matr_T[i, k];
                }
            }


            // Сложение Атр*b + alpha*X_0
            for (int i = 0; i < n; i++)
            {
                b_MPY[i] += alpha * xZero[i];
            }


            // Print
            Console.WriteLine(" Рабочая матрица после всех преобразований");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0:0.000000} ", matr_MPY[i, j]);
                }
                Console.WriteLine("= {0:0.##}", b_MPY[i]);
            }
            Console.WriteLine();


            // находим Икы методом гаусса 
            for (int i = 0; i < n - 1; ++i)
            {
                // 1) выбор главного элемента
                double r = matr_MPY[i, i];

                // 2) преобразование текущей строки матрицы A
                for (int j = 0; j < n; ++j)
                    matr_MPY[i, j] /= r;

                // 3) преобразование i-го элемента вектора b
                b_MPY[i] /= r;

                // 4) Вычитание текущей строки из всех нижерасположенных строк
                for (int k = i + 1; k < n; ++k)
                {
                    double p = matr_MPY[k, i];
                    for (int j = i; j < n; ++j)
                        matr_MPY[k, j] -= matr_MPY[i, j] * p;
                    b_MPY[k] -= b_MPY[i] * p;
                    matr_MPY[k, i] = 0.0;
                }
            }

            // Print
            Console.WriteLine(" Рабочая матрица после прямого хода Гаусса");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0:0.0000} ", matr_MPY[i, j]);
                }
                Console.WriteLine("= {0:0.0000}", b_MPY[i]);
            }
            Console.WriteLine();


            //b_MPY[1]+=0.001;

            // Нахождение Х
            double z = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                for (int k = i; k < n - 1; k++)
                {
                    z += matr_MPY[i, k + 1] * x[k + 1];
                }
                x[i] = (b_MPY[i] - z) / matr_MPY[i, i];
                z = 0;
            }

            // Вывод X
            Console.WriteLine(" X : ");
            for (int i = 0; i < n; i++)
            { Console.WriteLine("{0:0.00####}", x[i]); }

            reg1();
        }
    }
}
