using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Methods_Lab2_
{
    public class Zeidel
    {

        class ZeidelException : Exception
        {
            public ZeidelException(string msg)
                : base("Решение не может быть найдено:\r\n" + msg) { }               
            
        }

        double[,] matrixA;
        double[] vectorB, vectorX, vectorPrevX;
        double eps, w;
        int size;

        public int Count { get; private set; }

        public Zeidel(double[,] matrixA, double[] vectorB)
            : this(matrixA, vectorB, 0.0001, 1.0) { }

        public Zeidel(double[,] matrixA, double[] vectorB, double w)
            : this(matrixA, vectorB, 0.0001, w) { }

        public Zeidel(double[,] matrixA, double[] vectorB, double eps, double w)
        {
            if (w <= 0.0 | w >= 2.0) throw new ZeidelException("W больше 2.0 или меньше 0");

            this.matrixA = matrixA;
            this.vectorB = vectorB;
            this.eps = eps;
            size = vectorB.Length;
            this.w = w;            
            vectorX = new double[size];
            vectorPrevX = new double[size];
            Count = 0;
            Solve();
        }

        void Solve()
        {

            do
            {

                //задаём начальное приближение
                for (int i = 0; i < size; i++)
                {
                    vectorPrevX[i] = vectorX[i];
                }

                for (int i = 0; i < size; i++)
                {
                    double sum = 0;

                    for (int j = 0; j < i; j++)
                        sum += matrixA[i, j] * vectorX[j];//умножаем на х
                    for (int j = i + 1; j < size; j++)
                        sum += matrixA[i, j] * vectorPrevX[j];//уножаем на найденый х
                    vectorX[i] = ((vectorB[i] - sum)) / matrixA[i, i];
                    vectorX[i] = w * vectorX[i] + (1 - w) * vectorPrevX[i];//дополнительные вычисления для метода верхней релаксации
                }
                Count++;
            } while (!Converge(vectorX, vectorPrevX));

        }

        //проверяем на окночание, если разность вычислений х меньше или равен eps
        bool Converge(double[] vectorX, double[] vectorPrevX)
        {
            for (int i = 0; i < size; i++)
            {
                if (Math.Abs(vectorX[i] - vectorPrevX[i]) >= eps)
                    return false;
            }
            return true;
        }

        public double[] GetVectorX()
        {
            return vectorX;
        }

    }
}
