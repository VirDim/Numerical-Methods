using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAlgorithm;

namespace QRExpansionNS
{
    public class QRExpansion
    {
        double[][]
            matrixA,
            vectorsE,
            matrixQ,
            matrixR;
        int size;

        public QRExpansion(double[][] matrixA)
        {
            this.matrixA = matrixA;
            size = matrixA.GetLength(0);
            Solve();
        }

        public double[] operator +(double[] vec1, double[] vec2)
        {
            int size = vec1.Length;
            double[] tempVec = new double[size];
            for (int i = 0; i < size; i++)
            {
                tempVec[i] = vec1[i] + vec2[i];
            }
            return tempVec;
        }

        public double[] operator -(double[] vec1, double[] vec2)
        {
            int size = vec1.Length;
            double[] tempVec = new double[size];
            for (int i = 0; i < size; i++)
            {
                tempVec[i] = vec1[i] - vec2[i];
            }
            return tempVec;
        }

        public static double[] operator /(double[] vec1, double scalar)
        {
            int size = vec1.Length;
            double[] tempVec = new double[size];
            for (int i = 0; i < size; i++)
            {
                tempVec[i] = vec1[i] / scalar;
            }
            return tempVec;
        }


        void Solve()
        {
            
            for (int i = 0; i < size; i++)
            {
                double[] vectorU =  matrixA[i];

                double[] sumVec = new double[size];
                for (int j = 0; j < i; j++)
                {
                    sumVec += CommonAlgorithms.GetProjVector()
                    
                }

                matrixQ[i] = vectorU/CommonAlgorithms.GetNormVector(vectorU);
            }
        }

    }
}
