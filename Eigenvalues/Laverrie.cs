using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAlgorithm;

namespace Eigenvalues
{
    public class Laverrie
    {
        double[,] matrixA;
        double[] vectorC, vectorS;
        int size;

        public Laverrie(double[,] matrix)
        {
            size = matrix.GetLength(0);
            matrixA = matrix;
            vectorC = new double[size];
            vectorS = new double[size];
            solve();
        }

        void solve()
        {
            for(int i =0;i<size;i++)
                vectorS[i]=CommonAlgorithms.GetTraceMatrix(CommonAlgorithms.PowMatrix(matrixA,i+1));
            

            vectorC[0] = vectorS[0];
            for (int k = 2; k <= size ; k++)
            {
                vectorC[k - 1] = vectorS[k - 1];
                double sum = 0;
                for (int i = 0; i < k-1; i++)
                    sum += vectorC[i] * vectorS[k - i-2];
                vectorC[k-1] -= sum;
                vectorC[k - 1] /= k;
            }
        }

        public double[] VectorC { get { return vectorC; } }
    }
}
