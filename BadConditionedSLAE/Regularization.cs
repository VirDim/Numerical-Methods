using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAlgorithm;

namespace BadConditionedSLAE
{
    public class Regularization
    {
        double[,]
            matrixA,
            matrixATranson,
            matrixAMod,
            matrixE, //Единичная матрица
            matrixEMod;
        double[]
            vectorB,
            vectorX,
            vectorBMod,
            vectorX0;
        int size;
        double
            alpha,
            eps;

        public Regularization(double[,] matrixA, double[] vectorB, double eps)
        {
            this.matrixA = matrixA;
            this.vectorB = vectorB;
            size = vectorB.Length;
            this.alpha = 0;
            matrixATranson = CommonAlgorithm.CommonAlgorithm.MatrixTranspose(matrixA); //1.получаем транспонированную матрицу 
            matrixAMod = CommonAlgorithm.CommonAlgorithm.MatrixMultiplication(matrixA, matrixATranson);//2.умножаем матрицу на транспонированную
            matrixE = CommonAlgorithm.CommonAlgorithm.GetIdentityMatrix(size);//получаем единичную матрицу
            matrixEMod = CommonAlgorithm.CommonAlgorithm.MultiplyMatrixOnNumber(matrixE, alpha);//3.умножаем матрицу на альфу
            vectorBMod = CommonAlgorithm.CommonAlgorithm.MultiplyVectorOnMatrix(matrixATranson, vectorB);//5.умножаем вектор на матрицу
            this.vectorX0 = new double[size];
            this.eps = eps;
            NewSolve();
        }

        void solve()
        {
            double[,] tempMatrixAMod = matrixAMod;
            do
            {

                //суть: представить в виде(At*A+a*E)VX = At*VB+aVX0
                //4.слаживаем матрицы
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        matrixAMod[i, j] += matrixEMod[i, j];

                //6.сложение матрицы с приближённым вектором х умноженным на альфу
                for (int i = 0; i < size; i++)
                    vectorBMod[i] += vectorX0[i] * alpha;

                try
                {
                    Gaus.Gaus gaus = new Gaus.Gaus((double[,])matrixAMod.Clone(), (double[])vectorBMod.Clone());
                    vectorX = gaus.XVector;
                    vectorX0 = vectorX;
                }
                catch { }
                finally
                {
                    alpha += 0.00001;
                    matrixEMod = CommonAlgorithm.CommonAlgorithm.MultiplyMatrixOnNumber(matrixE, alpha);
                    matrixAMod = tempMatrixAMod;
                    Console.WriteLine(CommonAlgorithm.CommonAlgorithm.GetDiscrepancy(matrixAMod, vectorBMod, vectorX0));
                }
            }
            while (CommonAlgorithm.CommonAlgorithm.GetDiscrepancy(matrixAMod, vectorBMod, vectorX0) > eps);
        }

        void NewSolve()
        {
            do
            {

                double[,] tempMatrixAMod = matrixAMod;
                for (int i = 0; i < size; i++)
                    matrixAMod[i, i] += alpha;

                for (int i = 0; i < size; i++)
                    vectorBMod[i] += alpha * vectorX0[i];

                try
                {
                    Gaus.Gaus gaus = new Gaus.Gaus((double[,])matrixAMod.Clone(), (double[])vectorBMod.Clone());
                    
                    vectorX = gaus.XVector;
                    vectorX0 = vectorX;
                }
                catch { }
                finally
                {
                    alpha += 0.00001;
                    matrixEMod = CommonAlgorithm.CommonAlgorithm.MultiplyMatrixOnNumber(matrixE, alpha);
                    matrixAMod = tempMatrixAMod;
                    Console.WriteLine(CommonAlgorithm.CommonAlgorithm.GetDiscrepancy(matrixAMod, vectorBMod, vectorX0));
                }
            }
            while (CommonAlgorithm.CommonAlgorithm.GetDiscrepancy(matrixAMod, vectorBMod, vectorX0) > eps);
        }


        public double[] GetVectorX()
        {
            return vectorX;
        }
    }
}
