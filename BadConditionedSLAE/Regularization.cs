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
        double alpha;

        public Regularization(double[,] matrixA, double[] vectorB, double alpha)
        {
            this.matrixA = matrixA;
            this.vectorB = vectorB;
            size = vectorB.Length;
            this.alpha = alpha;
            matrixATranson = CommonAlgorithm.CommonAlgorithm.MatrixTranspose(matrixA); //1.получаем транспонированную матрицу 
            matrixAMod = CommonAlgorithm.CommonAlgorithm.MatrixMultiplication(matrixA, matrixATranson);//2.умножаем матрицу на транспонированную
            matrixE = CommonAlgorithm.CommonAlgorithm.GetIdentityMatrix(size);//получаем единичную матрицу
            matrixEMod = CommonAlgorithm.CommonAlgorithm.MultiplyMatrixOnNumber(matrixE, alpha);//3.умножаем матрицу на альфу
            vectorBMod = CommonAlgorithm.CommonAlgorithm.MultiplyVectorOnMatrix(matrixATranson, vectorB);//5.умножаем вектор на матрицу
            vectorX0 = new double[size];
            solve();
        }

        void solve()
        {
            //4.слаживаем матрицы
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrixAMod[i,j] += matrixEMod[i,j];

            //6.сложение матрицы с приближённым вектором х умноженным на альфу
            for (int i = 0; i < size; i++)
                vectorBMod[i] += vectorX0[i] * alpha;

            Gaus.Gaus gaus = new Gaus.Gaus(matrixAMod, vectorBMod);
            vectorX = gaus.XVector;
        }

        public double[] GetVectorX()
        {
            return vectorX;
        }
    }
}
