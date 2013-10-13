using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadConditionedSLAE
{
    class Givenson
    {
        double[,]
            matrixA,
            matrixAMod;
        double[]
            vectorB,
            vectorBMod,
            vectorX;
        double
            alpha,
            beta;
        int size;

        public Givenson(double[,] matrixA, double[] vectorB)
        {
            size = vectorB.Length;
            this.matrixA = matrixA;
            this.vectorB = vectorB;
            vectorX = new double[size];
            Solve();
        }

        void Solve()
        {
            for (int z = 0; z < size - 1; z++)
            {
                alpha = matrixA[0, 0] / (Math.Sqrt(Math.Pow(2, matrixA[0, 0]) + Math.Pow(2, matrixA[z + 1, 0])));
                beta = matrixA[z + 1, 0] / (Math.Sqrt(Math.Pow(2, matrixA[0, 0]) + Math.Pow(2, matrixA[z + 1, 0])));

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if ((1 + i) % 2 != 0)
                            matrixAMod[i, j] = alpha * matrixA[0, j] + beta * matrixA[1, j];
                        else
                            matrixAMod[i, j] = alpha * matrixA[1, j] - beta * matrixA[0, j];
                    }
                    if ((i + 1) % 2 != 0)
                        vectorBMod[i] = alpha * vectorB[0] + beta * vectorB[1];
                    else
                        vectorBMod[i] = alpha * vectorB[1] - beta * vectorB[0];
                }
                matrixAMod[z + 1, z] = 0;
            }

            double sum = 0;
            for (int i = size - 1; i >= 0; i--)
            {
                for (int k = i; k < size - 1; k++)
                {
                    sum += matrixAMod[i, k + 1] * vectorX[k + 1];
                }
                vectorX[i] = (vectorBMod[i] - sum) / matrixAMod[i, i];
                sum = 0;
            }
        }

        public double[] GetVectorX()
        {
            return vectorX;
        }

    }
}
