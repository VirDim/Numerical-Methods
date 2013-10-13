using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Methods_Lab2_
{
    public class Yakobi
    {
        double[,] MatrixA;
        double[] VectorB, VectorX, TempVectorX;
        int Size;
        double Eps, Norm,W;
        public int OperationCount { get; private set; }

        public Yakobi(double[,] MatrixA, double[] VectorB)
            : this(MatrixA, VectorB, 0.0001) { }

        public Yakobi(double[,] MatrixA, double[] VectorB, double Eps)
        {
            this.MatrixA = MatrixA;
            this.VectorB = VectorB;
            this.Eps = Eps;
            Size = VectorB.Length;
            VectorX = new double[Size];
            TempVectorX = new double[Size];
            OperationCount = 0;
            Solve();            
        }

        //решение
        void Solve()
        {
            do
            {   
                //приводим к виду x=ax+b и вычисление x
                for (int i = 0; i < Size; i++)
                {
                    TempVectorX[i] = -VectorB[i];
                    for (int j = 0; j < Size; j++)
                    {
                        if (i != j)
                        {
                            TempVectorX[i] += MatrixA[i, j] * VectorX[i];
                        }
                    }
                    TempVectorX[i] /= -MatrixA[i, i];
                }
                //вычисляем норму 
                Norm = Math.Abs(VectorX[0] - TempVectorX[0]);//используется далее в цикле
                for (int i = 0; i < Size; i++)
                {
                    //вычисляем максимальную норму
                    double TempNorm = Math.Abs(VectorX[i] - TempVectorX[i]);
                    if (TempNorm > Norm)
                    {
                        Norm = TempNorm;
                    }
                    VectorX[i] = TempVectorX[i];
                }
                OperationCount++;
            } while (Norm > Eps);//норма меньше eps условие выхода
        }

        public double[] GetVectorX()
        {
            return (double[])VectorX;
        }

        


    }
}
