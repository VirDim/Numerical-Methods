using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAlgorithm;

namespace Eigenvalues
{
    public class LeverrieFadeev
    {
        double[][,] matrixA, matrixB;
        double[,] eigenvaluesVectors,matrixE;
        double[] eigenvalues, vectorC;
        int size;


        public LeverrieFadeev(double[,] matrix)
        {
            size = matrix.GetLength(0);
            matrixA = new double[size][,];
            matrixB = new double[size][,];
            for (int i = 0; i < size; i++)
            {
                matrixA[i] = new double[size, size];
                matrixB[i] = new double[size, size];
            }
            vectorC = new double[size];
            matrixE = CommonAlgorithms.GetIdentityMatrix(size);
            matrixA[0] = (double[,])matrix.Clone();
            vectorC[0] = CommonAlgorithm.CommonAlgorithms.GetTraceMatrix(matrixA[0]);
            matrixB[0] = CommonAlgorithm.CommonAlgorithms.DifferenceMatrix(matrixA[0], CommonAlgorithms.MultiplyMatrixOnNumber(matrixE, vectorC[0]));
            solve();
        }

        void solve()
        {
            for (int i = 1; i < size; i++)
            {
                matrixA[i] = CommonAlgorithms.MatrixMultiplication(matrixA[0], matrixB[i - 1]);
                vectorC[i] = CommonAlgorithms.GetTraceMatrix(matrixA[i]) / i + 1;
                matrixB[i] = CommonAlgorithms.DifferenceMatrix(matrixA[i], CommonAlgorithms.MultiplyMatrixOnNumber(matrixE, vectorC[i]));
            }
        }

        //public double[] VectorC { get { return vectorC; } }
        //public 



    }
}
